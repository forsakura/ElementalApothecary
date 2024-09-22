using System.IO;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TeleportDataTool
    {
        private static readonly string teleportDataPath = Application.dataPath + "Resources/Room/Teleport";

        [MenuItem("GameTool/RoomTool/生成传送点json数据文件")]
        public static void InitTeleportData()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(teleportDataPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Extension.Equals(".asset"))
                {
                    string[] strs = fileInfo.Name.Split('.');
                    string fileName = strs[0];
                    string filePath = "Room/Teleport/" + fileName;
                    var res = Resources.Load(filePath);
                    SaveSystem.SaveGameByJson(fileName, res, JsonType.JsonUtility);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}
