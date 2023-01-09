using System;
using System.Collections.Generic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Unit;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class StatsActorUI : SerializedMonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        [SerializeField] private Dictionary<UnitType, TextMeshProUGUI> texts;


        private void Awake()
        {
            foreach (var keys in texts.Keys)
            {
                SetText(texts[keys], _progressService.Progress.StatisticsUnits.countUnits[keys]);
            }
        }

        private void SetText(TextMeshProUGUI text, int count)
        {
            text.text = count.ToString();
        }
    }
}