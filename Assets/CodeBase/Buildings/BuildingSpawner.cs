using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [Inject] private IUnitPoolingService _unitPoolingService;


        [SerializeField] private BuildingWindows buildingWindows;
        [SerializeField] private BuildingContainer buildingContainer;
        [SerializeField] private BuildingStrengthSpawn buildingStrength;

        [ShowInInspector] private bool IsReady => buildingStrength && buildingStrength.CoolDown <= 0;

        [ShowInInspector] private bool IsNeed => buildingWindows.activeWindows.Count > 0;

        [ShowInInspector] private bool IsLessUnits => buildingContainer.IsContainUnits;

        private void FixedUpdate()
        {
            if (!IsReady || !IsNeed || !IsLessUnits) return;

            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            buildingStrength.ResetCoolDown();

            var type = buildingContainer.GetRandomUnit();

            var unit = _unitPoolingService.SpawnUnit(type);

            var pos = buildingWindows.GetRandomWindow().GetPosition();

            unit.transform.position = pos;
        }
    }
}