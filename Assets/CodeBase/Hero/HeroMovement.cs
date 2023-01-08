using System;
using CodeBase.Services.InputService;
using Sirenix.OdinInspector;
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

        [Range(1, 10)] public float UpSpeed = 3f;


        [MinMaxSlider(0, 5f, true)] public Vector2 DownSpeed;

        [SerializeField] private float pointGround = -3.874f;
        [SerializeField] private float pointSky = 5f;

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
                return Math.Min(_rigidbody.velocity.y + axisY * GetVerticalSpeed(axisY) * Time.deltaTime,
                    axisY * UpSpeed);

            return axisY * GetVerticalSpeed(axisY);
        }

        private float GetVerticalSpeed(float axisY)
        {
            if (axisY > 0) return UpSpeed;

            var posY = transform.position.y;

            if (posY < pointGround) return DownSpeed.x;

            if (posY > pointSky) return DownSpeed.y;

            return DownSpeed.x + ((posY - pointGround) / (pointSky - pointGround)) * (DownSpeed.y - DownSpeed.x);
        }
    }
}