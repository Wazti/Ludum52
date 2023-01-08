using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Unit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroCapacity : MonoBehaviour
    {
        private readonly List<UnitInfo> _currentUnits = new List<UnitInfo>();

        public List<UnitInfo> CurrentEnemiesTaken
        {
            get => _currentUnits;
        }

        public event Action ModifyTakenEnemies;

        public void AddUnit(IUnitIntakes unitIntakes)
        {
            _currentUnits.Add(new UnitInfo(unitIntakes.Mass));

            Destroy(unitIntakes.GameObject);

            ModifyTakenEnemies?.Invoke();
        }

        [ShowInInspector] private float CountUnitsIn => _currentUnits.Count;
        [ShowInInspector] private float TotalWeight => _currentUnits.Aggregate(0f, (acc, x) => acc + x.Mass);
    }
}