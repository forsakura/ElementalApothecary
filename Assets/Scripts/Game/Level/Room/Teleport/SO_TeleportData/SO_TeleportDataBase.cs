using Game.Level.Room.Teleport.Data;
using UnityEngine;

namespace Game.Level.Room.Teleport.SO_TeleportData
{
    [CreateAssetMenu(fileName = "new SO_TeleportData", menuName = "ScriptableObjects/Room Data/new TeleportData")]
    public class SO_TeleportDataBase : ScriptableObject
    {
        public string destinationSceneName;
        
        public Vector3 destinationPoint;

        public PointType type;

        public bool isOpen;
    }
}
