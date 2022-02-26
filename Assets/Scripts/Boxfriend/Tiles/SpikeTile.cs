using System.Collections;
using UnityEngine;

namespace Boxfriend.Tiles
{
    public class SpikeTile : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private bool _isActive;

        private void Start () => StartCoroutine(SpikeDelay());

        private IEnumerator SpikeDelay()
        {
            var delay = Random.Range(2f, 10f);
            yield return new WaitForSeconds(delay);
            ToggleActive();
        }

        public void ToggleActive()
        {
            _isActive = !_isActive;
            _animator.SetBool("Active", _isActive);
            StartCoroutine(SpikeDelay());
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if(collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);
            }
        }
    }
}
