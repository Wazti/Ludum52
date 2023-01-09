using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Sections;
using CodeBase.Services;
using CodeBase.Unit;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<UnitType> unitTypes;

        [SerializeField] private float distanceSpawn;

        [MinMaxSlider(4f, 20.0f, true)] public Vector2 timeToSpawn;


        [Inject] private IGameFactory _gameFactory;

        [Inject] private IUnitPoolingService _unitPoolingService;

        [SerializeField] private float coolDown = 5f;

        private float _coolDown;


        private void Awake()
        {
            _coolDown = Random.Range(timeToSpawn.x, timeToSpawn.y);
        }

        private void Update()
        {
            _coolDown -= Time.deltaTime;

            if (_coolDown < 0)
            {
                _coolDown = Random.Range(timeToSpawn.x, timeToSpawn.y);
                GenerateUnit();
            }
        }


        private void GenerateUnit()
        {
            var unitType = unitTypes[Random.Range(0, unitTypes.Count)];

            var prefab = _unitPoolingService.SpawnUnit(unitType);

            var diff = -15f;
            if (Random.Range(0, 100) > 50)
            {
                diff = 15f;
            }

            prefab.transform.position =
                new Vector3(transform.position.x + diff,
                    -3.08f, 0);

            prefab.transform.SetParent(transform);

            prefab.gameObject.SetActive(true);
        }
    }
}