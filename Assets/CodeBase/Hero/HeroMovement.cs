using System;
using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroMovement : MonoBehaviour
    {
        [Inject] private IInputService _inputService;

        [SerializeField] private HeroBoost heroBoost;
        
        public HeroWeight heroWeight;

        [Range(1, 10)] public float HorizontalSpeed = 4f;

        [Range(1, 10)] public float VerticalSpeed = 4f;

        [Range(0, 40f)] public float Enercia;

        private Rigidbody2D _rigidbody;

        private Vector2 _previousAxis = Vector2.zero;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 axis = _inputService.Axis;

            _rigidbody.velocity = new Vector2(axis.x * HorizontalSpeed * heroBoost.BoostValue(), GetVelocityY(axis.y));

            _previousAxis = axis;
        }

        private float GetVelocityY(float axisY)
        {
            if (heroWeight.IsOverWeight)
                return Math.Min(_rigidbody.velocity.y + axisY * VerticalSpeed * Time.deltaTime, axisY * VerticalSpeed);

            return axisY * VerticalSpeed;
        }
    }
}