﻿using Aarthificial.Reanimation;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        public Reanimator animator;

        public HeroAnimatorState State { get; private set; }


        private void Awake()
        {
            animator.AddListener(AnimatorDrivers.State, StateFor);
        }


        public void ResetToIdle()
        {
            SetState(HeroAnimatorState.Idle);
        }

        public void ShowFly()
        {
            SetState(HeroAnimatorState.ShowIntake);
        }

        public void HideFly()
        {
            SetState(HeroAnimatorState.HideIntake);
        }

        public void SetFlip(bool flip)
        {
            animator.Flip = flip;
        }


        public void SetState(HeroAnimatorState state)
        {
            animator.Set(AnimatorDrivers.State, (int) state);
        }

        private void StateFor()
        {
            var intHash = animator.State.Get(AnimatorDrivers.State);
            State = (HeroAnimatorState) intHash;
        }
    }
}