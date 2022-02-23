using System;
using UnityEngine;

namespace Boxfriend.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerStats : MonoBehaviour, IDamageable, IKillable, IHealable
    {
        [SerializeField] private int _baseHealth;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private int _attackDamage;

        private PlayerController _movement;

        private int _health;

        public static event Action<PlayerStats> OnStatsChange;
        public static event Action OnPlayerDeath;

        public int BaseHealth => _baseHealth;
        public int Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, _baseHealth);

                if(_health <= 0) Kill();

                OnStatsChange?.Invoke(this);
            }
        }

        public float MoveSpeed
        {
            get => _movement.MoveSpeed;
            private set 
            { 
                _movement.MoveSpeed = value; 
                OnStatsChange.Invoke(this);
            }
        }

        public float AttackCooldown => _attackCooldown;
        public int Damage => _attackDamage;
        public float AttackSpeed => _attackSpeed;

        private void Awake ()
        {
            _movement = GetComponent<PlayerController>();
            Health = _baseHealth;
        }

        public void TakeDamage (int damage) => Health -= damage;

        public void Kill()
        {
            _movement.CanMove = false;
            OnPlayerDeath?.Invoke();
        }

        public bool Heal (int amount)
        {
            if (Health >= _baseHealth)
                return false;

            Health += amount;
            return true;
        }
    }
}