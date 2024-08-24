using System.Collections.Generic;
using ProjectBase.Date;
using UnityEngine;

namespace Game.Level.Room.Data
{
    public class FightRoomData : RoomDataBase
    {
        //����Ԥ����·��
        public List<string> enemiesPath;
        
        //�ز���Դ·��
        public List<string> materialsPath;

        public FightRoomData()
        {
            
        }

        public FightRoomData(string fileName)
        {
            LoadData(fileName);
        }
        
        //��ʼ���ķ������ݣ��ڸ÷�������ʱ����
        public override void LoadData(string fileName)
        {
            base.LoadData(fileName);
            var res = SaveSystem.LoadGameFromJson<FightRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }

            materialsPath = new List<string>();
            for (int j = 0; j < res.materialsPath.Count; j++)
            {
                materialsPath.Add(res.materialsPath[j]);
            }

            enemiesPath = new List<string>();
            for (int k = 0; k < res.enemiesPath.Count; k++)
            {
                enemiesPath.Add(res.enemiesPath[k]);
            }
        }

        //����÷�������Ʒ���ݣ�������뿪�÷���ʱ����
        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}