using Game.Level.Room.RoomData;
using Game.Level.Room.RoomInterface;
using ProjectBase.Date;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    public class FightRoom : RoomBase, IInitEnemy, IInitTeleport, IInitOtherObject
    {
        private FightRoomData _fightRoomData;
        // Start is called before the first frame update
        void Start()
        {
            LoadData();
            _fightRoomData = data as FightRoomData;
            InitTeleport();
            InitEnemies();
            InitOtherObjects();
        }

        // Update is called once per frame
        void OnDestroy()
        {
            SaveData();
        }

        public override void LoadData()
        {
            data = SaveSystem.LoadGameFromJson<FightRoomData>(fileName, JsonType.JsonUtility);
        }

        public override void SaveData()
        {
            SaveSystem.SaveGameByJson(fileName, _fightRoomData, JsonType.JsonUtility);
        }

        public void InitEnemies()
        {
            if (_fightRoomData != null)
                for (int i = 0; i < _fightRoomData.enemyPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_fightRoomData.enemyPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _fightRoomData.enemyPositions[i1], gameObject.transform);
                    });
                }
        }

        public void InitTeleport()
        {
            if (_fightRoomData != null)
                for (int i = 0; i < _fightRoomData.teleportPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_fightRoomData.teleportPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _fightRoomData.teleportPositions[i1], gameObject.transform);
                        SetTransformView(arg0);
                    });
                }
        }

        public void InitOtherObjects()
        {
            if (_fightRoomData != null)
                for (int i = 0; i < _fightRoomData.materialPrefabPaths.Count; i++)
                {
                    int i1 = i;
                    ResManager.LoadResourceAsync<GameObject>(_fightRoomData.materialPrefabPaths[i1], arg0 =>
                    {
                        SetGameObject(arg0, _fightRoomData.materialPositions[i1], gameObject.transform);
                    });
                }
        }
    }
}
