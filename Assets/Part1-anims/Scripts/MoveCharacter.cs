using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator))]
public class MoveCharacter : MonoBehaviour
{
    private const string ROLL = "roll";

    [Tooltip("Speed in Unit per second")]
    public float speed = 5f;

    private SpriteRenderer spriteRenderer;
    private SpritesheetAnimator animator;

    // COOLDOWNS
    [Tooltip("Cooldown of a roll in seconds")]
    public float rollCooldownDuration = 1;
    private float rollCooldown = 0;

    void Start()
    {
        this.transform.position = new Vector3(2, 2, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<SpritesheetAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left;
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right;
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && rollCooldown <= 0)
        {
            animator.Play(Anims.Roll);
            rollCooldown = rollCooldownDuration;
        }
        if (animator.CurrentAnimation != Anims.Roll || animator.LoopCount >= 1)
        {
            if (move.magnitude > 0)
            {
                animator.Play(Anims.Run);
            }
            else
            {
                animator.Play(Anims.Iddle);
            }
        }

        this.transform.position = this.transform.position + speed * Time.deltaTime * move.normalized;
        rollCooldown -= Time.deltaTime;
    }
}