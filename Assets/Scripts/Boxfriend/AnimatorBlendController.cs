using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boxfriend.Player;
using UnityEngine.InputSystem;

namespace Boxfriend
{
    public class AnimatorBlendController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Animator _animator;
        [SerializeField]
        private string _xMovementFloatParameter, _yMovementFloatParameter, _isMovingBoolParameter,
            _previousXMoveParamter, _previousYMoveParamter;

        private Vector2 _velocity;

        private void OnEnable () => _playerController.OnMove += UpdateMove;

        private void OnDisable () => _playerController.OnMove -= UpdateMove;

        private void UpdateMove (Vector2 ctx)
        {
            if (!string.IsNullOrWhiteSpace(_previousXMoveParamter))
                _animator.SetFloat(_previousXMoveParamter, _velocity.x);

            if (!string.IsNullOrWhiteSpace(_previousYMoveParamter))
                _animator.SetFloat(_previousYMoveParamter, _velocity.y);


            _velocity = ctx;

            if (!string.IsNullOrWhiteSpace(_isMovingBoolParameter))
                _animator.SetBool("IsMoving", _velocity != Vector2.zero);

            if (!string.IsNullOrWhiteSpace(_xMovementFloatParameter))
                _animator.SetFloat(_xMovementFloatParameter, _velocity.x);

            if (!string.IsNullOrWhiteSpace(_yMovementFloatParameter))
                _animator.SetFloat(_yMovementFloatParameter, _velocity.y);
        }
    }
}