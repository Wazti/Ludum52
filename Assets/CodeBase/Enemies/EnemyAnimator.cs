using Aarthificial.Reanimation;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAnimator: MonoBehaviour
    {
        public Reanimator animator;

        public EnemyAnimatorState State { get; private set; }


        private void Awake()
        {
            animator.AddListener(AnimatorDrivers.State, StateFor);
        }


        public void ResetToIdle()
        {
            SetState(EnemyAnimatorState.Idle);
        }

        public void SetFlip(bool flip)
        {
            animator.Flip = flip;
        }

        public void Walk()
        {
            SetState(EnemyAnimatorState.Walking);
        }

        public void Shoot()
        {
            SetState(EnemyAnimatorState.Shoot);
        }

        public void SetState(EnemyAnimatorState state)
        {
            animator.Set(AnimatorDrivers.State, (int) state);
        }

        private void StateFor()
        {
            var intHash = animator.State.Get(AnimatorDrivers.State);
            State = (EnemyAnimatorState) intHash;
        }
    }
}