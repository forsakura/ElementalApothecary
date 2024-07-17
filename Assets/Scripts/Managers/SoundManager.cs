using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrameWork;
using FrameWork.Base;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoSingleton<SoundManager>
{
    /// <summary>
    /// 所有声音的源头的父物体
    /// </summary>
    public GameObject soundSystem;
    /// <summary>
    /// bgm
    /// </summary>
    public AudioSource bGMSource;
    /// <summary>
    /// bgm声音列表
    /// </summary>
    public List<AudioSource> bGmSoundsSources;
    /// <summary>
    /// 音效
    /// </summary>
    public AudioSource effectSoundSource;
    /// <summary>
    /// 音效列表
    /// </summary>
    public List<AudioSource> effectSoundsSources;
    /// <summary>
    /// 控制所有音量大小的滑动块控件
    /// </summary>
    public List<Slider> sliders;
    
    /// <summary>
    /// 全局音乐混合器
    /// </summary>
    public AudioMixer mansterMixer;

    /// <summary>
    /// 背景音乐混合器
    /// </summary>
    public AudioMixer bGMMixer;

    /// <summary>
    /// 音效混合器
    /// </summary>
    public AudioMixer soundEffectMixer;

    private void OnEnable()
    {
        DontDestroyOnLoad(soundSystem);
    }

    private void Start()
    {
        
        foreach (var VARIABLE in sliders)
        {
            VARIABLE.maxValue = 20f;
            VARIABLE.minValue = -20f;
            VARIABLE.value = 0.6f;
        }
    }

    /// <summary>
    /// 设置主音量音效音量
    /// </summary>
    /// <param name="val"></param>
    public void SetMasterMixer(float val)
    {
        mansterMixer.SetFloat("MasterVolume", val);
    }

    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="val"></param>
    /// <param name="i"></param>
    public void SetBGMMixer(float val,int i)
    {
        bGMMixer.SetFloat("BGMVolume", val);
    }

    /// <summary>
    /// 设置音效音量
    /// </summary>
    /// <param name="val"></param>
    public void SetSoundEffectMixer(float val)
    {
        soundEffectMixer.SetFloat("SoundEffectVolume", val);
    }

    /// <summary>
    /// bgm声源修改
    /// </summary>
    /// <param name="i"></param>
    public void ChangeBGM(int i)
    {
        bGMSource.clip = bGmSoundsSources[i].clip;
    }

    /// <summary>
    /// 音效音源修改
    /// </summary>
    /// <param name="i"></param>
    public void ChangeEffectSound(int i)
    {
        effectSoundSource.clip = effectSoundsSources[i].clip;
    }
}
