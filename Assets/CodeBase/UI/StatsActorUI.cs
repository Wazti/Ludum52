using System;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class StatsActorUI : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;


        [SerializeField] private TextMeshProUGUI thinText;
        [SerializeField] private TextMeshProUGUI mediumText;
        [SerializeField] private TextMeshProUGUI heavyText;
        [SerializeField] private TextMeshProUGUI cowText;


        private void Awake()
        {
            SetThin(_progressService.Progress.StatisticsUnits.CountThin);
            SetMedium(_progressService.Progress.StatisticsUnits.CountMedium);
            SetHeavy(_progressService.Progress.StatisticsUnits.CountHeavy);
            SetCow(_progressService.Progress.StatisticsUnits.CountCows);
        }

        private void SetThin(int count)
        {
            thinText.text = count.ToString();
        }
        private void SetMedium(int count)
        {
            mediumText.text = count.ToString();
        }
        private void SetHeavy(int count)
        {
            heavyText.text = count.ToString();
        }
        private void SetCow(int count)
        {
            cowText.text = count.ToString();
        }
    }
}