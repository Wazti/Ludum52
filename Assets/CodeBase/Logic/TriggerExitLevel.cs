using System;
using CodeBase.Infrastructure;
using CodeBase.Services;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class TriggerExitLevel : MonoBehaviour
    {
        [Inject] private ILevelSessionService _levelService;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private ICurtainService _curtainService;
        
        [SerializeField] private TriggerObserver triggerObserver;

        private void Awake()
        {
            triggerObserver.TriggerEnter += OnHeroEnter;
        }

        private void OnHeroEnter(Collider2D obj)
        {
            _levelService.WinLevel();
            
            _curtainService.DOFade(1, .5f, () =>
            {
                _sceneLoader.Load("Home", () =>
                {
                    _curtainService.DOFade(0, .5f, ()=> { });
                });
            });
            
          
        }
    }
}