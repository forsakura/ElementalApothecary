using System;
using System.Collections.Generic;
using Game.Level.Room.Data;
using Game.Level.RoomInterface;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    
    /*
     * ̽������
     */
    public class DiscoveryRoom : RoomBase, IInitEnemy, IInitOtherObject, IInitTeleport
    {
        public List<Transform> materialsPoints = new List<Transform>();
        public List<Transform> enemiesTransforms = new List<Transform>();

        private void Start()
        {
            data = new DiscoveryRoomData(gameObject.name);
            InitEnemies();
            InitOtherObjects();
            InitTeleport();
        }

        //��ʼ�������ڷ�����
        public void InitEnemies()
        {
            for (int i = 0; i < ((DiscoveryRoomData)data).enemiesPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((DiscoveryRoomData)data).enemiesPath[i], arg0 =>
                {
                    SetGameObject(arg0, enemiesTransforms[i1].transform, gameObject.transform);
                });
            }
        }

        //��ʼ��������������������
        public void InitOtherObjects()
        {
            for (int i = 0; i < ((DiscoveryRoomData)data).materialsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((DiscoveryRoomData)data).materialsPath[i], arg0 =>
                {
                    SetGameObject(arg0, materialsPoints[i1].transform, gameObject.transform);
                });
            }
        }

        //��ʼ�������Ŷ���
        public void InitTeleport()
        {
            for (int i = 0; i < ((DiscoveryRoomData)data).transformPointsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((DiscoveryRoomData)data).transformPointsPath[i], arg0 =>
                {
                    SetGameObject(arg0, pointsTransforms[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                });
            }
        }
    }
}
