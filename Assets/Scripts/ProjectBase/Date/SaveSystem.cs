using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using LitJson;
using ProjectBase.Mono;

namespace ProjectBase.Date
{
    /*
     * 数据存储工具
     * 包含PlayerPrefs，json两种方式进行数据存储，读取和删除。
     * jsonUtility目前不支持字典存储，null值无法存储，会转换为默认值，注意数据存储位置都在StreamingAssets文件夹下面，最好将所有数据类都存储在Date文件夹下。
     * 
     * Json提供两种分别是JsonUtility和JsonMapper。
     * JsonMapper弥补了JsonUtility的部分缺点，
     * 就是序列化短板（部分数据无法序列化，如字典可序列化（字典的键类型不能是int，反序列化时会出问题），也可存储null值，直接读取数据集合）。注意私有变量无法使用特性来序列化。
     */

    public enum JsonType
    {
        JsonUtility,
        LitJson
    }
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
        /// <param name="type">指定使用的Json类型</param>
        public static void SaveGameByJson<T>(string fileName, T obj, JsonType type)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            var jsonStr = type switch
            {
                JsonType.JsonUtility => JsonUtility.ToJson(obj),
                JsonType.LitJson => JsonMapper.ToJson(obj),
                _ => null
            };
            try
            {
                File.WriteAllText(path, jsonStr);
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
        /// <param name="type">指定使用的Json类型</param>
        /// <typeparam name="T">需指定的数据类型</typeparam>
        /// <returns></returns>
        public static T LoadGameFromJson<T>(string fileName, JsonType type)
        {
            var path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (!File.Exists(path)) return default;
            var jsonStr = File.ReadAllText(path);
            try
            {
                var res = type switch
                {
                    JsonType.JsonUtility => JsonUtility.FromJson<T>(jsonStr),
                    JsonType.LitJson => JsonMapper.ToObject<T>(jsonStr),
                    _ => default
                };

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

        #region Binary

        public static void SaveByBinary(object obj, string fileName)
        {
            using (FileStream fs = new FileStream(Application.streamingAssetsPath+"/fileName", FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
                fs.Close();
            }
        }

        public static T LoadByBinary<T>(string fileName) where T : class
        {
            if (!File.Exists(Application.streamingAssetsPath + "/fileName")) return default;
            using (FileStream fs =new FileStream(Application.streamingAssetsPath+"/fileName", FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T res = bf.Deserialize(fs) as T;
                fs.Close();
                return res;
            }
        }

        public static void DeleteBinaryData(string fileName)
        {
            File.Delete(Application.streamingAssetsPath+"/fileName");
        }

        #endregion
        
    }
}
