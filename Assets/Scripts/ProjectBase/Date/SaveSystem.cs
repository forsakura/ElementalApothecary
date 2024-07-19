using System.IO;
using UnityEngine;

namespace ProjectBase.Date
{
    /*
     * ���ݴ洢����
     * ����PlayerPrefs��json���ַ�ʽ�������ݴ洢����ȡ��ɾ����
     * jsonĿǰ��֧���ֵ�洢��ע�����ݴ洢λ�ö���StreamingAssets�ļ������棬��ý����������඼�洢��Date�ļ����¡�
     * ��ǰ�в����ƣ����д�����
     */
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
        /// ��ȡ����
        /// </summary>
        /// <param name="fileName">��Ҫ��ȡ���ļ���</param>
        /// <typeparam name="T">��ָ������������</typeparam>
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
    }
}
