using System.Collections.Generic;
using Game.Level.TranslatePoints.Data;

namespace Game.Level.Room.Data
{
    public class RoomDataBase
    {
        //���͵����Դ·��
        public List<string> transformPointsPath = new List<string>();
        
        //���͵㼯��
        public virtual void SaveData(string fileName)
        {
            
        }

        public virtual void InitData(string fileName)
        {
            
        }
    }
}
