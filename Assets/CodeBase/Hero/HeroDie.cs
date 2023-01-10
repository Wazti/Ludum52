using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDie : MonoBehaviour
    {
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        public void Die()
        {
            _rigidbody2D.gravityScale = 1;
            _heroMovement.enabled = false;
        }
    }
}