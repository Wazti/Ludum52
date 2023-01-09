using CodeBase.Stats.Scriptables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Upgrades
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Config")]
    public class UpgradeConfig : ScriptableObject
    {
        public int id;

        public string Name;

        [PreviewField]
        public Sprite Icon;

        public StatType statUpgrade;

        public float statValue;
    }
}