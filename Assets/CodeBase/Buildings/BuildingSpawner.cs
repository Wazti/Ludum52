using System;
using CodeBase.Infrastructure.Factory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [Inject] private IUnitFactory _unitFactory;

        [SerializeField] private float CoolDownToSpawn;

        private float _coolDown;

        [ShowInInspector]
        [ProgressBar(0, "CoolDownToSpawn", 0.1f, 1f, 0.2f)]
        private float CoolDown
        {
            get => _coolDown;

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

        [SerializeField] private BuildingWindows buildingWindows;
        [SerializeField] private BuildingContainer buildingContainer;

        [ShowInInspector] private bool IsReady => CoolDown <= 0;

        [ShowInInspector] private bool IsNeed => buildingWindows.activeWindows.Count > 0;

        [ShowInInspector] private bool IsLessUnits => buildingContainer.IsContainUnits;

        private void FixedUpdate()
        {
            CoolDown -= Time.deltaTime;

            if (!IsReady || !IsNeed || !IsLessUnits) return;

            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            CoolDown = CoolDownToSpawn;

            var type = buildingContainer.GetRandomUnit();

            var unit = _unitFactory.Create(type);

            var pos = buildingWindows.GetRandomWindow().GetPosition();

            unit.transform.position = pos;
        }
    }
}