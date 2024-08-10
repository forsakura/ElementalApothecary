using ProjectBase.Date;
using ProjectBase.Event;
using UnityEngine;

namespace Game.Level.TranslatePoints.Data
{
    public class FightTransformPointData : TransformPointDataBase
    {
        public bool isTransform;

        public FightTransformPointData()
        {
            
        }

        public FightTransformPointData(string fileName)
        {
            this.fileName = fileName;
            InitData(fileName);
            EventCenter.Instance.AddEventListener(fileName, () =>
            {
                SaveData(fileName);
            });
        }

        ~FightTransformPointData()
        {
            EventCenter.Instance.RemoveEventLister(fileName, () =>
            {
                SaveData(fileName);
            });
        }

        public void ChangeTransform(bool b)
        {
            isTransform = b;
        }

        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<FightTransformPointData>(fileName, JsonType.LitJson);
            tipPanelName = res.tipPanelName;
            destinationPointName = res.destinationPointName;
            destinationPoint = GameObject.Find(destinationPointName).transform;
            type = res.type;
            isTransform = res.isTransform;
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
