using UnityEngine;
using UnityEngine.InputSystem;
using Boxfriend.Input;
using System.Collections;

namespace Boxfriend.Player
{
    internal class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile; //should probably pool this, but honestly it isn't necessary

        private Vector2 _attackDirection;

        private bool _isCooldown;
        private int _damage;
        private float _attackSpeed;

        private WaitForSeconds _delay;

        private void Awake ()
        {
            GetComponent<PlayerController>().OnAttack += Attack;

            var stats = GetComponent<PlayerStats>();
            _delay = new WaitForSeconds(stats.AttackCooldown);
            _damage = stats.Damage;
            _attackSpeed = stats.AttackSpeed;
        }
        public void Attack (InputAction.CallbackContext ctx) => _attackDirection = ctx.ReadValue<Vector2>();

        private void FixedUpdate ()
        {
            if (_isCooldown || _attackDirection == Vector2.zero)
                return;

            Shoot();
            StartCoroutine(AttackCooldown());
        }

        private void Shoot()
        {
            var direction = (Mathf.Atan2(_attackDirection.y, _attackDirection.x) * Mathf.Rad2Deg) + _projectile.RotationOffset;
            var projectile = Instantiate(_projectile, transform.position, Quaternion.Euler(0, 0, direction));
            projectile.gameObject.layer = gameObject.layer;
            projectile.Shoot(_damage, _attackDirection * _attackSpeed);
        }

        private IEnumerator AttackCooldown()
        {
            _isCooldown = true;
            yield return _delay;
            _isCooldown = false;
        }
    }
}
