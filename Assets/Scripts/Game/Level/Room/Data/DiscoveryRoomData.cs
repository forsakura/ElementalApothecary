using System.Collections.Generic;
using ProjectBase.Date;
using ProjectBase.Event;

namespace Game.Level.Room.Data
{
    //有传送点和素材数据
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
        
        //初始化改房间数据，在该房间生成时触发
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

        //保存该房间中物品数据，当玩家离开该房间时保存
        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
