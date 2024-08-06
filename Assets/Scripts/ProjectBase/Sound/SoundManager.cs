using ProjectBase.Mono;
using UnityEngine;

namespace ProjectBase.Sound
{
    /*
     * 音效管理器 对音效进行管理；
     * 包括对背景音乐，音效的播放，暂停，音源的替换，音量设置，音速调节；
     * 添加和删除新音源。                           --By 棾
     */
    public enum SourceType
    {
        BGM,
        SoundEffect
    }
    public class SoundManager : SingletonByQing<SoundManager>
    {
        
        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="audioSource">需要播放的背景音源</param>
        /// <param name="delay">延迟播放时间（默认0秒）</param>
        public void PlayBGM(AudioSource audioSource, ulong delay = 0)
        {
            if (audioSource == null)
            {
                audioSource = new GameObject("AudioSource of BGM").AddComponent<AudioSource>();
            }
            audioSource.Play(delay);
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioSource">需要播放的音效音源</param>
        /// <param name="delay">延迟播放时间（默认0秒）</param>
        public void PlaySoundEffect(AudioSource audioSource, ulong delay = 0)
        {
            if (audioSource == null)
            {
                audioSource = new GameObject("AudioSource of SoundEffect").AddComponent<AudioSource>();
            }
            audioSource.Play(delay);
        }
        
        /// <summary>
        /// 暂停声音播放
        /// </summary>
        /// <param name="audioSource">需要暂停的音源</param>
        public void PauseSound(AudioSource audioSource)
        {
            if (audioSource == null) return;
            audioSource.Pause();
        }
        
        /// <summary>
        /// 停止声音播放
        /// </summary>
        /// <param name="audioSource">需要停止的音源</param>
        public void StopSound(AudioSource audioSource)
        {
            if (audioSource == null) return;
            audioSource.Stop();
        }

        /// <summary>
        /// 添加音源
        /// </summary>
        /// <param name="clip">需要的声音片段</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="type">添加的是音源类型</param>
        /// <param name="parentTransform">父物体变换组件</param>
        public void AddSource(AudioClip clip, bool isLoop, SourceType type, Transform parentTransform)
        {
            var audioSource = type switch
            {
                SourceType.BGM => new GameObject("AudioSource of BGM " + clip.name).AddComponent<AudioSource>(),
                SourceType.SoundEffect => new GameObject("AudioSource of SoundEffect " + clip.name)
                    .AddComponent<AudioSource>(),
                _ => null
            };

            if (audioSource == null) return;
            audioSource.gameObject.transform.SetParent(parentTransform);
            audioSource.clip = clip;
            audioSource.loop = isLoop;
        }

        /// <summary>
        /// 移除音源
        /// </summary>
        /// <param name="audioSource">需要移除的音源</param>
        public void RemoveSource(AudioSource audioSource)
        {
            if (audioSource == null) return;
            audioSource.Stop();
            Object.Destroy(audioSource.gameObject);
        }
        
        /// <summary>
        /// 修改音源的信息（包括音源，是否循环，音量，音速）
        /// </summary>
        /// <param name="audioSource">需修改的音源</param>
        /// <param name="clip">需改成的音乐片段</param>
        /// <param name="isLoop">是否循环</param>
        public void ChangeSound(AudioSource audioSource, AudioClip clip, bool isLoop)
        {
            if (audioSource == null) return;
            audioSource.clip = clip;
            audioSource.loop = isLoop;
        }
        public void ChangeSound(AudioSource audioSource, AudioClip clip, bool isLoop, float value)
        {
            if (audioSource == null) return;
            audioSource.clip = clip;
            audioSource.loop = isLoop;
            audioSource.volume = value;
        }
        public void ChangeSound(AudioSource audioSource, AudioClip clip, bool isLoop, float value, float pitch)
        {
            if (audioSource == null) return;
            audioSource.clip = clip;
            audioSource.loop = isLoop;
            audioSource.volume = value;
            audioSource.pitch = pitch;
        }
    }
}
