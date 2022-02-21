using System;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

namespace Boxfriend.Dungeon
{
    public class DungeonManager : MonoBehaviour
    {
        public static event Action OnRoomsLoaded;

        [SerializeField] private GenerationData _generationData;

        [SerializeField] private List<Room> _rooms;

        private readonly Dictionary<Vector2, Room> _loadedRooms = new ();

        private readonly DungeonGenerator _dungeonGen = new ();

        public static DungeonManager Instance { get; private set; }
        public bool RoomsSpawned { get; private set; }

        public void Awake ()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void Start ()
        {
            _dungeonGen.Generate(_generationData);
        }

        private void OnEnable ()
        {
            DungeonGenerator.OnGenerateComplete += OnGenComplete;
        }
        private void OnDisable ()
        {
            DungeonGenerator.OnGenerateComplete -= OnGenComplete;
        }

        private void OnGenComplete (List<RoomInfo> rooms)
        {
            foreach (var room in rooms)
            {
                var roomPos = room.Position * _generationData.RoomOffset;
                var r = Instantiate(this._rooms[room.LayoutID % _rooms.Count], roomPos, Quaternion.identity,transform);
                _loadedRooms.Add(room.Position, r);
                r.gameObject.name = room.ToString();
                r.Info = room;
            }
            RoomsSpawned = true;
            OnRoomsLoaded?.Invoke();
        }

        public bool TryFindRoom (Vector2 pos) => _loadedRooms.ContainsKey(pos);

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
