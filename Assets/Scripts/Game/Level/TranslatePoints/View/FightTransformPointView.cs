using System;
using Game.Level.TranslatePoints.Data;
using Game.Level.UI;
using ProjectBase.Event;
using ProjectBase.UI;
using UnityEngine;

namespace Game.Level.TranslatePoints.View
{
    public class FightTransformPointView : TransformPointViewBase
    {
        private void Start()
        {
            data = new FightTransformPointData(gameObject.name);
        }

        //传送到下一坐标
        protected override void TransformToNext()
        {
            base.TransformToNext();
            GameObject.Find("Player").transform.position = ((FightTransformPointData)data).destinationPoint.position;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player")&&((FightTransformPointData)data).isTransform)
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("传送点", TransformToNext);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player") && ((FightTransformPointData)data).isTransform)
            {
                UIManager.Instance.HidePanel(tipPanelName);
                EventCenter.Instance.RemoveEventLister("传送点", TransformToNext);
            }
        }
    }
}
