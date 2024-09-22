using ProjectBase.Date;

namespace Game.Level.Room.Teleport.Data
{
    public class FightTeleportData : TeleportDataBase
    {
        public bool isTransform;

        public FightTeleportData()
        {
            
        }

        public FightTeleportData(string fileName)
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
            var res = SaveSystem.LoadGameFromJson<FightTeleportData>(fileName, JsonType.LitJson);
            destinationPointName = res.destinationPointName;
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