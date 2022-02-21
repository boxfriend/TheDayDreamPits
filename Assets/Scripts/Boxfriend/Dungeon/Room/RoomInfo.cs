using System;
using UnityEngine;

namespace Boxfriend.Dungeon
{
	public enum RoomType
	{
		Basic,
		Item,
		Boss,
		Secret
	}
	
	[Serializable]
	public struct RoomInfo
	{
		public Vector2Int Position { get; }
		public RoomType Type { get; }
		public byte LayoutID { get; }
		public int ID { get; }

		public RoomInfo (Vector2Int position, RoomType type, byte layoutID, int roomID)
		{
			Position = position;
			Type = type;
			LayoutID = layoutID;
			ID = roomID;
		}

        public override string ToString () => $"Room {ID}: {Position} {Type}:{LayoutID}";
    }
}
