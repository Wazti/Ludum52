using System;
using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroRotate : MonoBehaviour
    {
        [Inject] private IInputService _inputService;

        [SerializeField] private Transform viewTransform;

        [SerializeField] private float speed;

        [SerializeField] private float maxDefaultAngle;

        private void Update()
        {
            LerpAngle(_inputService.Axis.x);
        }

        private void LerpAngle(float axis)
        {
            float angle = viewTransform.localEulerAngles.z;
            angle = (angle > 180) ? angle - 360 : angle;

            viewTransform.localEulerAngles = new Vector3(0, 0,
                Mathf.Lerp(angle, maxDefaultAngle * (-axis), Time.deltaTime * speed));
        }
    }
}