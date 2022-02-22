using System;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using Boxfriend.Utils;

namespace Boxfriend.Dungeon
{
    public class DungeonManager : SingletonBehaviour<DungeonManager>
    {
        public static event Action OnRoomsLoaded;

        [SerializeField] private GenerationData _generationData;

        [SerializeField] private RoomLayouts _rooms;

        private readonly Dictionary<Vector2Int, Room> _loadedRooms = new ();

        private readonly DungeonGenerator _dungeonGen = new ();

        public bool RoomsSpawned { get; private set; }

        public void Start () => _dungeonGen.Generate(_generationData);
        private void OnEnable () => DungeonGenerator.OnGenerateComplete += OnGenComplete;
        private void OnDisable () => DungeonGenerator.OnGenerateComplete -= OnGenComplete;

        private void OnGenComplete (List<RoomInfo> rooms)
        {
            foreach (var room in rooms)
            {
                var roomPos = room.Position * _generationData.RoomOffset;
                var layout = GetLayout(room);
                var r = Instantiate(layout, roomPos, Quaternion.identity,transform);
                _loadedRooms.Add(room.Position, r);
                r.gameObject.name = room.ToString();
                r.Info = room;
            }
            RoomsSpawned = true;
            OnRoomsLoaded?.Invoke();
        }

        private Room GetLayout(RoomInfo info)
        {
            return info.Type switch
            {
                RoomType.Basic => _rooms.BasicRooms[info.LayoutID % _rooms.BasicRooms.Count],
                RoomType.Boss => _rooms.BossRooms[info.LayoutID % _rooms.BossRooms.Count],
                RoomType.FakeBoss => _rooms.ItemRooms[info.LayoutID % _rooms.ItemRooms.Count],
                _ => throw new NotImplementedException()
            };
        }

        public bool TryFindRoom (Vector2Int pos) => _loadedRooms.ContainsKey(pos);

#if !ODIN_INSPECTOR
        [SerializeField] private bool _resetDungeon;
        private void OnValidate()
        {
            if (_resetDungeon && Application.isPlaying)
            {
                ResetDungeon();
            }
            _resetDungeon = false;
        }
#else
        [Sirenix.OdinInspector.Button]
#endif
        private void ResetDungeon()
        {
            foreach (var room in _loadedRooms)
                Destroy(room.Value.gameObject);

            _loadedRooms.Clear();
            _dungeonGen.Generate(_generationData);
        }
    }
}
