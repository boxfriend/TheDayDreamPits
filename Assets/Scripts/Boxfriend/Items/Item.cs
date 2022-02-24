using System;
using UnityEngine;

namespace Boxfriend
{
    public class Item : MonoBehaviour
    {
        public static Action<Item> OnItemPickup;

        protected virtual void OnTriggerEnter2D (Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                OnItemPickup?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}