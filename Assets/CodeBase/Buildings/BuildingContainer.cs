using System;
using System.Collections.Generic;
using CodeBase.Unit;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Buildings
{
    public class BuildingContainer : MonoBehaviour
    {
        [MinMaxSlider(1, 50, true)] [SerializeField]
        private Vector2Int rangeUnits;

        [SerializeField] private List<UnitType> types;

        [SerializeField] private List<UnitType> _generatedUnits = new List<UnitType>();

        [ShowInInspector]
        [ProgressBar(0, "_maxUnits", 1f, 0f, 0f)]
        private int LessUnits
        {
            get => _generatedUnits.Count;
        }

        private int _maxUnits;

        private void Awake()
        {
            GenerateUnits();
        }


        [Button(ButtonSizes.Small)]
        private void GenerateUnits()
        {
            var length = Random.Range(rangeUnits.x, rangeUnits.y);

            for (int i = 0; i < length; i++)
            {
                _generatedUnits.Add(types[Random.Range(0, types.Count)]);
            }

            _maxUnits = _generatedUnits.Count;
        }

        public bool IsContainUnits => LessUnits > 0;

        public UnitType GetRandomUnit()
        {
            var item = _generatedUnits[Random.Range(0, _generatedUnits.Count)];

            _generatedUnits.Remove(item);

            return item;
        }
    }
}