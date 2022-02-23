using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Boxfriend.Input;

namespace Boxfriend.Player
{
    
    public class TopDownController2D : PlayerController
    {
        private void FixedUpdate () => _rigidbody2d.AddForce(_moveDirection * _moveSpeed);

#if UNITY_EDITOR
        private void Reset ()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();

            _moveSpeed = 10;
        }
#if !ODIN_INSPECTOR
        private void OnValidate ()
        {
            if (_resetRigidbody)
            {
                ResetRB();
                _resetRigidbody = false;
            }
        }
#else
        [Sirenix.OdinInspector.Button]
#endif
        private void ResetRB ()
        {
            _rigidbody2d.gravityScale = 0;
            _rigidbody2d.drag = 6;
            _rigidbody2d.mass = 0.6f;

            if (_rigidbody2d.sharedMaterial == null)
            {
                var material = new PhysicsMaterial2D { friction = 0, bounciness = 0 };
                _rigidbody2d.sharedMaterial = material;
            }
        }
#endif
    }
}