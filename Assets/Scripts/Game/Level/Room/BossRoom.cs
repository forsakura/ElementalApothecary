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
    //boss����
    public class BossRoom : RoomBase, IInitEnemy, IInitTeleport
    {
        //��ǰ�����boss��
        public string bossName;

        public Transform bossInitTransform;
        
        //��ǰ���䴫���ż���
        private List<GameObject> transformObjects = new List<GameObject>();
        
        private void Start()
        {
            data = new BossRoomData(gameObject.name);
            InitEnemies();
            InitTeleport();
            EventCenter.Instance.AddEventListener(bossName, UnlockRoom);
        }

        //��ʼ��boss����
        public void InitEnemies()
        {
            ResManager.LoadResourceAsync<GameObject>(((BossRoomData)data).bossPrefabPath, arg0 =>
            {
                SetGameObject(arg0, bossInitTransform, gameObject.transform);
            });
        }
        //��ʼ�����͵�
        public void InitTeleport()
        {
            for (int i = 0; i < ((BossRoomData)data).transformPointsPath.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((BossRoomData)data).transformPointsPath[i1], arg0 =>
                {
                    SetGameObject(arg0, TeleportPositions[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                    transformObjects.Add(arg0);
                });
            }
        }

        private void UnlockRoom()
        {
            foreach (var transformObject in transformObjects)
            {
                if (transformObject.CompareTag("Fight"))
                {
                    ((FightTransformPointData)transformObject.GetComponent<FightTransformPointView>().data)
                        .isTransform = true;
                }
            }
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventLister(bossName, UnlockRoom);
        }
    }
}
