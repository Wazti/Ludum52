using System;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class PointsActorUI : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        [SerializeField] private TextMeshProUGUI textPoints;

        private void Awake()
        {
            _progressService.Progress.ChangePoints += SetPoints;
            SetPoints();
        }

        private void SetPoints()
        {
            textPoints.text = $"{_progressService.Progress.Points} points left";
        }
    }
}