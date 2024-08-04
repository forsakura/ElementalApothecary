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
            EventCenter.Instance.AddEventLister<float>("进度更新", Loading);
        }

        public void Loading(float progress)
        {
            GetControl<Slider>("加载进度条").value = progress;
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventLister<float>("进度更新", Loading);
        }
    }
}
