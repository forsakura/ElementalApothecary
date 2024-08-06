using System;
using System.Collections.Generic;
using Game.Level.Room.Data;
using Game.Level.TranslatePoints.Data;
using Game.Level.TranslatePoints.View;
using ProjectBase.Res;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Level.Room.View
{
    public class CommonRoomView : RoomViewBase
    {
        //材料坐标集合
        public List<Transform> materialsPoints = new List<Transform>();


        private void Start()
        {
            data = new CommonRoomData(gameObject.name);
            InitMaterials();
            InitTransformPoints();
        }

        public override void InitMaterials()
        {
            base.InitMaterials();
            for (int i = 0; i < ((CommonRoomData)data).materialsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((CommonRoomData)data).materialsPath[i], arg0 =>
                {
                    SetGameObject(arg0, materialsPoints[i1].transform, gameObject.transform);
                });
            }
        }

        public override void InitTransformPoints()
        {
            base.InitTransformPoints();
            for (int i = 0; i < ((CommonRoomData)data).transformPointsPath.Count; i++)
            {
                var i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((CommonRoomData)data).transformPointsPath[i], arg0 =>
                {
                    SetGameObject(arg0, pointsTransforms[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                });
            }
        }
    }
}
