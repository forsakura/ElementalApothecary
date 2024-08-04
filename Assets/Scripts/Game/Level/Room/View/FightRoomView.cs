using System;
using System.Collections.Generic;
using Game.Level.Room.Data;
using Game.Level.TranslatePoints.Data;
using Game.Level.TranslatePoints.View;
using ProjectBase.Event;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room.View
{
    public class FightRoomView : RoomViewBase
    {
        //材料坐标集合
        public List<Transform> materialsPoints = new List<Transform>();

        //敌人坐标集合
        public List<Transform> enemiesTransforms = new List<Transform>();

        //敌人物品集合
        public List<GameObject> transformObjects = new List<GameObject>();

        public int enemyCount;

        private void Start()
        {
            data = new FightRoomData(gameObject.name);
            InitTransformPoints();
            InitMaterials();
            InitEnemies();
            EventCenter.Instance.AddEventListener(gameObject.name, DecreaseEnemyCount);
        }

        public override void InitTransformPoints()
        {
            base.InitTransformPoints();
            for (int i = 0; i < ((FightRoomData)data).transformPointsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((FightRoomData)data).transformPointsPath[i], arg0 =>
                {
                    SetGameObject(arg0, pointsTransforms[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                    transformObjects.Add(arg0);
                });
            }
        }

        public override void InitMaterials()
        {
            base.InitMaterials();
            for (int i = 0; i < ((FightRoomData)data).materialsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((FightRoomData)data).materialsPath[i], arg0 =>
                {
                    SetGameObject(arg0, materialsPoints[i1].transform, gameObject.transform);
                });
            }
        }

        public override void InitEnemies()
        {
            base.InitEnemies();
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

        private void DecreaseEnemyCount()
        {
            enemyCount--;
            if (enemyCount == 0)
            {
                foreach (var gb in transformObjects)
                {
                    if (gb.CompareTag("Fight"))
                    {
                        ((FightTransformPointData)gb.GetComponent<FightTransformPointView>().data).isTransform = true;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventLister(gameObject.name, DecreaseEnemyCount);
        }
    }
}
