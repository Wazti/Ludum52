using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingStrengthSpawn : MonoBehaviour
    {
        [SerializeField] private float CoolDownToSpawn;

        private float _coolDown;

        [ShowInInspector]
        [ProgressBar(0, "CoolDownToSpawn", 0.1f, 1f, 0.2f)]
        public float CoolDown
        {
            get => _coolDown;

            private
            set
            {
                if (value <= 0)
                {
                    _coolDown = 0;
                    return;
                }

                _coolDown = value;
            }
        }

        [SerializeField] [MinMaxSlider(1, 100, true)]
        private Vector2 StrengthRange;

        [SerializeField] private float _strength;

        [SerializeField] private float speedStrength;

        public float Strength
        {
            get => _strength;

            private
            set
            {
                if (value < StrengthRange.x)
                {
                    _strength = StrengthRange.x;
                    return;
                }

                if (value > StrengthRange.y)
                {
                    _strength = StrengthRange.y;
                    return;
                }

                _strength = value;
            }
        }


        [SerializeField] private BuildingWindows buildingWindows;

        private void FixedUpdate()
        {
            CoolDown -= Time.deltaTime * Strength;

            if (buildingWindows.IsActive)
            {
                Strength += Time.deltaTime * speedStrength;
                return;
            }

            Strength -= Time.deltaTime * speedStrength;
        }

        public void ResetCoolDown()
        {
            CoolDown = CoolDownToSpawn;
        }
    }
}