using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弃用！弃用！
public class PotionCast : MonoBehaviour
{
    //这里只实现一下药水丢出去的方法，显然执行层不在这
    float currentTime;

    public float Duration, MaxHeight;//默认数值
    //这样只要开始投掷后续就自动计算
    IEnumerator castCorotine(GameObject Potion,
                             Vector2 start,
                             Vector2 target,
                             float duration,
                             float maxHeight)
    {
        Potion.transform.position = Parabola.ClaculateCurrentPoint(start,
                                                                   target,
                                                                   currentTime,
                                                                   duration,
                                                                   maxHeight);
        currentTime += Time.deltaTime;
        if (currentTime > duration)
        {
            yield break;
        }
        yield return null;
    }

    //也完全可以重载让Duration, MaxHeight也可控
    public void ExecuteAtOnce(GameObject potionPrefab, Vector2 start, Vector2 target)
    {
        var Potion = Instantiate(potionPrefab);
        currentTime = 0;
        StartCoroutine(castCorotine(Potion, start, target, Duration, MaxHeight));
        Destroy(Potion);
    }

    //重载版让Duration, MaxHeight也可控
    public void ExecuteAtOnce(GameObject potionPrefab, Vector2 start, Vector2 target,float duration,float maxHeight)
    {
        var Potion = Instantiate(potionPrefab);
        currentTime = 0;
        StartCoroutine(castCorotine(Potion, start, target, duration, maxHeight));

        Destroy(Potion);
    }

    //如果有必要的话（比如持续改变Target）(?)
    public bool ControlledExecute(GameObject potionPrefab, Vector2 start, Vector2 target)
    {
        var Potion = Instantiate(potionPrefab);
        currentTime = 0;
        Potion.transform.position = Parabola.ClaculateCurrentPoint(start,
                                                           target,
                                                           currentTime,
                                                           Duration,
                                                           MaxHeight);
        currentTime += Time.deltaTime;
        if(currentTime> Duration)
        {
            Destroy(Potion);
            return false;//告知已结束
        }
        return true;
    }

    //嘛，先不做了
    public void GenerateZone(GameObject zonePrefab,Vector2 generatePos)
    {
        var zone = Instantiate(zonePrefab, generatePos, zonePrefab.transform.rotation);
    }
}
