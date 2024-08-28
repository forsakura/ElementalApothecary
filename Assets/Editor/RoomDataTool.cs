using System;
using System.IO;
using System.Net.Mime;
using ProjectBase.Date;
using ProjectBase.Res;
using Unity.VisualScripting;
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
                    string[] strs = fileInfo.Name.Split(' ', '.');
                    string filePath = fileInfo.Name.Split('.')[0];
                    string path = "Room/" + filePath;
                    string fileName = strs[1];
                    var obj = Resources.Load(path);
                    SaveSystem.SaveGameByJson(fileName, obj, JsonType.JsonUtility);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}
