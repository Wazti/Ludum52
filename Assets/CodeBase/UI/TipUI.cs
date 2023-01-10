using System;
using CodeBase.Services.PersistentProgress;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class TipUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tip;
        [Inject] private IPersistentProgressService _progressService;

        private void Awake()
        {
            if (_progressService.Progress.Day > 1) return;

            tip.enabled = true;

            tip.DOFade(0, 1f).SetDelay(2f).SetTarget(gameObject);
        }
    }
}