using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boxfriend
{
    public class SpawnEnemy : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AIDestinationSetter _enemyPrefab;
        [SerializeField] private Transform _player;

        public void OnPointerClick (PointerEventData eventData)
        {
            var spawnPosition = Camera.main.ScreenToWorldPoint (eventData.position);
            spawnPosition.z = 0;
            var enemy = Instantiate(_enemyPrefab,spawnPosition,Quaternion.identity);
            enemy.target = _player;
        }
    }
}