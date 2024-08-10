using System.Collections.Generic;
using ProjectBase.Date;
using ProjectBase.Event;
using UnityEngine;

namespace Game.Level.Room.Data
{
    public class FightRoomData : RoomDataBase
    {
        //敌人预制体路径
        public List<string> enemiesPath;

        public FightRoomData() : base()
        {
            
        }

        public FightRoomData(string fileName) : base(fileName)
        {
            LoadData(fileName);
        }

        ~FightRoomData()
        {
            EventCenter.Instance.RemoveEventLister(fileName, () =>
            {
                SaveData(fileName);
            });
        }
        
        //初始化改房间数据，在该房间生成时触发
        public override void LoadData(string fileName)
        {
            base.LoadData(fileName);
            var res = SaveSystem.LoadGameFromJson<FightRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }

            enemiesPath = new List<string>();
            for (int k = 0; k < res.enemiesPath.Count; k++)
            {
                enemiesPath.Add(res.enemiesPath[k]);
            }
        }

        //保存该房间中物品数据，当玩家离开该房间时保存
        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
