using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using CodeBase.Unit;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroCapacity : MonoBehaviour
    {
        [Inject] private ILevelSessionService _levelSessionService;
       
        private readonly List<UnitInfo> _currentUnits = new List<UnitInfo>();

        public List<UnitInfo> CurrentEnemiesTaken
        {
            get => _currentUnits;
        }

        [Inject] private IUnitPoolingService _unitPoolingService;

        public event Action ModifyTakenEnemies;

        public void AddUnit(IUnitIntakes unitIntakes)
        {
            var unit = new UnitInfo(unitIntakes.Mass, unitIntakes.UnitType);
            _currentUnits.Add(unit);
            _levelSessionService.AddUnit(unit);
            _unitPoolingService.DespawnUnit(unitIntakes.GameObject.GetComponent<BaseUnit>());

            ModifyTakenEnemies?.Invoke();
        }

        [ShowInInspector] private float CountUnitsIn => _currentUnits.Count;
        [ShowInInspector] private float TotalWeight => _currentUnits.Aggregate(0f, (acc, x) => acc + x.Mass);
    }
}