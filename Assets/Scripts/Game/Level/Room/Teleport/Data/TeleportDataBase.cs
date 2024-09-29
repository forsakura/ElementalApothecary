using UnityEngine;

namespace Game.Level.Room.Teleport.Data
{
    public enum PointType
    {
        Enter,
        Exit
    }
    public class TeleportDataBase
    {
        public string destinationSceneName;
        
        public Vector3 destinationPoint;

        public PointType type;


        public virtual void InitData(string fileName)
        {
            
        }

        public virtual void SaveData(string fileName)
        {
            
        }
    }
}
