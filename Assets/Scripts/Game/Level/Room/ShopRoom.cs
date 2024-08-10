using System;
using Game.Level.Room.Data;
using Game.Level.RoomInterface;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    //�̵귿��
    public class ShopRoom : RoomBase, IInitTeleport, IInitOtherObject
    {
        private void Start()
        {
            data = new ShopRoomData(gameObject.name);
        }

        public void InitTeleport()
        {
            for (int i = 0; i < ((ShopRoomData)data).transformPointsPath.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((ShopRoomData)data).transformPointsPath[i1], arg0 =>
                {
                    SetGameObject(arg0, TeleportPositions[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                });
            }
        }

        //��ʼ���̵��еĶ���
        public void InitOtherObjects()
        {
            
        }
    }
}
