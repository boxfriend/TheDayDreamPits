using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend.Enemies
{
    public class EnemyStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _baseHealth;
        [SerializeField] private int _baseSpeed;
        [SerializeField] private int _baseDamage;

        private int _health;
        public int Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, _baseHealth);
                if (Health <= 0 && TryGetComponent(out IKillable kill))
                    kill.Kill();
            }
        }
        public int Damage => _baseDamage;
        public void TakeDamage (int damage) => Health -= damage;

        void Awake ()
        {
            Health = _baseHealth;
            GetComponent<AIPath>().maxSpeed = _baseSpeed;
        }
    }
}