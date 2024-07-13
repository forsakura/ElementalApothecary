using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ã����ã�
public class PotionCast : MonoBehaviour
{
    //����ֻʵ��һ��ҩˮ����ȥ�ķ�������Ȼִ�в㲻����
    float currentTime;

    public float Duration, MaxHeight;//Ĭ����ֵ
    //����ֻҪ��ʼͶ���������Զ�����
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

    //Ҳ��ȫ����������Duration, MaxHeightҲ�ɿ�
    public void ExecuteAtOnce(GameObject potionPrefab, Vector2 start, Vector2 target)
    {
        var Potion = Instantiate(potionPrefab);
        currentTime = 0;
        StartCoroutine(castCorotine(Potion, start, target, Duration, MaxHeight));
        Destroy(Potion);
    }

    //���ذ���Duration, MaxHeightҲ�ɿ�
    public void ExecuteAtOnce(GameObject potionPrefab, Vector2 start, Vector2 target,float duration,float maxHeight)
    {
        var Potion = Instantiate(potionPrefab);
        currentTime = 0;
        StartCoroutine(castCorotine(Potion, start, target, duration, maxHeight));

        Destroy(Potion);
    }

    //����б�Ҫ�Ļ�����������ı�Target��(?)
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
            return false;//��֪�ѽ���
        }
        return true;
    }

    //��Ȳ�����
    public void GenerateZone(GameObject zonePrefab,Vector2 generatePos)
    {
        var zone = Instantiate(zonePrefab, generatePos, zonePrefab.transform.rotation);
    }
}
