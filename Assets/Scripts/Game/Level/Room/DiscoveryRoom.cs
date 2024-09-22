using Game.Level.Room.RoomData;
using Game.Level.Room.RoomInterface;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    public class DiscoveryRoom : RoomBase, IInitEnemy, IInitOtherObject, IInitTeleport
    {
        private DiscoveryRoomData _discoveryRoomData;
        private void Start()
        {
            LoadData();
            _discoveryRoomData = data as DiscoveryRoomData;
            InitEnemies();
            InitTeleport();
            InitOtherObjects();
        }

        private void OnDestroy()
        {
            SaveData();
        }

        public override void LoadData()
        {
            data = SaveSystem.LoadGameFromJson<DiscoveryRoomData>(fileName, JsonType.JsonUtility);
        }

        public override void SaveData()
        {
            SaveSystem.SaveGameByJson(fileName, _discoveryRoomData, JsonType.JsonUtility);
        }
        public void InitEnemies()
        {
            if (_discoveryRoomData != null)
            {
                for (int i = 0; i < _discoveryRoomData.enemyPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_discoveryRoomData.enemyPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _discoveryRoomData.enemyInfos[i1], gameObject.transform);
                    });
                }
            }
        }

        public void InitOtherObjects()
        {
            if (_discoveryRoomData != null)
            {
                for (int i = 0; i < _discoveryRoomData.materialPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_discoveryRoomData.materialPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _discoveryRoomData.materialInfos[i1], gameObject.transform);
                    });
                }
            }
        }

        public void InitTeleport()
        {
            if (_discoveryRoomData != null)
            {
                for (int i = 0; i < _discoveryRoomData.teleportPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_discoveryRoomData.teleportPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _discoveryRoomData.teleportInfos[i1], gameObject.transform);
                        SetTransformView(arg0);
                    });
                }
            }
        }
    }
}
