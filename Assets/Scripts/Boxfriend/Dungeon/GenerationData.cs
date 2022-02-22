using UnityEngine;

namespace Boxfriend.Dungeon
{
    [System.Serializable,CreateAssetMenu(fileName = "LevelData.asset", menuName = "SOs/LevelData")]
    public class GenerationData : ScriptableObject
    {
        [SerializeField,Range(1,10)] private int _numberOfCrawlers;
        [SerializeField,Range(5,30)] private int _maxBasicRooms;
        [SerializeField, Range(1, 5)] private int _maxFakeBossRooms;
        [SerializeField, Range(1, 5)] private int _maxBossRooms;
        [SerializeField] private Vector2 _roomOffset;

        private int _basicRooms;
        private int _fakeBossRooms;
        private int _bossRooms;

        public int TotalRooms => BasicRooms + FakeBossRooms + BossRooms;
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
        public int FakeBossRooms
        {
            get
            {
                Debug.Assert(_fakeBossRooms > 0, "You must initialize the Generation Data using the InitializeData() method.");
                return _fakeBossRooms;
            }
            private set => _fakeBossRooms = value;
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
            FakeBossRooms = rng.Next(1, _maxFakeBossRooms+1);
            BossRooms = rng.Next(1, _maxBossRooms+1);
        }
    }
}
