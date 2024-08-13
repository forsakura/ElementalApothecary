using Game.Level.Room.Teleport.Data;
using Game.Level.Room.UI;
using ProjectBase.Event;
using ProjectBase.Scene;
using ProjectBase.UI;
using UnityEngine;

namespace Game.Level.Room.Teleport
{
    public class SpecialTeleportView : TeleportViewBase
    {
        private void Start()
        {
            data = new SpecialTeleportData(gameObject.name);
        }

        //?????????????????????§Ý?????????????????
        protected override void TransformToNext()
        {
            base.TransformToNext();
            UIManager.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_UI_Layer.system);
            SceneMgr.Instance.LoadSceneAsync(((SpecialTeleportData)data).nextSceneName, true, () =>
            {
                
            });
            GameObject.Find("Player").transform.position = ((SpecialTeleportData)data).destinationPoint.position;
            SceneMgr.Instance.UnloadSceneAsync(((SpecialTeleportData)data).currentSceneName, () =>
            {
                
            });
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player")&&((SpecialTeleportData)data).type==PointType.Enter)
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("?????", TransformToNext);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player")&&((SpecialTeleportData)data).type==PointType.Enter)
            {
                UIManager.Instance.HidePanel(tipPanelName);
                EventCenter.Instance.RemoveEventLister("?????", TransformToNext);
            }
        }
    }
}

