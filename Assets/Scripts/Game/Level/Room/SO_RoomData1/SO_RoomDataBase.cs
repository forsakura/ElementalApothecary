using System.Collections.Generic;
using UnityEngine;

namespace Game.Level.Room.SO_RoomData1
{
    [CreateAssetMenu(fileName = "new SO_RoomData", menuName = "ScriptableObjects/Room Data/new RoomData")]
    public class SO_RoomDataBase : ScriptableObject
    {
        public List<string> teleportPrefabPaths;
        public List<GameObjectInfo> teleportInfos;
        public List<string> materialPrefabPaths;
        public List<GameObjectInfo> materialInfos;
        public List<string> enemyPrefabPaths;
        public List<GameObjectInfo> enemyInfos;
    }
}
