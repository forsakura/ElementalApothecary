using System.Collections.Generic;
using UnityEngine;

namespace Game.Level.Room.RoomData
{
    public class FightRoomData : RoomDataBase
    {
        public List<string> materialPrefabPaths;
        public List<GameObjectInfo> materialInfos;
        public List<string> enemyPrefabPaths;
        public List<GameObjectInfo> enemyInfos;
    }
}
