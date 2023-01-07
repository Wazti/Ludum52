using System;

namespace CodeBase.Infrastructure
{
    public interface ISceneLoader
    {
        public void Load(string name, Action onLoaded);
    }
}