using System;
using Cinemachine;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class InitSpawner : MonoBehaviour
    {
        [Inject] private IGameFactory _gameFactory;

        [SerializeField] private CinemachineVirtualCamera _camera;
        private void Awake()
        {
            _gameFactory.CreateHero();
            _gameFactory.Hero.transform.SetParent(transform);
            _camera.Follow = _gameFactory.Hero.transform;
        }
    }
}