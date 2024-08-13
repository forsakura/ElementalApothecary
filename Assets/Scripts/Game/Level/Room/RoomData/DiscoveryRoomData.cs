using System.Collections.Generic;
using UnityEngine;

namespace Game.Level.Room.RoomData
{
    public class DiscoveryRoomData : RoomDataBase
    {
        public List<string> materialPrefabPaths;
        public List<Vector3> materialPositions;
        public List<string> enemyPrefabPaths;
        public List<Vector3> enemyPositions;
    }
}
