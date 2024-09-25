using ProjectBase.Date;
using UnityEngine.SceneManagement;

namespace Game.Level.Room.Teleport.Data
{
    public class SpecialTeleportData : TeleportDataBase
    {
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
            destinationSceneName = res.destinationSceneName;
            currentSceneName = SceneManager.GetActiveScene().name;
        }

        public override void SaveData(string fileName)
        {
            base.SaveData(fileName);
            SaveSystem.SaveGameByJson(fileName, this, JsonType.LitJson);
        }
    }
}
