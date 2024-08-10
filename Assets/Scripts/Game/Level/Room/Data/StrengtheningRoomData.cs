using System.Collections.Generic;
using ProjectBase.Date;

namespace Game.Level.Room.Data
{
    public class StrengtheningRoomData : RoomDataBase
    {
        public StrengtheningRoomData()
        {
            
        }
        public StrengtheningRoomData(string fileName)
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
            var res = SaveSystem.LoadGameFromJson<StrengtheningRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }
        }
    }
}
