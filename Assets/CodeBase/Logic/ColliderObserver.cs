using System;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderObserver : MonoBehaviour
    {
        public event Action<Collision2D> ColliderEnter;
        public event Action<Collision2D> CollideExit;

        private void OnCollisionEnter2D(Collision2D col) => ColliderEnter?.Invoke(col);
        private void OnCollisionExit2D(Collision2D col) => CollideExit?.Invoke(col);
    }
}