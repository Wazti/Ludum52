using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Sections;
using CodeBase.Services;
using CodeBase.Unit;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private SectionDynamic sectionParent;

        [SerializeField] private List<UnitType> unitTypes;

        [SerializeField] private float distanceSpawn;

        [MinMaxSlider(4f, 20.0f)] public Vector2 timeToSpawn;

        [SerializeField] private int maxUnits;

        private int _lessUnits;

        private Coroutine lastCoroutine;

        [Inject] private IUnitPoolingService _unitPoolingService;

        private void Awake()
        {
            _lessUnits = maxUnits;


            GenerateUnits();
        }

        private void GenerateUnits()
        {
            var count = Random.Range(1, (maxUnits + 1) / 2);

            for (int i = 0; i < count; i++)
            {
                GenerateUnit();
            }

            lastCoroutine = StartCoroutine(DelayedSpawn());
        }

        private IEnumerator DelayedSpawn()
        {
            GenerateUnit();

            yield return new WaitForSeconds(Random.Range(timeToSpawn.x, timeToSpawn.y));

            if (_lessUnits <= 0)
            {
                yield break;
            }

            lastCoroutine = StartCoroutine(DelayedSpawn());
        }

        private void GenerateUnit()
        {
            _lessUnits -= 1;

            var unitType = unitTypes[Random.Range(0, unitTypes.Count)];

            var prefab = _unitPoolingService.SpawnUnit(unitType);

            prefab.transform.position =
                new Vector3(transform.position.x + Random.Range(-distanceSpawn / 2, distanceSpawn / 2),
                    transform.position.y, 0);

            prefab.transform.SetParent(transform);

            prefab.gameObject.SetActive(true);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(200, 100, 20, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(distanceSpawn, 1f, 0));
        }
    }
}