using System.Collections.Generic;
using ProjectBase.Date;

namespace Game.Level.Room.Data
{
    public class CampRoomData : RoomDataBase
    {
        public CampRoomData()
        {
            
        }
        public CampRoomData(string fileName)
        {
            LoadData(fileName);
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }

        public override void LoadData(string fileName)
        {
            base.LoadData(fileName);
            var res = SaveSystem.LoadGameFromJson<CampRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }
        }
    }
}
