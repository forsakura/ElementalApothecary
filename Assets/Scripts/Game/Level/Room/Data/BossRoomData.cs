using System.Collections.Generic;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room.Data
{
    public class BossRoomData : RoomDataBase
    {
        //bossԤ������ļ�·��
        public string bossPrefabPath;
        //boss����ʱ�ڷ����λ��
        public Transform bossInitTranform;

        public BossRoomData()
        {
            
        }

        public BossRoomData(string fileName)
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
            BossRoomData res = SaveSystem.LoadGameFromJson<BossRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }
            bossPrefabPath = res.bossPrefabPath;
            bossInitTranform = res.bossInitTranform;
        }
    }
}
