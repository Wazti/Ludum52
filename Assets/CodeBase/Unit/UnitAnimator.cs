using System;
using Aarthificial.Reanimation;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Unit
{
    public class UnitAnimator : MonoBehaviour, IAnimationReader
    {
        public Reanimator animator;

        public AnimatorState State { get; private set; }


        private void Awake()
        {
            animator.AddListener(AnimatorDrivers.State, StateFor);
        }


        public void ResetToIdle()
        {
            SetState(AnimatorState.Idle);
        }

        public void SetFlip(bool flip)
        {
            animator.Flip = flip;
        }

        public void Walk()
        {
            SetState(AnimatorState.Walking);
        }

        public void Fly()
        {
            SetState(AnimatorState.Fly);
        }

        public void Fall()
        {
            SetState(AnimatorState.Fall);
        }

        public void SetState(AnimatorState state)
        {
            animator.Set(AnimatorDrivers.State, (int) state);
        }

        private void StateFor()
        {
            var intHash = animator.State.Get(AnimatorDrivers.State);
            State = (AnimatorState) intHash;
        }
    }
}