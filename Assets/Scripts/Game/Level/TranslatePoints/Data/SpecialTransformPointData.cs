using ProjectBase.Date;
using ProjectBase.Event;
using UnityEngine;

namespace Game.Level.TranslatePoints.Data
{
    public class SpecialTransformPointData : TransformPointDataBase
    {
        public string nextSceneName;
        public string currentSceneName;


        public SpecialTransformPointData()
        {
            
        }
        public SpecialTransformPointData(string fileName)
        {
            this.fileName = fileName;
            InitData(fileName);
            EventCenter.Instance.AddEventListener(fileName, () =>
            {
                SaveData(fileName);
            });
        }

        ~SpecialTransformPointData()
        {
            EventCenter.Instance.RemoveEventLister(fileName, () =>
            {
                SaveData(fileName);
            });
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<SpecialTransformPointData>(fileName, JsonType.LitJson);
            tipPanelName = res.tipPanelName;
            destinationPointName = res.destinationPointName;
            destinationPoint = GameObject.Find(destinationPointName).transform;
            type = res.type;
            nextSceneName = res.nextSceneName;
            currentSceneName = res.currentSceneName;
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
