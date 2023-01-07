using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroCapacity : MonoBehaviour
    {
        private List<EnemyInfo> _currentEnemies = new List<EnemyInfo>();

        public List<EnemyInfo> CurrentEnemiesTaken
        {
            get => _currentEnemies;
        }

        public event Action ModifyTakenEnemies;

        public void AddEnemy(IEnemyIntakes enemyIntakes)
        {
            _currentEnemies.Add(new EnemyInfo(enemyIntakes.Mass));
           
            Destroy(enemyIntakes.GameObject);
            
            ModifyTakenEnemies?.Invoke();
        }
    }
}