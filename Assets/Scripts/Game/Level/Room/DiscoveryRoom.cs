using System;
using System.Collections.Generic;
using Game.Level.Room.Data;
using Game.Level.RoomInterface;
using ProjectBase.Event;
using ProjectBase.Res;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Level.Room
{
    
    /*
     * 探索房间
     */
    public class DiscoveryRoom : RoomBase, IInitEnemy, IInitOtherObject, IInitTeleport
    {
        public List<Transform> materialPositions = new List<Transform>();
        public List<Transform> enemyPositions = new List<Transform>();

        private void Start()
        {
            data = new DiscoveryRoomData(gameObject.name);
            InitEnemies();
            InitOtherObjects();
            InitTeleport();
        }

        //初始化敌人在房间中
        public void InitEnemies()
        {
            for (int i = 0; i < ((DiscoveryRoomData)data).enemiesPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((DiscoveryRoomData)data).enemiesPath[i], arg0 =>
                {
                    SetGameObject(arg0, enemyPositions[i1].transform, gameObject.transform);
                });
            }
        }

        //初始化房间中其他环境对象
        public void InitOtherObjects()
        {
            for (int i = 0; i < ((DiscoveryRoomData)data).materialsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((DiscoveryRoomData)data).materialsPath[i], arg0 =>
                {
                    SetGameObject(arg0, materialPositions[i1].transform, gameObject.transform);
                });
            }
        }

        //初始化传送门对象
        public void InitTeleport()
        {
            for (int i = 0; i < ((DiscoveryRoomData)data).transformPointsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((DiscoveryRoomData)data).transformPointsPath[i], arg0 =>
                {
                    SetGameObject(arg0, TeleportPositions[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                });
            }
        }
    }
}
