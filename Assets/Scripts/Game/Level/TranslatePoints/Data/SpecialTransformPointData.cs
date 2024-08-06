using ProjectBase.Date;

namespace Game.Level.TranslatePoints.Data
{
    public class SpecialTransformPointData : TransformPointDataBase
    {
        public string nextSceneName;
        public string currentSceneName;

        public SpecialTransformPointData(string fileName)
        {
            InitData(fileName);
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<SpecialTransformPointData>(fileName, JsonType.LitJson);
            destinationPoint = res.destinationPoint;
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
