using Game.Level.Room.RoomData;
using Game.Level.Room.RoomInterface;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    public class BossRoom : RoomBase, IInitEnemy, IInitTeleport, IInitOtherObject
    {
        private BossRoomData _bossRoomData;
        // Start is called before the first frame update
        void Start()
        {
            LoadData();
            _bossRoomData = data as BossRoomData;
            InitTeleport();
            InitEnemies();
        }

        private void OnDestroy()
        {
            SaveData();
        }

        public override void LoadData()
        {
            data = SaveSystem.LoadGameFromJson<BossRoomData>(fileName, JsonType.JsonUtility);
        }

        public override void SaveData()
        {
            SaveSystem.SaveGameByJson(fileName, _bossRoomData, JsonType.JsonUtility);
        }

        public void InitEnemies()
        {
            if (_bossRoomData != null)
                for (int i = 0; i < _bossRoomData.enemyPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_bossRoomData.enemyPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _bossRoomData.enemyInfos[i1], gameObject.transform);
                    });
                }
        }

        public void InitTeleport()
        {
            if (_bossRoomData!=null)
            {
                for (int i = 0; i < _bossRoomData.teleportPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_bossRoomData.teleportPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _bossRoomData.teleportInfos[i1], gameObject.transform);
                        SetTransformView(arg0, _bossRoomData.teleportFileNames[i1]);
                    });
                }
            }
        }

        public void InitOtherObjects()
        {
            
        }
    }
}
