using System.Collections.Generic;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        public void CreateHero();
        
        public GameObject Hero { get; }
        public void Register(ISavedProgressReader savedProgress);

        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
    }
}