using System;
using Chanhyeong.Decorate;
using Chanhyeong.Enum;
using UnityEngine;

namespace Chanhyeong
{
    [RequireComponent(typeof(ShapeController), typeof(ShapeVelocity))]
    public class ShapeObject : MonoBehaviour, IScore
    {
        public ShapeType CurrentShape { get; set; }

        private ShapeController _shapeController;
        private ShapeVelocity _shapeVelocity;
        
        public event Action<int> OnScorePoint;
        public int Score { get; set; }

        private void Awake()
        {
            _shapeController = GetComponent<ShapeController>();
            _shapeVelocity = GetComponent<ShapeVelocity>();
        }

        private void OnEnable()
        {
            _shapeController.onEndDrag += _shapeVelocity.SetVelocity;
            _shapeController.onInteractCondition += _shapeVelocity.IsVelocityZero;
        }

        private void OnDisable()
        {
            _shapeController.onEndDrag -= _shapeVelocity.SetVelocity;
            _shapeController.onInteractCondition -= _shapeVelocity.IsVelocityZero;
        }

        public void OnScore()
        {
            OnScorePoint?.Invoke(Score);     
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                
            }
        }
    }
}