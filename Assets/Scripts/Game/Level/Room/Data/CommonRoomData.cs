using System.Collections.Generic;
using ProjectBase.Date;

namespace Game.Level.Room.Data
{
    //有传送点和素材数据
    public class CommonRoomData : RoomDataBase
    {
        public List<string> materialsPath = new List<string>();

        public CommonRoomData(string fileName)
        {
            InitData(fileName);
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<CommonRoomData>(fileName, JsonType.LitJson);
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath[i] = res.transformPointsPath[i];
            }

            for (int j = 0; j < res.materialsPath.Count; j++)
            {
                materialsPath[j] = res.materialsPath[j];
            }
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
