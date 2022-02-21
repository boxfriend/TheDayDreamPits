using UnityEngine;

namespace Boxfriend.Dungeon
{
    [System.Serializable,CreateAssetMenu(fileName = "LevelData.asset", menuName = "SOs/LevelData")]
    public class GenerationData : ScriptableObject
    {
        [SerializeField,Range(1,10)] private int _numberOfCrawlers;
        [SerializeField,Range(5,30)] private int _maxBasicRooms;
        [SerializeField, Range(1, 5)] private int _maxItemRooms;
        [SerializeField, Range(1, 5)] private int _maxBossRooms;
        [SerializeField] private Vector2 _roomOffset;

        private int _basicRooms;
        private int _itemRooms;
        private int _bossRooms;

        public int TotalRooms => BasicRooms + ItemRooms + BossRooms;
        public int Crawlers => _numberOfCrawlers;
        public int BasicRooms 
        { 
            get
            {
                Debug.Assert(_basicRooms > 0,"You must initialize the Generation Data using the InitializeData() method.");
                return _basicRooms;
            }
            private set => _basicRooms = value;
        }
        public int ItemRooms
        {
            get
            {
                Debug.Assert(_itemRooms > 0, "You must initialize the Generation Data using the InitializeData() method.");
                return _itemRooms;
            }
            private set => _itemRooms = value;
        }
        public int BossRooms
        {
            get
            {
                Debug.Assert(_bossRooms > 0, "You must initialize the Generation Data using the InitializeData() method.");
                return _bossRooms;
            }
            private set => _bossRooms = value;
        }
        public Vector2 RoomOffset => _roomOffset;

        public void InitializeData(System.Random rng)
        {
            BasicRooms = rng.Next(5, _maxBasicRooms+1);
            ItemRooms = rng.Next(1, _maxItemRooms+1);
            BossRooms = rng.Next(1, _maxBossRooms+1);
        }
    }
}
