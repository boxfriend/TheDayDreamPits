using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

namespace Boxfriend.Dungeon
{
	public class Room : MonoBehaviour
	{
		
		private static readonly Dictionary<DoorType, Vector2Int> _direction = new () {
			[DoorType.UP] = Vector2Int.up, 
			[DoorType.DOWN] = Vector2Int.down, 
			[DoorType.LEFT] = Vector2Int.left, 
			[DoorType.RIGHT] = Vector2Int.right
		};
		
		[SerializeField] private RoomInfo _info;
		public RoomInfo Info { get => _info; set => _info = value; }
		
		public RoomType Type => Info.Type;
		

		[SerializeField] private List<Door> _doors;
		[SerializeField] private List<Door> _walls;
		[SerializeField] private CinemachineVirtualCamera _vcam;

		public void OnEnable ()
		{
			DungeonManager.OnRoomsLoaded += RemoveUnusedDoors;
		}
		private void OnDisable ()
		{
			DungeonManager.OnRoomsLoaded -= RemoveUnusedDoors;
		}

		private void RemoveUnusedDoors ()
		{
			foreach (var (key, value) in _direction)
			{
				var roomExists = DungeonManager.Instance.TryFindRoom(Info.Position + value);
				
				_doors.Find(item => item.Type == key).gameObject.SetActive(roomExists);
				_walls.Find(item => item.Type == key).gameObject.SetActive(!roomExists);
			}
		}

		private void OnTriggerEnter2D (Collider2D col)
		{
			if (col.CompareTag("Player"))
			{
				_vcam.enabled = true;
			}
		}

		private void OnTriggerExit2D (Collider2D col)
		{
			if (col.CompareTag("Player"))
			{
				_vcam.enabled = false;
			}
		}
	}
}
