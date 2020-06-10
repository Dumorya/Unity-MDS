using System;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    // 2 joueurs poussent un ballon
    // si le ballon touche un but
    // => score pour l'équipe qui marque (équpe adverse de celle qui prend le but)
    // => on réenge: remise à la position initiale des joueurs et de la balle
    [Serializable]
    public struct Entity
    {
        public GameObject gameObject;
        [HideInInspector]
        public Vector3 initialPosition;
    }

    [Serializable]
    public struct Team
    {
        public GameObject goal;
        public Text score;
    }

    public Entity[] players;
    public Entity ball;
    public Team orange;
    public Team blue;
    private int orangeScore = 0;
    private int blueScore = 0;
    public ParticleSystem boom;
    private float slowdownFactor = 0.1f;
    private float slowdownLength = 2f;


    void Start()
    {
        RecordInitialPositions();
        UpdateScores();
        BallCollisionEmitter emitter = ball.gameObject.GetComponentInChildren<BallCollisionEmitter>();
        emitter.OnCollided += BallCollided;
    }
    
    void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    void Destroy()
    {
        BallCollisionEmitter emitter = ball.gameObject.GetComponent<BallCollisionEmitter>();
        
        emitter.OnCollided -= BallCollided;
    }

    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void BallCollided(Collision2D collision)
    {
        if (collision.collider.gameObject.name == orange.goal.name)
        {
            blueScore++;
            //déclenche l'animation d'explosion de la balle
            Instantiate(boom.gameObject,  collision.GetContact(0).point, Quaternion.identity);
            DoSlowmotion();

            yield return new WaitForSeconds(2);

            UpdateScores();
            ResetPositions();
        }
        else if (collision.collider.gameObject.name == blue.goal.name)
        {
            orangeScore++;
            //déclenche l'animation d'explosion de la balle
            Instantiate(boom.gameObject,  collision.GetContact(0).point, Quaternion.identity);
            DoSlowmotion();

            yield return new WaitForSeconds(2);

            UpdateScores();
            ResetPositions();
        }
    }

    private void ResetPositions()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.transform.position = players[i].initialPosition;
        }
        ball.gameObject.transform.position = ball.initialPosition;
        ball.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void RecordInitialPositions()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].initialPosition = players[i].gameObject.transform.position;
        }
        ball.initialPosition = ball.gameObject.transform.position;
    }

    public void UpdateScores()
    {
        orange.score.text = orangeScore.ToString();
        blue.score.text = blueScore.ToString();
    }
}