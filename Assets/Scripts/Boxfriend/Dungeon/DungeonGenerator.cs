using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Boxfriend.Dungeon
{
	public class DungeonGenerator
	{
		public static event Action<List<RoomInfo>> OnGenerateComplete;
		
		private enum Direction
		{
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3
		}
		
		private readonly Dictionary<Direction, Vector2> Directions = new Dictionary<Direction, Vector2>()
		{
			[Direction.Up] = Vector2.up,
			[Direction.Down] = Vector2.down,
			[Direction.Right] = Vector2.right,
			[Direction.Left] = Vector2.left
		};

		private readonly List<Vector2> _visitedPositions = new ();
		private readonly List<RoomInfo> _generatedRooms = new ();
		private readonly List<DungeonCrawler> _crawlers = new ();
		private Random _rng;

		public void Generate (GenerationData data)
		{
			_crawlers.Clear();
			_visitedPositions.Clear();
			_generatedRooms.Clear();
			
			_rng = new Random();
			data.InitializeData(_rng);

			SpawnCrawlers(data.Crawlers);

			var startingRoom = new RoomInfo(Vector2.zero, RoomType.Basic, 0, 0);
			_generatedRooms.Add(startingRoom);
			_visitedPositions.Add(Vector2.zero);
			Debug.Log(startingRoom);
			
			GenerateBasicRooms(data.BasicRooms);
			for (var i = 0; i < data.ItemRooms; i++)
			{
				GenerateSpecialRooms(RoomType.Item);
			}
			for (var i = 0; i < data.BossRooms; i++)
			{
				GenerateSpecialRooms(RoomType.Boss);
			}
			
			GenerationComplete(data);
		}

		private void GenerateBasicRooms (int roomsNumber)
		{
			while(_visitedPositions.Count < roomsNumber)
			{
				foreach (var crawler in _crawlers)
				{
					var position = crawler.NextPosition(GetDirection());
					AddRoomPosition(position, RoomType.Basic);

					if (_visitedPositions.Count >= roomsNumber)
						break;
				}
			}
		}
		private bool AddRoomPosition (Vector2 position, RoomType type)
		{
			if (_visitedPositions.Contains(position) || NumberOfNeighbors(position) >= 3 || NumberOfNeighbors(position) <= 0)
			{
				return false;
			}
			
			_visitedPositions.Add(position);
			var room = new RoomInfo(position, type, (byte)_rng.Next(0, byte.MaxValue), _generatedRooms.Count);
			_generatedRooms.Add(room);
			Debug.Log(room);
			return true;
		}

		private void GenerateSpecialRooms (RoomType type)
		{
			var tempPosition = _visitedPositions[_rng.Next(0, _visitedPositions.Count)];
			tempPosition += GetDirection();

			if (!AddRoomPosition(tempPosition, type))
			{
				GenerateSpecialRooms(type);
			}
		}

        private Vector2 GetDirection () => Directions[(Direction)_rng.Next(0, Directions.Count)];

        private void SpawnCrawlers (int crawlersAmount)
		{
			for (var i = 0; i < crawlersAmount; i++)
			{
				_crawlers.Add(new DungeonCrawler());
			}
		}

		private int NumberOfNeighbors (Vector2 position)
		{
			var neighbors = 0;
			foreach (var dir in Directions)
			{
				var check = position + dir.Value;
				if (_visitedPositions.Contains(check))
					neighbors++;
			}

			return neighbors;
		}

		private void GenerationComplete (GenerationData data)
		{
			Debug.Log($"Generated {data.TotalRooms} rooms. Basic:{data.BasicRooms} Item:{data.ItemRooms} Boss:{data.BossRooms}");
			
			OnGenerateComplete?.Invoke(_generatedRooms);
			
			_crawlers.Clear();
			_visitedPositions.Clear();
			_generatedRooms.Clear();
		}
		
		private class DungeonCrawler
		{
			private Vector2 _currentPosition = Vector2.zero;

            public Vector2 NextPosition (Vector2 direction) => _currentPosition += direction;
        }
	}
}
