using UnityEngine;

namespace Boxfriend.Items
{
    public class HealthItem : Item
    {
        [SerializeField] private int _healAmount;
        protected override void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.TryGetComponent(out IHealable heal))
            {
                if(heal.Heal(_healAmount))
                    Destroy(gameObject);
            }
        }
    }
}