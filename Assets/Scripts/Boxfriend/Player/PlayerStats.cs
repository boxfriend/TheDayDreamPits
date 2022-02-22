using System;
using UnityEngine;

namespace Boxfriend.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerStats : MonoBehaviour, IDamageable, IKillable, IHealable
    {
        [SerializeField] private int _baseHealth;

        private PlayerMovement _movement;

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

        private void Awake ()
        {
            _movement = GetComponent<PlayerMovement>();
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