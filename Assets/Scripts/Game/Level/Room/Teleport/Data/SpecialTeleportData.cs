using ProjectBase.Date;

namespace Game.Level.Room.Teleport.Data
{
    public class SpecialTeleportData : TeleportDataBase
    {
        public string nextSceneName;
        public string currentSceneName;


        public SpecialTeleportData()
        {
            
        }
        public SpecialTeleportData(string fileName)
        {
            InitData(fileName);
        }
        public override void InitData(string fileName)
        {
            base.InitData(fileName);
            var res = SaveSystem.LoadGameFromJson<SpecialTeleportData>(fileName, JsonType.LitJson);
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
