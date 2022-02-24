using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private Item _spawnItem;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private bool _isEmpty;

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _animator.SetBool("IsEmpty", _isEmpty);
                _animator.SetTrigger("Open");
            }
        }

        public void Spawn ()
        {
            if (_spawnItem != null)
                Instantiate(_spawnItem, _spawnPosition.position, Quaternion.identity);

            Destroy(this);
        }
    }
}