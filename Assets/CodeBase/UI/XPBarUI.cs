using System;
using System.Collections.Generic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class XPBarUI : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        [Inject] private ILevelSessionService _levelSessionService;

        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField] private TextMeshProUGUI _progressText;

        [SerializeField] private List<Image> images;

        private int lastLevel;
        private int lastXp;
        private int lastProgress;

        private int currentLevel;
        private int currentXp;
        private int currentProgress;

        private void Awake()
        {
            SetupPrevious();
            SetupData();
            AnimateXPBar();
        }

        private void SetupPrevious()
        {
            lastLevel = _levelSessionService.levelOld;
            lastXp = _levelSessionService.xpOld;
            lastProgress = (int) Math.Ceiling(_levelSessionService.progressOld * 100);
            SetCurrentLevel(lastLevel);
            CalculateSlices(lastProgress);
        }

        private void CalculateSlices(int progress)
        {
            _progressText.text = $"{progress}%";

            for (int i = 0; i < images.Count; i++)
            {
                images[i].fillAmount = (progress > ((i + 1) * 20) ? 1 : Math.Max(0, progress - i * 20) / 20f);
            }
        }

        private void SetupData()
        {
            currentLevel = _progressService.Progress.LevelHero.CurrentLevel;
            currentXp = _progressService.Progress.LevelHero.xp;
            currentProgress = (int) Math.Ceiling(_progressService.Progress.LevelHero.Progress() * 100);
        }

        private void SetCurrentLevel(int level)
        {
            levelText.text = $"LVL.{level}";
        }

        private void AnimateXPBar()
        {
            var seq = DOTween.Sequence().SetDelay(.4f);

            if (currentLevel == lastLevel)
            {
                seq.Append(DOVirtual.Int(lastProgress, currentProgress, 2f, CalculateSlices));
                return;
            }

            var diff = currentLevel - lastLevel;
            for (int i = 0; i < diff; i++)
            {
                var from = lastProgress;
                if (i != 0)
                {
                    from = 0;
                }

                seq.Append(DOVirtual.Int(from, 100, 2f, CalculateSlices).OnComplete(() =>
                {
                    SetCurrentLevel(lastLevel + 1);
                    lastLevel += 1;
                    CalculateSlices(0);
                }));
            }

            seq.Append(DOVirtual.Int(0, currentProgress, 2f, CalculateSlices));
        }
    }
}