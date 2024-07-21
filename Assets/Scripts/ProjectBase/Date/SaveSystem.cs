using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using LitJson;
using ProjectBase.Mono;

namespace ProjectBase.Date
{
    /*
     * ���ݴ洢����
     * ����PlayerPrefs��json���ַ�ʽ�������ݴ洢����ȡ��ɾ����
     * jsonUtilityĿǰ��֧���ֵ�洢��nullֵ�޷��洢����ת��ΪĬ��ֵ��ע�����ݴ洢λ�ö���StreamingAssets�ļ������棬��ý����������඼�洢��Date�ļ����¡�
     * 
     * Json�ṩ���ֱַ���JsonUtility��JsonMapper��
     * JsonMapper�ֲ���JsonUtility�Ĳ���ȱ�㣬
     * �������л��̰壨���������޷����л������ֵ�����л����ֵ�ļ����Ͳ�����int�������л�ʱ������⣩��Ҳ�ɴ洢nullֵ��ֱ�Ӷ�ȡ���ݼ��ϣ���ע��˽�б����޷�ʹ�����������л���
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
        /// �洢����
        /// </summary>
        /// <param name="key"></param>
        /// <param name="saveDate">��Ҫ�洢������</param>
        public static void SaveGameByPlayerPrefs(string key, object saveDate)
        {
            var res = JsonUtility.ToJson(saveDate);
            PlayerPrefs.SetString(key, res);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="key">��Ҫ��ȡ������</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadGameByPlayerPrefs<T>(string key)
        {
            var res = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(res);
        }

        /// <summary>
        /// ɾ��ĳ���ض��浵
        /// </summary>
        /// <param name="key">��Ҫɾ���浵��</param>
        public static void DeleteSaveDateByPlayerPrefs(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        /// ɾ�����д浵
        /// </summary>
        public static void DeleteAllSaveDateByPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion

        #region Json

        /// <summary>
        /// �洢����
        /// </summary>
        /// <param name="fileName">���ݴ洢���ļ���</param>
        /// <param name="obj">��洢�Ķ���</param>
        /// <param name="type">ָ��ʹ�õ�Json����</param>
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
        /// ��ȡ����
        /// </summary>
        /// <param name="fileName">��Ҫ��ȡ���ļ���</param>
        /// <param name="type">ָ��ʹ�õ�Json����</param>
        /// <typeparam name="T">��ָ������������</typeparam>
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
        /// ɾ���浵����
        /// </summary>
        /// <param name="fileName">��ɾ���������ļ�</param>
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
