using System;
using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class MoveCharacter : MonoBehaviour
    {
        public enum PlayerControls
        {
            ZQSDF,
            ArrowAnd0
        }
        private const string ROLL = "roll";

        [Tooltip("Speed in Unit per second")] public float speed = 5f;

        private SpriteRenderer spriteRenderer;
        private SpritesheetAnimator animator;
        private Rigidbody2D body;
        public ParticleSystem ShootFX;
        public ParticleSystem dust;
        public float ShootStrength;

        public PlayerControls controls = PlayerControls.ArrowAnd0;
        
        // COOLDOWNS
        [Tooltip("Cooldown of a roll in seconds")]
        public float rollCooldownDuration = 1;

        private float rollCooldown = 0;

        private void OnCollisionEnter2D(Collision2D other)
        {
            // si on roule, on déclache un effet visuel et la balle s'accélère
            if ((Input.GetKeyDown(KeyCode.Keypad0) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.F) && controls == PlayerControls.ZQSDF) && rollCooldown <= 0)
            {
                if (other.rigidbody.bodyType == RigidbodyType2D.Dynamic)
                {
                    // vitesse sur la balle
                    other.rigidbody.AddForce(- other.GetContact(0).normal * ShootStrength, ForceMode2D.Impulse);
                    // effet visuel
                    Instantiate(ShootFX.gameObject,  other.GetContact(0).point, Quaternion.identity);
                }
            }
        }

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 vitesse = Vector2.zero;
            if (Input.GetKey(KeyCode.UpArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.Z) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.up;
                Instantiate(dust.gameObject, body.position, Quaternion.identity);
            }

            if (Input.GetKey(KeyCode.DownArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.S) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.down;
                Instantiate(dust.gameObject, body.position, Quaternion.identity);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.Q) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.left;
                spriteRenderer.flipX = true;
                Instantiate(dust.gameObject, body.position, Quaternion.identity);
            }

            if (Input.GetKey(KeyCode.RightArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.D) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.right;
                spriteRenderer.flipX = false;
                Instantiate(dust.gameObject, body.position, Quaternion.identity);
            }

            if ((Input.GetKeyDown(KeyCode.Keypad0) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.F) && controls == PlayerControls.ZQSDF) && rollCooldown <= 0)
            {
                animator.Play(Anims.Roll);
                rollCooldown = rollCooldownDuration;
            }

            if (animator.CurrentAnimation.name != Anims.Roll || animator.LoopCount >= 1)
            {
                if (vitesse.magnitude > 0)
                {
                    animator.Play(Anims.Run);
                }
                else
                {
                    animator.Play(Anims.Iddle);
                }
            }

            body.velocity = vitesse.normalized * speed;

            rollCooldown -= Time.deltaTime;
        }
    }
}