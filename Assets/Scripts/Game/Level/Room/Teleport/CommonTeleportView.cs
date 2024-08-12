using Game.Level.Room.UI;
using Game.Level.TranslatePoints.Data;
using ProjectBase.Event;
using ProjectBase.UI;
using UnityEngine;

namespace Game.Level.Room.Teleport
{
    public class CommonTeleportView : TeleportViewBase
    {
        private void Start()
        {
            data = new CommonTeleportData(gameObject.name);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player"))
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("?????", TransformToNext);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player"))
            {
                UIManager.Instance.HidePanel(tipPanelName);
                EventCenter.Instance.RemoveEventLister("?????", TransformToNext);
            }
        }

        protected override void TransformToNext()
        {
            base.TransformToNext();
            GameObject.Find("Player").transform.position = ((CommonTeleportData)data).destinationPoint.position;
        }
    }
}