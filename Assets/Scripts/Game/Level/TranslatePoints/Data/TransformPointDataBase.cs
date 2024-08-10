using ProjectBase.Date;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Level.TranslatePoints.Data
{
    public enum PointType
    {
        Enter,
        Exit
    }
    public class TransformPointDataBase
    {
        public string destinationPointName;
        
        public Transform destinationPoint;

        public PointType type;


        public virtual void InitData(string fileName)
        {
            
        }

        public virtual void SaveData(string fileName)
        {
            
        }
    }
}
