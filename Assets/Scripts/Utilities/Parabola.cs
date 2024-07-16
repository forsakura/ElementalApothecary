using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parabola
{
    /// <summary>
    /// ���������ߵĺ�������������������Ʒ����ֵȲ�����ҿ��Ƶ���Ʒ��Ծ
    /// </summary>
    /// <param name="start">��ʼ����</param>
    /// <param name="target">�յ�����</param>
    /// <param name="currentTime">��ǰ���˶�ʱ��</param>
    /// <param name="duration">�˶���Ҫ����ʱ��</param>
    /// <param name="maxHeight">�����������ʼ�յ��������߶�ƫ��</param>
    /// <returns>���ص�ǰʱ����Ʒ������</returns>
    public static Vector2 ClaculateCurrentPoint( Vector2 start, Vector2 target, float currentTime, float duration, float maxHeight)
    {
        float normalizedTime = currentTime / duration;
        Vector2 basePos = Vector2.Lerp(start, target, normalizedTime);
        // �򵥵�0��1Ϊ��㿪�ڳ��µ������� x * (1 - x), x = 0.5 ʱ��yMax = 0.25
        return new Vector2( basePos.x, basePos.y + (maxHeight * normalizedTime * (1 - normalizedTime) / 0.25f) );
    }
}
