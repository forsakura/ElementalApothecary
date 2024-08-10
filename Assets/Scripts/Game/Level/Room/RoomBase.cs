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

        //���͵����꼯��
        public List<Transform> TeleportPositions = new List<Transform>();
        
        
        /// <summary>
        /// ������Ʒ���ڵ��λ��
        /// </summary>
        /// <param name="gb">��Ʒ����</param>
        /// <param name="tr">��������</param>
        /// <param name="parentTransform">���󸸽ڵ�</param>
        protected void SetGameObject(GameObject gb, Transform  tr, Transform parentTransform)
        {
            gb.transform.position = tr.position;
            gb.transform.SetParent(parentTransform);
        }

        /// <summary>
        /// ���ô��͵�ű�����
        /// </summary>
        /// <param name="gb">���͵����</param>
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
