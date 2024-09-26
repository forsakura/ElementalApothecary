using Game.Level.Room.Teleport.Data;
using ProjectBase.Date;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Game.Level.TranslatePoints.Data
{
    public class CommonTeleportData : TeleportDataBase
    {
        public CommonTeleportData()
        {
            
        }
        public CommonTeleportData(string fileName)
        {
            InitData(fileName);
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            //Debug.Log(fileName);
            var res = SaveSystem.LoadGameFromJson<CommonTeleportData>(fileName, JsonType.LitJson);
            //destinationPointName = res.destinationPointName;
            destinationPoint = res.destinationPoint;
            type = res.type;
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
