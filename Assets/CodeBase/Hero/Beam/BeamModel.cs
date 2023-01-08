using UnityEngine;

namespace CodeBase.Hero.Beam
{
    public class BeamModel : MonoBehaviour
    {
        
        //Angles
        private float _middleAngle = 270f;
        public float LeftAngle
        {
            get
            {
                return _middleAngle - _castAngle;
            }
        }

        public float RightAngle
        {
            get
            {
                return _middleAngle + _castAngle;
            }
        }
        public float CastAngle => _castAngle;
        
        //Cast
        public Vector2 LeftStartPos
        {
            get
            {
                var pos = StartPos;
                pos.x -= _startRange;
                return pos;
            }
        }
        
        public Vector2 RightStartPos
        {
            get
            {
                var pos = StartPos;
                pos.x += _startRange;
                return pos;
            }
        }
        public Vector2 StartPos
        {
            get
            {
                return _startPoint.position;
            }
        }
        public LayerMask CastMask => _castMask;
        
        
        [SerializeField] private float _startRange = 0.25f;
        [SerializeField] private Transform _startPoint;
        [Header("")]
        [SerializeField] private LayerMask _castMask;
        [Range(0, 360f)]
        [SerializeField] private float _castAngle = 45f;
    }
}