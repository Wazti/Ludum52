using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.Scriptables
{
    [Serializable]
    public abstract class ConfigContainer<TValue, TKey> : ScriptableObject, IConfigContainer<TValue, TKey>
    {
        [SerializeField] private List<Config> Prefabs = new List<Config>();


        public TValue Get(TKey key)
        {
            foreach (var prefab in Prefabs)
            {
                if (key.Equals(prefab.Key))
                {
                    return prefab.Value;
                }
            }

            throw new Exception(String.Format($"Prefab for {key} was not found"));
        }

        public bool IsKey(TKey key)
        {
            foreach (var prefab in Prefabs)
            {
                if (key.Equals(prefab.Key))
                {
                    return true;
                }
            }

            return false;
        }
        

        [Serializable]
        private struct Config
        {
            public TValue Value;
            public TKey Key;

            public Config(TKey Key, TValue Value)
            {
                this.Key = Key;
                this.Value = Value;
            }
        }
    }
}