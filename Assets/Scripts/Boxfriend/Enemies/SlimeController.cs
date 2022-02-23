using UnityEngine;
using Pathfinding;
using Boxfriend.Player;

namespace Boxfriend.Enemies
{
    public class SlimeController : MonoBehaviour, IKillable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AIPath _path;
        private void OnEnable () => PlayerStats.OnPlayerDeath += Kill;

        private void OnDisable () => PlayerStats.OnPlayerDeath -= Kill;

        public void Kill()
        {
            _animator.SetTrigger("Death");
            _path.canMove = false;

            if(TryGetComponent(out Collider2D collider))
                collider.enabled = false;
        }

        private void Reset ()
        {
            _path = GetComponentInParent<AIPath>();
            _animator = GetComponent<Animator>();
        }
    }
}
