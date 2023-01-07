using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Services;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IStaticDataService _staticData;

        public void Register(ISavedProgressReader savedProgress)
        {
            
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public void Cleanup()
        {
        }
    }
}