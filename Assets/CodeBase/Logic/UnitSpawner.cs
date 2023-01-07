using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Unit;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private List<UnitType> unitTypes;

        [SerializeField] private float distanceSpawn;
        
        [MinMaxSlider(4f, 20.0f)]
        public Vector2 timeToSpawn;

        [SerializeField] private int maxUnits;
        

        [Inject] private IUnitFactory _unitFactory;

        private void Awake()
        {
            GenerateUnits();
        }

        private void GenerateUnits()
        {
            var count = Random.Range(1, maxUnits + 1);

            for (int i = 0; i < count; i++)
            {
                GenerateUnit();
            }
        }

        private void GenerateUnit()
        {
            var unitType = unitTypes[Random.Range(0, unitTypes.Count)];

            var prefab = _unitFactory.Create(unitType);

            prefab.transform.position = new Vector3(Random.Range(-distanceSpawn, distanceSpawn), transform.position.y, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(200, 100, 20, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(distanceSpawn, 1f, 0));
        }
    }
}