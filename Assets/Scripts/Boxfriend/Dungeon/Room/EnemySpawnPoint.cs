using Pathfinding;
using UnityEngine;
using Boxfriend.Player;

namespace Boxfriend.Dungeon
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private Room _currentRoom;
        [SerializeField] private AIDestinationSetter _enemyToSpawn;

        private void OnEnable () => Room.OnRoomEntered += Spawn;

        private void OnDisable () => Room.OnRoomEntered -= Spawn;

        private void Spawn(Room room)
        {
            if (room != _currentRoom)
                return;

            var enemy = Instantiate(_enemyToSpawn, transform.position, Quaternion.identity);
            enemy.target = GameObject.FindObjectOfType<PlayerController>().transform; //TODO: get a not shit way to reference the player
            Destroy(gameObject);
        }

        private void Reset ()
        {
            _currentRoom = GetComponentInParent<Room>();
        }
    }
}
