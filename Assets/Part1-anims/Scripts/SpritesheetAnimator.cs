using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Anims
{
    Iddle,
    Run,
    Roll
}

[RequireComponent(typeof(SpriteRenderer))]
public class SpritesheetAnimator : MonoBehaviour
{
    
    [Tooltip("animation speed in images per seconds")]
    public float animationSpeed = 25;

    // TODO: Utiliser un système plus générique pour définir les animations
    public Sprite[] iddle;
    public Sprite[] run;
    public Sprite[] roll;


    // accessors (getters)
    public Anims CurrentAnimation => currentAnimation;
    public int LoopCount => loopCount;
    public float animationFrameDuration => 1f / animationSpeed;

    // private properties
    private SpriteRenderer spriteRenderer;
    private Anims currentAnimation;
    private int currentFrameIndex = 0;
    // time in seconds before next frame is displayed
    private float nextFrameCoolDown;
    // number of time the current animation have been played
    private int loopCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextFrameCoolDown = animationFrameDuration;
        currentAnimation = Anims.Iddle;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentAnimationFrames().Length == 0) return;

        if (nextFrameCoolDown <= 0)
        {
            AnimateNextFrame();
        }

        nextFrameCoolDown -= Time.deltaTime;
    }

    public void AnimateNextFrame()
    {
        currentFrameIndex = (currentFrameIndex + 1) % GetCurrentAnimationFrames().Length;
        spriteRenderer.sprite = GetCurrentAnimationFrames()[currentFrameIndex];
        nextFrameCoolDown += animationFrameDuration;
        if (currentFrameIndex == 0) loopCount++;
    }

    // TODO: permettre un callback lorsque l'animation est terminée
    public void Play(Anims nextAnimation)
    {
        if (currentAnimation == nextAnimation) return;
        currentAnimation = nextAnimation;
        currentFrameIndex = 0;
        loopCount = 0;
    }

    private Sprite[] GetCurrentAnimationFrames()
    {
        switch (currentAnimation)
        {
            case Anims.Iddle:
                return iddle;
            case Anims.Run:
                return run;
            case Anims.Roll:
                return roll;
        }
        return iddle;
    }
}
