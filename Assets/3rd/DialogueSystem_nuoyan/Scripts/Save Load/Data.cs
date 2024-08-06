using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Data
{
    public string scrData;
    public List<MessionDataSO> messionDataSOList;
    public Dictionary<string, float> flotSaveData = new Dictionary<string, float>();
    public Dictionary<string,SerializeVector3> characterPosDicData = new Dictionary<string,SerializeVector3>();
    

    public void SerializableData(object obj)
    {
        scrData = JsonUtility.ToJson(obj);
    }
    public T DeSerializeData<T>() where T : ScriptableObject
    {
        var data = ScriptableObject.CreateInstance<T>();

        JsonUtility.FromJsonOverwrite(scrData,data);

        return data;
    }
}
[System.Serializable]
public class SaveDataCatalogue
{
    public List<SaveButtonData> saveButtonDataList;
}
[System.Serializable]
public class SaveButtonData
{
    public string saveName;
    public string savePath;
}

[System.Serializable]
public class SerializeVector3
{
    public float x, y, z;

    public SerializeVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}