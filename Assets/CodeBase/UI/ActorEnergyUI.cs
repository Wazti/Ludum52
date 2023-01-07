using System;
using CodeBase.Hero;
using UniRx;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorEnergyUI : MonoBehaviour
    {
        public HpBar HpBar;

        [SerializeField] private HeroEnergy heroEnergy;

        private readonly CompositeDisposable _dispose = new CompositeDisposable();

        private void Awake()
        {
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