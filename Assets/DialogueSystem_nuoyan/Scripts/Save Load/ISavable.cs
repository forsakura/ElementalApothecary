
public interface ISavable 
{
    void RegisterSaveData() => SaveLoadManager.RegisterSaveData(this);

    void UnRegisterSaveData() => SaveLoadManager.UnRegisterSaveData(this);

    void GetSaveData(Data data);
    void LoadData(Data data);
}
