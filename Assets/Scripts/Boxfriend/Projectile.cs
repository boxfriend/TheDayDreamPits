using UnityEngine;

namespace Boxfriend
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _rotationOffset;

        public int RotationOffset => _rotationOffset;
        public int Damage { get; private set; }

        public void Shoot(int damage, Vector2 direction)
        {
            Damage = damage;
            GetComponent<Rigidbody2D>().AddForce(direction,ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.isTrigger)
                return;

            if(collision.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(Damage);

            Destroy(gameObject);
        }
    }
}
