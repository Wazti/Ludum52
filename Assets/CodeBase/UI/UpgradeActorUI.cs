using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.PersistentProgress;
using CodeBase.Upgrades;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class UpgradeActorUI : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        public List<UpgradeShopCeil> upgradesFisrt;
        public List<UpgradeShopCeil> upgradesSecond;
        public List<UpgradeShopCeil> upgradesThird;
        public List<UpgradeShopCeil> upgradesFifth;

        [SerializeField] private UpgradeButtonUI upgradeButtonUI;

        private UpgradeShopCeil lastItem;
        private Tween _deselectCoroutine;

        private List<List<UpgradeShopCeil>> listGlobal;


        private void Awake()
        {
            upgradeButtonUI.PressedButton += OnClickUpgrade;
            listGlobal = new List<List<UpgradeShopCeil>>();
            listGlobal.Add(upgradesFisrt);
            listGlobal.Add(upgradesSecond);
            listGlobal.Add(upgradesThird);
            listGlobal.Add(upgradesFifth);

            foreach (UpgradeShopCeil upgradeShopCeil in listGlobal.SelectMany(list => list))
            {
                upgradeShopCeil.SelectCeil += OnSelectCeil;
                upgradeShopCeil.DeselectCeil += OnDeselectCeil;
            }

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            foreach (var list in listGlobal)
            {
                bool isLast = false;

                foreach (UpgradeShopCeil upgradeShopCeil in list)
                {
                    if (_progressService.Progress.upgradeList.Contains(upgradeShopCeil.config))
                    {
                        upgradeShopCeil.Buy();
                        continue;
                    }

                    if (isLast)
                    {
                        upgradeShopCeil.Lock();
                        continue;
                    }

                    isLast = true;
                    upgradeShopCeil.SetupIcon();
                }
            }
        }

        private void OnClickUpgrade()
        {
            if (lastItem == null) return;

            if (_progressService.Progress.Points == 0) return;

            upgradeButtonUI.Hide();
            _progressService.Progress.Points -= 1;
            _progressService.Progress.upgradeList.Add(lastItem.config);
            lastItem.Buy();
            UpdateInfo();
        }

        private void OnDeselectCeil(UpgradeShopCeil obj)
        {
            _deselectCoroutine = DOVirtual.DelayedCall(0.1f, () =>
            {
                if (lastItem != null) return;

                upgradeButtonUI.Hide();
            }, false);
        }

        private void OnSelectCeil(UpgradeShopCeil obj)
        {
            _deselectCoroutine?.Kill();
            lastItem = obj;
            upgradeButtonUI.Show();
        }
    }
}