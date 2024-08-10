using Game.Level.Room;
using Game.Level.TranslatePoints.Data;
using Game.Level.UI;
using ProjectBase.Event;
using ProjectBase.Scene;
using ProjectBase.UI;
using UnityEngine;

namespace Game.Level.TranslatePoints.View
{
    public class SpecialTransformPointView : TransformPointViewBase
    {
        private void Start()
        {
            data = new SpecialTransformPointData(gameObject.name);
        }

        //传送到下一坐标，会发生场景切换，所以需要加载面板
        protected override void TransformToNext()
        {
            base.TransformToNext();
            EventCenter.Instance.EventTrigger(data.fileName);
            var component = gameObject.GetComponentInParent<RoomBase>();
            EventCenter.Instance.EventTrigger(component.gameObject.name);
            UIManager.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_UI_Layer.system);
            SceneMgr.Instance.LoadSceneAsync(((SpecialTransformPointData)data).nextSceneName, true, () =>
            {
                
            });
            GameObject.Find("Player").transform.position = ((SpecialTransformPointData)data).destinationPoint.position;
            SceneMgr.Instance.UnloadSceneAsync(((SpecialTransformPointData)data).currentSceneName, () =>
            {
                
            });
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player")&&((SpecialTransformPointData)data).type==PointType.Enter)
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(((SpecialTransformPointData)data).tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("传送点", TransformToNext);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player")&&((SpecialTransformPointData)data).type==PointType.Enter)
            {
                UIManager.Instance.HidePanel(((SpecialTransformPointData)data).tipPanelName);
                EventCenter.Instance.RemoveEventLister("传送点", TransformToNext);
            }
        }
    }
}
