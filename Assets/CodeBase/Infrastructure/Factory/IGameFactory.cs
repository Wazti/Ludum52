using System.Collections.Generic;
using CodeBase.Hero;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        public void Register(ISavedProgressReader savedProgress);

        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
    }
}