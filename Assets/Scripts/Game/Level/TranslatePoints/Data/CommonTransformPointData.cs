using ProjectBase.Date;
using ProjectBase.Event;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Game.Level.TranslatePoints.Data
{
    public class CommonTransformPointData : TransformPointDataBase
    {
        public CommonTransformPointData()
        {
            
        }
        public CommonTransformPointData(string fileName)
        {
            this.fileName = fileName;
            InitData(fileName);
            EventCenter.Instance.AddEventListener(fileName, () =>
            {
                SaveData(fileName);
            });
        }

        ~CommonTransformPointData()
        {
            EventCenter.Instance.RemoveEventLister(fileName, () =>
            {
                SaveData(fileName);
            });
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<CommonTransformPointData>(fileName, JsonType.LitJson);
            tipPanelName = res.tipPanelName;
            destinationPointName = res.destinationPointName;
            destinationPoint = GameObject.Find(destinationPointName).transform;
            type = res.type;
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
