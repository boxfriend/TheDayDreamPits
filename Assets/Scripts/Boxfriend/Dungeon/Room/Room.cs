using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

namespace Boxfriend.Dungeon
{
	public class Room : MonoBehaviour
	{
		
		private static Dictionary<DoorType, Vector2> _direction = new Dictionary<DoorType, Vector2>() {
			[DoorType.UP] = Vector2.up, 
			[DoorType.DOWN] = Vector2.down, 
			[DoorType.LEFT] = Vector2.left, 
			[DoorType.RIGHT] = Vector2.right
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
				if (!DungeonManager.Instance.TryFindRoom(Info.Position + value))
					continue;
				
				_doors.Find(item => item.Type == key).gameObject.SetActive(true);
				_walls.Find(item => item.Type == key).gameObject.SetActive(false);
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
