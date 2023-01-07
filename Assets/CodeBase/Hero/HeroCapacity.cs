using System;
using System.Collections.Generic;
using CodeBase.Unit;
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
    }
}