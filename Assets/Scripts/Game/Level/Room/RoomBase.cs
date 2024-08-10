using System.Collections.Generic;
using Game.Level.Room.Data;
using Game.Level.TranslatePoints.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Level.Room
{
    public class RoomBase : MonoBehaviour
    {
        public RoomDataBase data;

        //传送点坐标集合
        public List<Transform> TeleportPositions = new List<Transform>();
        
        
        /// <summary>
        /// 设置物品父节点和位置
        /// </summary>
        /// <param name="gb">物品对象</param>
        /// <param name="tr">对象坐标</param>
        /// <param name="parentTransform">对象父节点</param>
        protected void SetGameObject(GameObject gb, Transform  tr, Transform parentTransform)
        {
            gb.transform.position = tr.position;
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
                    gb.AddComponent<CommonTransformPointView>();
                    break;
                case "Special" :
                    gb.AddComponent<SpecialTransformPointView>();
                    break;
                case "Fight" :
                    gb.AddComponent<FightTransformPointView>();
                    break;
            }
        }

    }
}
