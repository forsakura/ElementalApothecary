using Game.Level.Room.RoomData;
using Game.Level.Room.Teleport;
using Game.Level.Room.Teleport.Data;
using UnityEngine;

namespace Game.Level.Room
{
    public abstract class RoomBase : MonoBehaviour
    {
        public string fileName;
        public RoomDataBase data;

        public abstract void LoadData();

        public abstract void SaveData();
        
        /// <summary>
        /// 设置物品父节点和位置
        /// </summary>
        /// <param name="gb">物品对象</param>
        /// <param name="tr">对象坐标</param>
        /// <param name="parentTransform">对象父节点</param>
        protected void SetGameObject(GameObject gb, GameObjectInfo info, Transform parentTransform)
        {
            gb.transform.localPosition = info.position * 5;
            gb.transform.localRotation = Quaternion.Euler(info.rotation);
            gb.transform.localScale = info.scale * 5;
            gb.transform.SetParent(parentTransform);
        }

        /// <summary>
        /// 设置传送点脚本类型
        /// </summary>
        /// <param name="gb">传送点对象</param>
        protected void SetTransformView(GameObject gb)
        {
            switch (gb.tag)
            {
                case "Common" :
                    gb.AddComponent<CommonTeleportView>();
                    break;
                case "Special" :
                    gb.AddComponent<SpecialTeleportView>();
                    break;
                case "Fight" :
                    gb.AddComponent<FightTeleportView>();
                    break;
            }
        }
    }
}
