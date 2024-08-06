using System;
using ProjectBase.Event;
using ProjectBase.UI;
using UnityEngine.UI;

namespace Game.Level.UI
{
    public class LoadingPanel : BasePanel
    {
        private void Start()
        {
            EventCenter.Instance.AddEventLister<float>("���ȸ���", Loading);
        }

        public void Loading(float progress)
        {
            GetControl<Slider>("���ؽ�����").value = progress;
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventLister<float>("���ȸ���", Loading);
        }
    }
}
