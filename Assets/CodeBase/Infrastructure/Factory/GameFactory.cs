using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        [Inject] private DiContainer _container;

        public GameObject Hero { get; set; }
        private IStaticDataService _staticData;

        public void Register(ISavedProgressReader savedProgress)
        {
        }


        public void CreateHero()
        {
            Hero = _container.InstantiatePrefabResource("Hero/Hero");
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public void Cleanup()
        {
        }
    }
}