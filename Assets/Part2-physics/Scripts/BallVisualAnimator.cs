using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class BallVisualAnimator : MonoBehaviour
    {
        public int animationSpeedRatio = 5;

        private Rigidbody2D body;
        private SpritesheetAnimator animator;
        public ParticleSystem ps;
        public float HighSpeedEffectTriggerThreshold = 2;

        void Start()
        {
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 velocity = body.velocity;
            float amplitude = velocity.magnitude;
            var emission = ps.emission;

            animator.animationSpeed = amplitude * animationSpeedRatio;
            body.rotation = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);

            emission.rateOverTime = amplitude * 15;
            
            ps.gameObject.SetActive(amplitude > 2);
        }
    }
}