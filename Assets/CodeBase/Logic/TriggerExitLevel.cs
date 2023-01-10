using System;
using CodeBase.Hero;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using DG.Tweening;
using FMOD.Studio;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Logic
{
    public class TriggerExitLevel : MonoBehaviour
    {
        [Inject] private IGameFactory _gameFactory;
        [Inject] private ILevelSessionService _levelService;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private ICurtainService _curtainService;

        [SerializeField] private TriggerObserver triggerObserver;
        private readonly CompositeDisposable _dispose = new CompositeDisposable();

        [SerializeField] private Image _blackScreen;
        [SerializeField] private TextMeshProUGUI text;

        private bool _ended;
        private Bus _playerBus;

        private void Start()
        {
            _playerBus = FMODUnity.RuntimeManager.GetBus("bus:/");
            triggerObserver.TriggerEnter += OnHeroEnter;
            _gameFactory.Hero.GetComponent<HeroHealth>().OnHealthChange += OnHealthChange;
            var energy = _gameFactory.Hero.GetComponent<HeroEnergy>();
            energy.Energy?.Subscribe(_ => OnChangeBar(energy.Energy.Value)).AddTo(_dispose);
        }

        private void OnChangeBar(float energyValue)
        {
            if (_ended) return;

            if (!(energyValue <= 0)) return;

            LoseLevel();
            _ended = true;
        }

        private void LoseLevel()
        {
            _gameFactory.Hero.GetComponent<HeroDie>().Die();

            _blackScreen.DOFade(1, 1).SetDelay(0.5f).OnComplete(() =>
            {
                text.text = "Oh, you loose. Your harvest - 0 humans";
                text.gameObject.SetActive(true);
            });

            _levelService.LoseLevel();
            _playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
            DOVirtual.DelayedCall(3f, () =>
            {
                _curtainService.DOFade(1, 1f,
                    () => { _sceneLoader.Load("Home", () => { _curtainService.DOFade(0, .5f, () => { }); }); });
            }, true);
        }

        private void OnHealthChange(int arg1, int arg2)
        {
            if (_ended) return;

            if (arg1 > 0) return;

            _ended = true;
            LoseLevel();
        }


        private void OnHeroEnter(Collider2D obj)
        {
            if (_ended) return;

            var count = _levelService.CurrentUnits.Count;
            _ended = true;
            _levelService.WinLevel();
            _blackScreen.DOFade(1, 1).SetDelay(0.5f).OnComplete(() =>
            {
                text.text = $"Nice. Your harvest - {count} humans";
                text.gameObject.SetActive(true);
            });

            _playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
            DOVirtual.DelayedCall(3f, () =>
            {
                _curtainService.DOFade(1, 1f,
                    () => { _sceneLoader.Load("Home", () => { _curtainService.DOFade(0, .5f, () => { }); }); });
            }, true);
        }
    }
}