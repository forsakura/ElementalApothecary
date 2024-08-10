using System;
using Game.Level.Room;
using Game.Level.Room.Data;
using Game.Level.TranslatePoints.Data;
using Game.Level.UI;
using ProjectBase.Event;
using ProjectBase.UI;
using UnityEngine;

namespace Game.Level.TranslatePoints.View
{
    public class CommonTransformPointView : TransformPointViewBase
    {
        private void Start()
        {
            data = new CommonTransformPointData(gameObject.name);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player"))
            {
                UIManager.Instance.ShowPanel<TranslateTipPanel>(((CommonTransformPointData)data).tipPanelName, E_UI_Layer.system);
                EventCenter.Instance.AddEventListener("���͵�", TransformToNext);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            if (other.CompareTag("Player"))
            {
                UIManager.Instance.HidePanel(((CommonTransformPointData)data).tipPanelName);
                EventCenter.Instance.RemoveEventLister("���͵�", TransformToNext);
            }
        }

        protected override void TransformToNext()
        {
            base.TransformToNext();
            //���浱ǰ���͵�����
            EventCenter.Instance.EventTrigger(data.fileName);
            //���淿������
            var component = gameObject.GetComponentInParent<RoomBase>();
            EventCenter.Instance.EventTrigger(component.gameObject.name);
            GameObject.Find("Player").transform.position = ((CommonTransformPointData)data).destinationPoint.position;
        }
    }
}
