using Game.Level.Room.Teleport.Data;
using UnityEngine;

namespace Game.Level.Room.Teleport
{
    //???????????§Õ???
    public class TeleportViewBase : MonoBehaviour
    {
        public string tipPanelName;

        public TeleportDataBase data;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            
        }

        protected virtual void TransformToNext()
        {
            
        }
    }
}
