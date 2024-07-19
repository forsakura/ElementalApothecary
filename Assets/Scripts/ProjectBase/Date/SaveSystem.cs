using System.IO;
using UnityEngine;

namespace ProjectBase.Date
{
    /*
     * 数据存储工具
     * 包含PlayerPrefs，json两种方式进行数据存储，读取和删除。
     * json目前不支持字典存储，注意数据存储位置都在StreamingAssets文件夹下面，最好将所有数据类都存储在Date文件夹下。
     * 当前尚不完善，还有待补充
     */
    public static class SaveSystem
    {
        #region PlayerPrefs

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="saveDate">需要存储的数据</param>
        public static void SaveGameByPlayerPrefs(string key, object saveDate)
        {
            var res = JsonUtility.ToJson(saveDate);
            PlayerPrefs.SetString(key, res);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="key">需要读取的数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadGameByPlayerPrefs<T>(string key)
        {
            var res = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(res);
        }

        /// <summary>
        /// 删除某个特定存档
        /// </summary>
        /// <param name="key">需要删除存档键</param>
        public static void DeleteSaveDateByPlayerPrefs(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        /// 删除所有存档
        /// </summary>
        public static void DeleteAllSaveDateByPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion

        #region Json

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="fileName">数据存储的文件名</param>
        /// <param name="obj">需存储的对象</param>
        public static void SaveGameByJson<T>(string fileName, T obj)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            var json = JsonUtility.ToJson(obj);
            try
            {
                File.WriteAllText(path, json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error to Save date to {path}. \n{e}");
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="fileName">需要读取的文件名</param>
        /// <typeparam name="T">需指定的数据类型</typeparam>
        /// <returns></returns>
        public static T LoadGameFromJson<T>(string fileName)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            var json = File.ReadAllText(path);
            try
            {
                var res = JsonUtility.FromJson<T>(json);
                return res;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error to load from {path}. \n{e}");
                return default;
            }
        }

        /// <summary>
        /// 删除存档数据
        /// </summary>
        /// <param name="fileName">需删除的数据文件</param>
        public static void DeleteGameDateByJson(string fileName)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            try
            {
                File.Delete(path);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error to delete {path}. \n{e}");
            }
        }

        #endregion
    }
}
