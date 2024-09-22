using Game.Level.Room.RoomData;
using Game.Level.Room.RoomInterface;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    public class CampRoom : RoomBase, IInitTeleport, IInitOtherObject
    {
        private CampRoomData _campRoomData;
        // Start is called before the first frame update
        void Start()
        {
            LoadData();
            _campRoomData = data as CampRoomData;
            InitTeleport();
            InitOtherObjects();
        }

        // Update is called once per frame
        private void OnDestroy()
        {
            SaveData();
        }

        public override void LoadData()
        {
            data = SaveSystem.LoadGameFromJson<CampRoomData>(fileName, JsonType.JsonUtility);
        }

        public override void SaveData()
        {
            SaveSystem.SaveGameByJson(fileName, _campRoomData, JsonType.JsonUtility);
        }

        public void InitTeleport()
        {
            if (_campRoomData != null)
                for (int i = 0; i < _campRoomData.teleportPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_campRoomData.teleportPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _campRoomData.teleportInfos[i1], gameObject.transform);
                        SetTransformView(arg0);
                    });
                }
        }

        public void InitOtherObjects()
        {
            for (int i = 0; i < _campRoomData.materialPrefabPaths.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(_campRoomData.materialPrefabPaths[i1], arg0 =>
                {
                    SetGameObject(arg0, _campRoomData.materialInfos[i1], gameObject.transform);
                });
            }
        }
    }
}
