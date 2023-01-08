using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Buildings
{
    public class BuildingWindows : MonoBehaviour
    {
        public List<BuildingIntake> activeWindows = new List<BuildingIntake>();

        [SerializeField] private BuildingTint buildingTint;

        private bool IsZone => buildingTint.IsActive;

        public bool IsActive => activeWindows.Count > 0 && IsZone;

        public event Action ActiveWindowChange;

        private void Awake()
        {
            GetComponentsInChildren<BuildingIntake>().ForEach((item) =>
            {
                item.IntakeEnter += AddWindowActive;
                item.IntakeExit += RemoveWindowActive;
            });
        }

        private void RemoveWindowActive(BuildingIntake window)
        {
            if (!activeWindows.Contains(window)) return;

            ActiveWindowChange?.Invoke();
            activeWindows.Remove(window);
        }

        private void AddWindowActive(BuildingIntake window)
        {
            if (activeWindows.Contains(window)) return;

            activeWindows.Add(window);
            ActiveWindowChange?.Invoke();
        }

        public BuildingIntake GetRandomWindow() => activeWindows[Random.Range(0, activeWindows.Count)];
    }
}