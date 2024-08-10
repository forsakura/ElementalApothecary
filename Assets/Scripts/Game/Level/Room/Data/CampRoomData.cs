using System.Collections.Generic;
using ProjectBase.Date;
using ProjectBase.Event;

namespace Game.Level.Room.Data
{
    public class CampRoomData : RoomDataBase
    {
        public CampRoomData() : base()
        {
            
        }
        public CampRoomData(string fileName) : base(fileName)
        {
            LoadData(fileName);
        }

        ~CampRoomData()
        {
            EventCenter.Instance.RemoveEventLister(fileName, () =>
            {
                SaveData(fileName);
            });
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
