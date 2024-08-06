using ProjectBase.Date;

namespace Game.Level.TranslatePoints.Data
{
    public class FightTransformPointData : TransformPointDataBase
    {
        public bool isTransform;

        public FightTransformPointData(string fileName)
        {
            InitData(fileName);
        }

        public void ChangeTransform(bool b)
        {
            isTransform = b;
        }

        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<FightTransformPointData>(fileName, JsonType.LitJson);
            destinationPoint = res.destinationPoint;
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
