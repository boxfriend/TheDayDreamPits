using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Boxfriend.Input;

namespace Boxfriend.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownController2D : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField,Range(6,20)] private float _moveSpeed;

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody2d;
#if UNITY_EDITOR
        [SerializeField, Tooltip("Clicking this will reset the rigidbody to my preferred settings. It will then immediately be false again.")]
        private bool _resetRigidbody;
#endif

        private InputActions _inputs;
        private Vector2 _moveDirection;
        private bool _canMove = true;

        public static Action OnStatsChange;

        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = Mathf.Clamp(value, 3, 20);
                OnStatsChange?.Invoke();
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

        private void Awake ()
        {
            _inputs = new InputActions();
            _inputs.TopDown.Move.performed += OnMove;
            _inputs.TopDown.Move.canceled += OnMove;

            _inputs.Enable();
        }

        private void FixedUpdate () => _rigidbody2d.AddForce(_moveDirection * _moveSpeed);

        private void OnMove (InputAction.CallbackContext ctx) => _moveDirection = ctx.ReadValue<Vector2>();
#if UNITY_EDITOR
        private void Reset ()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();

            _moveSpeed = 10;
        }

        private void OnValidate ()
        {
            if (_resetRigidbody)
            {
                ResetRB();
                _resetRigidbody = false;
            }
        }

        [ExecuteInEditMode]
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