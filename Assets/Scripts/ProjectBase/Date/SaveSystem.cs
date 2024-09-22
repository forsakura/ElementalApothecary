using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using LitJson;

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
        public static string DATA_BINARY_PATH = Application.streamingAssetsPath + "/Binary/";

        public static Dictionary<string, object> tableDic = new Dictionary<string, object>();

        public static string applicationPath = Application.streamingAssetsPath;
        
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
            var path = Application.streamingAssetsPath + "/" + fileName + ".json";
            var jsonStr = type switch
            {
                JsonType.JsonUtility => JsonUtility.ToJson(obj, true),
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
            var path = applicationPath + "/" + fileName + ".json";
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
            var path = applicationPath + "/" + fileName + ".json";
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
            using (FileStream fs = new FileStream(applicationPath+"/fileName", FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
                fs.Close();
            }
        }

        public static T LoadByBinary<T>(string fileName) where T : class
        {
            if (!File.Exists(applicationPath + "/fileName")) return default;
            using (FileStream fs =new FileStream(applicationPath+"/fileName", FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T res = bf.Deserialize(fs) as T;
                fs.Close();
                return res;
            }
        }

        public static void DeleteBinaryData(string fileName)
        {
            File.Delete(applicationPath+"/fileName");
        }

        
        /// <summary>
        ///���ر����ݵ��ڴ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        public static void LoadTable<T, K>()
        {
            using (FileStream fs = File.Open(DATA_BINARY_PATH+typeof(K).Name, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                int index = 0;
                int count = BitConverter.ToInt32(bytes, index);
                index += 4;
                int keyNameLength = BitConverter.ToInt32(bytes, index);
                index += 4;
                string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
                index += keyNameLength;

                Type contrainerType = typeof(T);
                object contrainerObj = Activator.CreateInstance(contrainerType);

                Type classType = typeof(K);
                FieldInfo[] fieldInfos = classType.GetFields();
                for (int i = 0; i < count; i++)
                {
                    object classObj = Activator.CreateInstance(classType);
                    foreach (var info in fieldInfos)
                    {
                        if( info.FieldType == typeof(int) )
                        {
                            info.SetValue(classObj, BitConverter.ToInt32(bytes, index));
                            index += 4;
                        }
                        else if (info.FieldType == typeof(float))
                        {
                            info.SetValue(classObj, BitConverter.ToSingle(bytes, index));
                            index += 4;
                        }
                        else if (info.FieldType == typeof(bool))
                        {
                            info.SetValue(classObj, BitConverter.ToBoolean(bytes, index));
                            index += 1;
                        }
                        else if (info.FieldType == typeof(string))
                        {
                            int length = BitConverter.ToInt32(bytes, index);
                            index += 4;
                            info.SetValue(classObj, Encoding.UTF8.GetString(bytes, index, length));
                            index += length;
                        }
                    }

                    object dicObject = contrainerType.GetField("dataDic").GetValue(contrainerObj);
                    MethodInfo method = dicObject.GetType().GetMethod("Add");
                    object keyValue = classType.GetField(keyName).GetValue(classObj);
                    method.Invoke(dicObject, new object[] { keyValue, classObj });
                }
                
                tableDic.Add(typeof(T).Name, contrainerObj);
                
                fs.Close();
                
            }
        }

        /// <summary>
        /// ��ȡ����Ϣ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetTable<T>() where T : class
        {
            string tableName = typeof(T).Name;
            if (tableDic.ContainsKey(tableName)) return tableDic[tableName] as T;
            return null;
        }

        #endregion
        
    }
}
