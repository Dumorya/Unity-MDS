using System;
using System.Linq;
using UnityEngine;

namespace Part2
{
    public enum Anims
    {
        Iddle,
        Run,
        Roll
    }

    [Serializable]
    public struct AnimDefinition
    {
        public Anims name;
        public Sprite[] frames;
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class SpritesheetAnimator : MonoBehaviour
    {

        [Tooltip("animation speed in images per seconds")]
        public float animationSpeed = 25;

        public AnimDefinition[] animations;

        // accessors (getters)
        public AnimDefinition CurrentAnimation => currentAnimation;
        public int LoopCount => loopCount;
        public float animationFrameDuration => 1f / animationSpeed;

        // private properties
        private SpriteRenderer spriteRenderer;
        private AnimDefinition currentAnimation;

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
            currentAnimation = animations[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (GetCurrentAnimationFrames().Length == 0) return;

            if (nextFrameCoolDown <= 0)
            {
                AnimateNextFrame();
            }
            
            // in case the animation speed is very slow then accelerated
            if (animationFrameDuration < nextFrameCoolDown) nextFrameCoolDown = animationFrameDuration;
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
            if (currentAnimation.name == nextAnimation) return;
            currentAnimation = animations.First(a => a.name == nextAnimation);
            currentFrameIndex = 0;
            loopCount = 0;
        }

        private Sprite[] GetCurrentAnimationFrames()
        {
            return currentAnimation.frames;
        }
    }
}