using System.Collections.Generic;
using ProjectBase.Date;
using ProjectBase.Event;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room.Data
{
    public class BossRoomData : RoomDataBase
    {
        //boss预制体的文件路径
        public string bossPrefabPath;

        public BossRoomData() : base()
        {
            
        }

        public BossRoomData(string fileName) : base(fileName)
        {
            LoadData(fileName);
        }

        ~BossRoomData()
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
            BossRoomData res = SaveSystem.LoadGameFromJson<BossRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }
            bossPrefabPath = res.bossPrefabPath;
        }
    }
}
