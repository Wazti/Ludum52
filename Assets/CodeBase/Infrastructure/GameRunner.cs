using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        [Inject] private DiContainer _container;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
                _container.InstantiatePrefabForComponent<GameBootstrapper>(BootstrapperPrefab);
        }
    }
}