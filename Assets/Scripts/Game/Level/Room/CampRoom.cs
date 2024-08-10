using Game.Level.Room.Data;
using Game.Level.RoomInterface;
using ProjectBase.Res;
using UnityEngine;

namespace Game.Level.Room
{
    //营地房间
    public class CampRoom : RoomBase, IInitTeleport, IInitOtherObject
    {
        private void Start()
        {
            data = new CampRoomData(gameObject.name);
            InitTeleport();
            InitOtherObjects();
        }

        public void InitTeleport()
        {
            for (int i = 0; i < ((CampRoomData)data).transformPointsPath.Count; i++)
            {
                int i1 = i;
                ResManager.LoadResourceAsync<GameObject>(((CampRoomData)data).transformPointsPath[i1], arg0 =>
                {
                    SetGameObject(arg0, pointsTransforms[i1].transform, gameObject.transform);
                    SetTransformView(arg0);
                });
            }
        }

        //生成营地内容相关的对象
        public void InitOtherObjects()
        {
            
        }
    }
}
