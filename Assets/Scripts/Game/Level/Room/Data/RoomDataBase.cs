using System.Collections.Generic;
using Game.Level.TranslatePoints.Data;

namespace Game.Level.Room.Data
{
    public class RoomDataBase
    {
        //传送点的资源路径
        public List<string> transformPointsPath;
        
        public virtual void SaveData(string fileName)
        {
            
        }

        public virtual void LoadData(string fileName)
        {
            
        }
    }
}
