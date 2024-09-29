using Game.Level.Room.RoomData;
using Game.Level.Room.RoomInterface;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    public class StrengtheningRoom : RoomBase, IInitTeleport, IInitOtherObject
    {
        private StrengtheningRoomData _strengtheningRoomData;
        // Start is called before the first frame update
        void Start()
        {
            LoadData();
            _strengtheningRoomData = data as StrengtheningRoomData;
            InitTeleport();
            InitOtherObjects();
        }

        private void OnDestroy()
        {
            SaveData();
        }

        public override void LoadData()
        {
            data = SaveSystem.LoadGameFromJson<StrengtheningRoomData>(fileName, JsonType.JsonUtility);
        }

        public override void SaveData()
        {
            SaveSystem.SaveGameByJson(fileName, _strengtheningRoomData, JsonType.JsonUtility);
        }

        public void InitTeleport()
        {
            for (int i = 0; i < _strengtheningRoomData.teleportPrefabPaths.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(_strengtheningRoomData.teleportPrefabPaths[i1], arg0 =>
                {
                    SetGameObject(arg0, _strengtheningRoomData.teleportInfos[i1], gameObject.transform);
                    SetTransformView(arg0, _strengtheningRoomData.teleportFileNames[i1]);
                });
            }
        }

        public void InitOtherObjects()
        {
            for (int i = 0; i < _strengtheningRoomData.materialPrefabPaths.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(_strengtheningRoomData.materialPrefabPaths[i1], arg0 =>
                {
                    SetGameObject(arg0, _strengtheningRoomData.materialInfos[i1], gameObject.transform);
                });
            }
        }
    }
}
