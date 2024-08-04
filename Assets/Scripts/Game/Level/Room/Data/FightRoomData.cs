using System.Collections.Generic;
using ProjectBase.Date;

namespace Game.Level.Room.Data
{
    public class FightRoomData : CommonRoomData
    {
        public List<string> enemiesPath = new List<string>();

        public FightRoomData(string fileName) : base(fileName)
        {
            InitData(fileName);
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<FightRoomData>(fileName, JsonType.LitJson);
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath[i] = res.transformPointsPath[i];
            }

            for (int j = 0; j < res.materialsPath.Count; j++)
            {
                materialsPath[j] = res.materialsPath[j];
            }

            for (int k = 0; k < res.enemiesPath.Count; k++)
            {
                enemiesPath[k] = res.enemiesPath[k];
            }
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
