using System.Collections.Generic;
using Game.Level.TranslatePoints.Data;
using ProjectBase.Event;
using UnityEditor;

namespace Game.Level.Room.Data
{
    public class RoomDataBase
    {
        public string fileName;
        
        //传送点的资源路径
        public List<string> transformPointsPath;

        public RoomDataBase()
        {
            
        }
        
        public RoomDataBase(string fileName)
        {
            this.fileName = fileName;
            EventCenter.Instance.AddEventListener(fileName, ()=>
            {
                SaveData(fileName);
            });
        }
        
        public virtual void SaveData(string fileName)
        {
            
        }

        public virtual void LoadData(string fileName)
        {
            
        }
    }
}
