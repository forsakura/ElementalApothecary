using System.IO;
using ProjectBase.Date;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class RoomDataTool
    {
        private static readonly string dataPath = Application.dataPath + "/Resources/Room";

        [MenuItem("GameTool/RoomTool/生成房间json数据文件")]
        public static void InitRoomDataJson()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dataPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Extension == ".asset")
                {
                    string[] strs = fileInfo.Name.Split('.');
                    string fileName = strs[0];
                    string filePath = "Room/" + fileName;
                    var obj = Resources.Load(filePath);
                    SaveSystem.SaveGameByJson(fileName, obj, JsonType.JsonUtility);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}
