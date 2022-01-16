using System;
using UnityEngine;

namespace Chanhyeong
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShapeVelocity : MonoBehaviour
    {
        public float maxVelocity = 5f;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rigidbody.angularDrag = _rigidbody.drag;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
            _rigidbody.angularVelocity = velocity;
        }

        public void SetVelocity(Vector3 startPoint, Vector3 endPoint)
        {
            var velocity = Vector3.ClampMagnitude(startPoint - endPoint, maxVelocity);
            SetVelocity(velocity);
        }

        public bool IsVelocityZero() => _rigidbody.velocity.magnitude == 0;
    }
}