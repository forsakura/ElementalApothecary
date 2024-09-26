using Game.Level.Room.Teleport.Data;
using Game.Level.Room.UI;
using ProjectBase.Event;
using ProjectBase.Scene;
using ProjectBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Level.Room.Teleport
{
    public class SpecialTeleportView : TeleportViewBase
    {
        private void Start()
        {
            data = new SpecialTeleportData(fileName);
            //Debug.Log(((SpecialTeleportData)data).destinationSceneName);
        }

        //?????????????????????§Ý?????????????????
        protected override void TransformToNext()
        {
            base.TransformToNext();
            Vector3 initPosition = ((SpecialTeleportData)data).destinationPoint;
            SceneMgr.Instance.LoadSceneAsync(((SpecialTeleportData)data).destinationSceneName, false, () =>
            {
                GameObject.Find("Player").transform.position = initPosition;
            });
        }

        public override void Interact()
        {
            base.Interact();
            TransformToNext();
        }

        /*protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player")&&((SpecialTeleportData)data).type==PointType.Enter)
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("´«ËÍ", TransformToNext);
            }
        }*/

        /*protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player")&&((SpecialTeleportData)data).type==PointType.Enter)
            {
                UIManager.Instance.HidePanel(tipPanelName);
                EventCenter.Instance.RemoveEventLister("´«ËÍ", TransformToNext);
            }
        }*/
    }
}

