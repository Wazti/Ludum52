using System;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class DayActor : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        [SerializeField] private TextMeshProUGUI text;
        
        private void Awake()
        {
            text.text = $"Day {_progressService.Progress.Day}";
        }
    }
}