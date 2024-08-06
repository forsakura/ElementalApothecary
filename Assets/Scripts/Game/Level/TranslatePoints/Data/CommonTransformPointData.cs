using ProjectBase.Date;
using Unity.Burst.Intrinsics;

namespace Game.Level.TranslatePoints.Data
{
    public class CommonTransformPointData : TransformPointDataBase
    {
        public CommonTransformPointData(string fileName)
        {
            InitData(fileName);
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<CommonTransformPointData>(fileName, JsonType.LitJson);
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
