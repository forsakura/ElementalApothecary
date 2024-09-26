using Game.Level.Room.RoomData;
using Game.Level.Room.RoomInterface;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    public class ShopRoom : RoomBase, IInitTeleport, IInitOtherObject
    {
        private ShopRoomData _shopRoomData;
        // Start is called before the first frame update
        void Start()
        {
            LoadData();
            _shopRoomData = data as ShopRoomData;
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
            data = SaveSystem.LoadGameFromJson<ShopRoomData>(fileName, JsonType.JsonUtility);
        }

        public override void SaveData()
        {
            SaveSystem.SaveGameByJson(fileName, _shopRoomData, JsonType.JsonUtility);
        }

        public void InitTeleport()
        {
            if (_shopRoomData != null)
            {
                for (int i = 0; i < _shopRoomData.teleportPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_shopRoomData.teleportPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _shopRoomData.teleportInfos[i1], gameObject.transform);
                        SetTransformView(arg0, _shopRoomData.teleportFileNames[i1]);
                    });
                }
            }
        }

        public void InitOtherObjects()
        {
            for (int i = 0; i < _shopRoomData.materialPrefabPaths.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(_shopRoomData.materialPrefabPaths[i1], arg0 =>
                {
                    SetGameObject(arg0, _shopRoomData.materialInfos[i1], gameObject.transform);
                });
            }
        }
    }
}
