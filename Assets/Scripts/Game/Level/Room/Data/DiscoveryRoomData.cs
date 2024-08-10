using System.Collections.Generic;
using ProjectBase.Date;
using ProjectBase.Event;

namespace Game.Level.Room.Data
{
    //�д��͵���ز�����
    public class DiscoveryRoomData : RoomDataBase
    {
        public List<string> materialsPath;
        public List<string> enemiesPath;

        public DiscoveryRoomData() : base()
        {
            
        }

        public DiscoveryRoomData(string fileName) : base(fileName)
        {
            LoadData(fileName);
        }

        ~DiscoveryRoomData()
        {
            EventCenter.Instance.RemoveEventLister(fileName, () =>
            {
                SaveData(fileName);
            });
        }
        
        //��ʼ���ķ������ݣ��ڸ÷�������ʱ����
        public override void LoadData(string fileName)
        {
            base.LoadData(fileName);
            var res = SaveSystem.LoadGameFromJson<DiscoveryRoomData>(fileName, JsonType.LitJson);
            transformPointsPath = new List<string>();
            for (int i = 0; i < res.transformPointsPath.Count; i++)
            {
                transformPointsPath.Add(res.transformPointsPath[i]);
            }

            materialsPath = new List<string>(res.materialsPath.Count);
            for (int j = 0; j < res.materialsPath.Count; j++)
            {
                materialsPath.Add(res.materialsPath[j]);
            }

            enemiesPath = new List<string>(res.enemiesPath.Count);
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
