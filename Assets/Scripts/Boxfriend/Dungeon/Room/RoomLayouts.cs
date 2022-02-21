using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend.Dungeon
{
    [CreateAssetMenu(menuName = "RoomLayouts.asset",fileName ="SOs/RoomLayouts")]
    public class RoomLayouts : ScriptableObject
    {
        [SerializeField] private List<Room> _basicRooms;
        [SerializeField] private List<Room> _itemRooms;
        [SerializeField] private List<Room> _bossRooms;

        public List<Room> BasicRooms => _basicRooms;
        public List<Room> ItemRooms => _itemRooms;
        public List<Room> BossRooms => _bossRooms;
    }
}
