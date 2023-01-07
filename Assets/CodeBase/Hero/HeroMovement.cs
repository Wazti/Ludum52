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

        [Range(1, 10)] public float HorizontalSpeed = 4f;

        [Range(1, 10)] public float VerticalSpeed = 4f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 axis = _inputService.Axis;

            _rigidbody.velocity = new Vector2(axis.x * HorizontalSpeed, axis.y * VerticalSpeed);
        }
    }
}