using System;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class ActorEnergyUI : MonoBehaviour
    {
        [Inject] private IGameFactory _gameFactory;

        public HpBar HpBar;

         private HeroEnergy heroEnergy;

        private readonly CompositeDisposable _dispose = new CompositeDisposable();

        private void Start()
        {
            heroEnergy = _gameFactory.Hero.GetComponent<HeroEnergy>();
            heroEnergy.Energy?.Subscribe(_ => OnChangeBar(heroEnergy.Energy.Value)).AddTo(_dispose);
        }

        private void OnChangeBar(float v)
        {
            HpBar.SetValue(v, heroEnergy.MaxEnergy);
        }

        private void OnDisable()
        {
            _dispose?.Clear();
        }
    }
}