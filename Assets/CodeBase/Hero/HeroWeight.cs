using System;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody2D), typeof(HeroCapacity))]
    public class HeroWeight : MonoBehaviour
    {
        public float CurrentWeight = 0f;

        public float MaxWeight = 500f;

        public float deltaWeight = 200f;

        public float StrengthDown;

        [SerializeField] private HeroCapacity heroCapacity;
        [SerializeField] private HeroTickUnits heroIntake;


        [SerializeField] private Rigidbody2D _rigidBody;

        public bool IsOverWeight => CurrentWeight > MaxWeight;

        private void Awake()
        {
            heroCapacity.ModifyTakenEnemies += OnCapacityChange;
            heroIntake.ChangeProcessUnit += OnProcessChange;
        }

        public void Update()
        {
            if (!IsOverWeight) return;

            _rigidBody.velocity -= new Vector2(0, GetWeightGravity() * Time.deltaTime);
        }

        private void OnProcessChange()
        {
            RecalculateWeight();
        }

        private void OnCapacityChange()
        {
            RecalculateWeight();
        }

        private void OnDestroy()
        {
            heroCapacity.ModifyTakenEnemies -= OnCapacityChange;
            heroIntake.ChangeProcessUnit -= OnProcessChange;
        }

        private void RecalculateWeight()
        {
            var weight = 0f;

            heroCapacity.CurrentEnemiesTaken.ForEach(enemy => weight += enemy.Mass);
            heroIntake.GetCurrentProcessEnemies().ForEach(enemy => weight += enemy.Mass);

            CurrentWeight = weight;
        }

        private float GetWeightGravity()
        {
            if (CurrentWeight <= MaxWeight) return 0;

            if (MaxWeight + deltaWeight < CurrentWeight) return StrengthDown;

            return CurrentWeight / (MaxWeight + deltaWeight) * StrengthDown;
        }
    }
}