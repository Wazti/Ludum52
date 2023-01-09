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
                return _middleAngle - _castAngle + UfoRotation;
            }
        }

        public float RightAngle
        {
            get
            {
                return _middleAngle + _castAngle + UfoRotation;
            }
        }
        public float CastAngle => _castAngle;
        
        //Cast
        public float Range = 1f;
        public Vector2 LeftStartPos => _startPointLeft.position;
        public Vector2 LeftStartLocalPos => _startPointLeft.localPosition;
        public Vector2 RightStartPos => _startPointRight.position;
        public Vector2 RighStartLocalPos => _startPointRight.localPosition;

        public float UfoRotation => _ufo.eulerAngles.z;
        
        public LayerMask CastMask => _castMask;
        
        
        
        [SerializeField] private float _startRange = 0.25f;
        [SerializeField] private Transform _startPointLeft;
        [SerializeField] private Transform _startPointRight;
        [SerializeField] private Transform _ufo;
        [Header("")]
        [SerializeField] private LayerMask _castMask;
        [Range(0, 360f)]
        [SerializeField] private float _castAngle = 45f;
    }
}