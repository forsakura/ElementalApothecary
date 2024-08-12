using System.Collections.Generic;
using ProjectBase.Date;
using UnityEngine;

namespace Game.Level
{
    public class SaveRoomData : MonoBehaviour
    {
        public List<string> roomDataPaths;
        void Start()
        {
            foreach (var dataPath in roomDataPaths)
            {
                string[] strs = dataPath.Split('/');
                string dataName = strs[^1];
                var data = Resources.Load(dataPath);
                SaveSystem.SaveGameByJson(dataName, data, JsonType.JsonUtility);
            }
        }
    }
}
