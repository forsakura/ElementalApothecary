using System;
using System.Collections.Generic;
using Game.Level.Room.Data;
using Game.Level.RoomInterface;
using Game.Level.TranslatePoints.Data;
using Game.Level.TranslatePoints.View;
using ProjectBase.Event;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    /*
     * 战斗房间
     */
    public class FightRoom : RoomBase, IInitEnemy, IInitTeleport
    {
        //敌人坐标集合
        public List<Transform> enemiesTransforms = new List<Transform>();

        //当前房间传送门集合
        public List<GameObject> transformObjects = new List<GameObject>();

        public int enemyCount;

        private void Start()
        {
            data = new FightRoomData(gameObject.name);
            InitTeleport();
            InitEnemies();
            EventCenter.Instance.AddEventListener(gameObject.name, DecreaseEnemyCount);
        }

        //生成敌人
        public void InitEnemies()
        {
            for (int i = 0; i < ((FightRoomData)data).enemiesPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((FightRoomData)data).enemiesPath[i], arg0 =>
                {
                    SetGameObject(arg0, enemiesTransforms[i1].transform, gameObject.transform);
                });
                enemyCount++;
            }
        }

        //生成传送门
        public void InitTeleport()
        {
            for (int i = 0; i < ((FightRoomData)data).transformPointsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((FightRoomData)data).transformPointsPath[i], arg0 =>
                {
                    SetGameObject(arg0, TeleportPositions[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                    transformObjects.Add(arg0);
                });
            }
        }
        
        //用于触发该房间数据中敌人数减少
        private void DecreaseEnemyCount()
        {
            enemyCount--;
            if (enemyCount != 0) return;
            foreach (var gb in transformObjects)
            {
                if (gb.CompareTag("Fight"))
                {
                    ((FightTransformPointData)gb.GetComponent<FightTransformPointView>().data).isTransform = true;
                }
            }
        }

        private void OnDestroy()
        {
            ((FightRoomData)data).SaveData(gameObject.name);
            EventCenter.Instance.RemoveEventLister(gameObject.name, DecreaseEnemyCount);
        }
    }
}
