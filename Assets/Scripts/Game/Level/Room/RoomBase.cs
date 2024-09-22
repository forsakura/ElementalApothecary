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
        /// ������Ʒ���ڵ��λ��
        /// </summary>
        /// <param name="gb">��Ʒ����</param>
        /// <param name="tr">��������</param>
        /// <param name="parentTransform">���󸸽ڵ�</param>
        protected void SetGameObject(GameObject gb, GameObjectInfo info, Transform parentTransform)
        {
            gb.transform.localPosition = info.position * 5;
            gb.transform.localRotation = Quaternion.Euler(info.rotation);
            gb.transform.localScale = info.scale * 5;
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
