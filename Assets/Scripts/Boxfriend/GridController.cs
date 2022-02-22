using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boxfriend.Dungeon;
using Pathfinding;

namespace Boxfriend
{

    public class GridController : MonoBehaviour
    {
        private GridGraph _graph;
        private void OnEnable ()
        {
            Room.OnRoomEntered += UpdateGridPosition;
            _graph = (GridGraph)AstarPath.active.data.FindGraphOfType(typeof(GridGraph));
        }

        private void OnDisable ()
        {
            Room.OnRoomEntered -= UpdateGridPosition;
        }

        private void UpdateGridPosition(Room room)
        {
            _graph.center = room.transform.position;
            _graph.Scan();
        }
    }
}