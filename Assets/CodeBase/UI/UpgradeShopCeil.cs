using System;
using CodeBase.Upgrades;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class UpgradeShopCeil : MonoBehaviour
    {
        public UpgradeConfig config;

        [SerializeField] private bool isText;

        [SerializeField] private Transform lockTransform;

        [SerializeField] private Image iconImage;

        [SerializeField] private TextMeshProUGUI titleText;

        [SerializeField] private Transform activeBorder;
        [SerializeField] private Transform doneBorder;

        [SerializeField] private Image backImage;


        public event Action<UpgradeShopCeil> SelectCeil, DeselectCeil;

        private bool _selected = false;
        private bool _done = false;
        private bool _lock = false;

        [SerializeField] private Color doneColor;
        [SerializeField] private Color defaultColor;

        private Tween _selectTween;

        private void Awake()
        {
        }

        public void SetupIcon()
        {
            _lock = false;
            _done = false;
            _selected = false;
            iconImage.sprite = config.Icon;
            iconImage.gameObject.SetActive(true);

            lockTransform.gameObject.SetActive(false);

            titleText.text = config.Name;
            titleText.gameObject.SetActive(isText);

            backImage.color = defaultColor;
            activeBorder.gameObject.SetActive(false);
            doneBorder.gameObject.SetActive(false);
        }

        public void Lock()
        {
            SetupIcon();
            _lock = true;
            lockTransform.gameObject.SetActive(true);
            iconImage.gameObject.SetActive(false);
        }

        public void Buy()
        {
            SetupIcon();
            doneBorder.gameObject.SetActive(true);
            backImage.color = doneColor;
            _done = true;
        }


        public void OnHover()
        {
            if (_done || _lock) return;

            activeBorder.gameObject.SetActive(true);
            ScaleActive();
        }

        public void OnUnHover()
        {
            if (_selected) return;

            _selectTween?.Kill();
            activeBorder.gameObject.SetActive(false);
        }

        public void Select()
        {
            if (_done || _lock ) return;

            _selected = true;
            SelectCeil?.Invoke(this);
            ScaleActive();
            activeBorder.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _selected = false;
            _selectTween?.Kill();
            activeBorder.gameObject.SetActive(false);
            DeselectCeil?.Invoke(this);
        }

        private void ScaleActive()
        {
            _selectTween?.Kill();
            activeBorder.localScale = Vector3.one;
            _selectTween = activeBorder.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}