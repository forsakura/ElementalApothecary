using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider Global;
    public Slider Background;
    public Slider Effect;

    public Button Back;

    private void OnEnable() 
    {
        SoundManager.Instance.sliders.Add(Global);
        SoundManager.Instance.sliders.Add(Background);
        SoundManager.Instance.sliders.Add(Effect);

        SoundManager.Instance.SetBGMMixer(Global.value,1);
    }
    private void OnDestroy()
    {
        SoundManager.Instance.sliders.Remove(Global);
        SoundManager.Instance.sliders.Remove(Background);
        SoundManager.Instance.sliders.Remove(Effect);
    }
}
