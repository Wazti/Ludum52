using System;
using CodeBase.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class CountUnitsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [Inject] private ILevelSessionService _levelSessionService;


        private void Awake()
        {
            _levelSessionService.CountChange += OnCountChange;
            OnCountChange(0);
        }

        private void OnDestroy()
        {
            _levelSessionService.CountChange -= OnCountChange;
        }

        private void OnCountChange(int obj)
        {
            text.text = obj.ToString();
        }
    }
}