using System;
using Game.Level.Room.Data;
using Game.Level.RoomInterface;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    //强化房间
    public class StrengtheningRoom : RoomBase, IInitTeleport, IInitOtherObject
    {
        private void Start()
        {
            data = new StrengtheningRoomData(gameObject.name);
            InitTeleport();
            InitOtherObjects();
        }

        public void InitTeleport()
        {
            for (int i = 0; i < ((StrengtheningRoomData)data).transformPointsPath.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((StrengtheningRoomData)data).transformPointsPath[i1], arg0 =>
                {
                    SetGameObject(arg0, TeleportPositions[i1], gameObject.transform);
                    SetTransformView(arg0);
                });
            }
        }

        public void InitOtherObjects()
        {
           
        }
    }
}
