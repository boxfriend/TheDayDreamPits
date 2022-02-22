using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Boxfriend.Input;

namespace Boxfriend.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField, Range(6, 20)] protected float _moveSpeed;

        [Header("Components")]
        [SerializeField] protected Rigidbody2D _rigidbody2d;
#if UNITY_EDITOR && !ODIN_INSPECTOR
        [SerializeField, Tooltip("Clicking this will reset the rigidbody to my preferred settings. It will then immediately be false again.")]
        private bool _resetRigidbody;
#endif

        protected InputActions _inputs;
        protected Vector2 _moveDirection;
        protected bool _canMove = true;

        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = Mathf.Clamp(value, 3, 20);
            }
        }

        public bool CanMove
        {
            get => _canMove;
            set
            {
                _canMove = value;

                if (_canMove)
                    _inputs.Enable();
                else
                    _inputs.Disable();
            }
        }

        protected virtual void Awake ()
        {
            _inputs = new InputActions();
            _inputs.TopDown.Move.performed += OnMove;
            _inputs.TopDown.Move.canceled += OnMove;


            _inputs.Enable();
        }

        private void OnMove (InputAction.CallbackContext ctx) => _moveDirection = ctx.ReadValue<Vector2>();
    }
}
