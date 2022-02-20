using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Boxfriend.Input;

namespace Boxfriend.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SideScrollerController2D : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField, Range(6, 20),Tooltip("The multiplier applied to the movement force. Lower than 6 is too slow")] 
        private float _moveSpeed;

        [SerializeField, Range(6, 20),Tooltip("Amount of power for a jump, same as Move Speed usually feels decent")] 
        private float _jumpPower;

        [SerializeField, Range(0.5f,10),Tooltip("Gravity Scale when player is considered falling. Higher = Fall Faster")] 
        private float _fallForce;

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody2d;
#if UNITY_EDITOR
        [SerializeField,Tooltip("Clicking this will reset the rigidbody to my preferred settings. It will then immediately be false again.")] 
        private bool _resetRigidbody;
#endif
        [Header("Ground Check")]
        [SerializeField] private Vector3 _groundCheckOffset;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _groundCheckRadius;

        private InputActions _inputs;
        private float _moveDirection;
        private bool _canMove = true;

        public static Action OnStatsChange;

        /// <summary>
        /// Returns <see langword="true"/> when the player is considered 'Grounded', only updated on physics steps
        /// </summary>
        public bool IsGrounded { get; private set; }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = Mathf.Clamp(value, 3,20);
                OnStatsChange?.Invoke();
            }
        }

        public float JumpPower
        { 
            get => _jumpPower; 
            set 
            { 
                _jumpPower = Mathf.Clamp(value, 3, 20); 
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
            _inputs.SideScroller.Move.performed += OnMove;
            _inputs.SideScroller.Move.canceled += OnMove;

            _inputs.SideScroller.Jump.performed += OnJump;

            _inputs.Enable();
        }

        private void FixedUpdate ()
        {
            _rigidbody2d.AddForce(new Vector2(_moveDirection * _moveSpeed,0));

            IsGrounded = Physics2D.OverlapCircle(transform.position + _groundCheckOffset, _groundCheckRadius, _groundMask);

            _rigidbody2d.gravityScale = _rigidbody2d.velocity.y <= 0 && !IsGrounded ? _fallForce : 1;
        }

        private void OnMove (InputAction.CallbackContext ctx) => _moveDirection = ctx.ReadValue<float>();

        private void OnJump (InputAction.CallbackContext ctx)
        {
            if (IsGrounded)
                _rigidbody2d.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
        }

#if UNITY_EDITOR
        private void Reset ()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();

            _moveSpeed = 10;
            _jumpPower = 10;
            _fallForce = 5;

            _groundCheckOffset = new Vector3(0, -0.6f);
            _groundCheckRadius = 0.5f;
        }

        private void OnValidate ()
        {
            if(_resetRigidbody)
            {
                ResetRB();
                _resetRigidbody = false;
            }
        }

        [ExecuteInEditMode]
        private void ResetRB()
        {
            _rigidbody2d.gravityScale = 1;
            _rigidbody2d.drag = 6;
            _rigidbody2d.mass = 0.6f;
            _rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (_rigidbody2d.sharedMaterial == null)
            {
                var material = new PhysicsMaterial2D { friction = 0, bounciness = 0 };
                _rigidbody2d.sharedMaterial = material;
            }
        }
#endif
    }
}