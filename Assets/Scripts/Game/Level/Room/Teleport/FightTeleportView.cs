using Game.Level.Room.Teleport.Data;
using Game.Level.Room.UI;
using ProjectBase.Event;
using ProjectBase.UI;
using UnityEngine;

namespace Game.Level.Room.Teleport
{
    public class FightTeleportView : TeleportViewBase
    {
        private void Start()
        {
            data = new FightTeleportData(gameObject.name);
        }

        //????????????
        protected override void TransformToNext()
        {
            base.TransformToNext();
            GameObject.Find("Player").transform.position = ((FightTeleportData)data).destinationPoint.position;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player")&&((FightTeleportData)data).isTransform)
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("?????", TransformToNext);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player") && ((FightTeleportData)data).isTransform)
            {
                UIManager.Instance.HidePanel(tipPanelName);
                EventCenter.Instance.RemoveEventLister("?????", TransformToNext);
            }
        }
    }
}

