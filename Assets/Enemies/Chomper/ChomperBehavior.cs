using UnityEditor.VersionControl;
using UnityEngine;

namespace Gamekit3D
{
    [DefaultExecutionOrder(100)]
    public class ChomperBehavior : MonoBehaviour
    {
        public static readonly int hashInPursuit = Animator.StringToHash("InPursuit");
        public static readonly int hashAttack = Animator.StringToHash("Attack");
        public static readonly int hashHit = Animator.StringToHash("Hit");
        public static readonly int hashVerticalDot = Animator.StringToHash("VerticalHitDot");
        public static readonly int hashHorizontalDot = Animator.StringToHash("HorizontalHitDot");
        public static readonly int hashThrown = Animator.StringToHash("Thrown");
        public static readonly int hashGrounded = Animator.StringToHash("Grounded");
        public static readonly int hashVerticalVelocity = Animator.StringToHash("VerticalVelocity");
        public static readonly int hashSpotted = Animator.StringToHash("Spotted");
        public static readonly int hashNearBase = Animator.StringToHash("NearBase");

        public static readonly int hashIdleState = Animator.StringToHash("ChomperIdle");

        public EnemyController controller { get { return m_Controller; } }


        public Vector3 originalPosition { get; protected set; }
        [System.NonSerialized]
        public float attackDistance = 3;

        [Tooltip("Time in seconde before the Chomper stop pursuing the player when the player is out of sight")]
        public float timeToStopPursuit;

        [Header("Audio")]
        public RandomAudioPlayer attackAudio;
        public RandomAudioPlayer frontStepAudio;
        public RandomAudioPlayer backStepAudio;
        public RandomAudioPlayer hitAudio;
        public RandomAudioPlayer gruntAudio;
        public RandomAudioPlayer deathAudio;
        public RandomAudioPlayer spottedAudio;

        protected float m_TimerSinceLostTarget = 0.0f;

        protected EnemyController m_Controller;

        protected void OnEnable()
        {
            m_Controller = GetComponentInChildren<EnemyController>();

            originalPosition = transform.position;

        }

        /// <summary>
        /// Called by animation events.
        /// </summary>
        /// <param name="frontFoot">Has a value of 1 when it's a front foot stepping and 0 when it's a back foot.</param>
        void PlayStep(int frontFoot)
        {
            if (frontStepAudio != null && frontFoot == 1)
                frontStepAudio.PlayRandomClip();
            else if (backStepAudio != null && frontFoot == 0)
                backStepAudio.PlayRandomClip ();
        }

        /// <summary>
        /// Called by animation events.
        /// </summary>
        public void Grunt ()
        {
            if (gruntAudio != null)
                gruntAudio.PlayRandomClip ();
        }

        public void Spotted()
        {
            if (spottedAudio != null)
                spottedAudio.PlayRandomClip();
        }

        protected void OnDisable()
        {

        }

        private void FixedUpdate()
        {
            
        }

        public void FindTarget()
        {
            //we ignore height difference if the target was already seen
            
        }

        public void StartPursuit()
        {
            
        }

        public void StopPursuit()
        {
            
        }

        public void RequestTargetPosition()
        {
            
        }

        public void WalkBackToBase()
        {
            
        }

        public void TriggerAttack()
        {
           
        }

        public void AttackBegin()
        {
            
        }

        public void AttackEnd()
        {
            
        }


       
    }
}