using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parabola
{
    /// <summary>
    /// 计算抛物线的函数，可以用于抛射物品，或怪等不受玩家控制的物品跳跃
    /// </summary>
    /// <param name="start">起始坐标</param>
    /// <param name="target">终点坐标</param>
    /// <param name="currentTime">当前已运动时间</param>
    /// <param name="duration">运动需要的总时间</param>
    /// <param name="maxHeight">抛物线相对起始终点连线最大高度偏移</param>
    /// <returns>返回当前时间物品的坐标</returns>
    public static Vector2 ClaculateCurrentPoint( Vector2 start, Vector2 target, float currentTime, float duration, float maxHeight)
    {
        float normalizedTime = currentTime / duration;
        Vector2 basePos = Vector2.Lerp(start, target, normalizedTime);
        // 简单的0，1为零点开口朝下的抛物线 x * (1 - x), x = 0.5 时，yMax = 0.25
        return new Vector2( basePos.x, basePos.y + (maxHeight * normalizedTime * (1 - normalizedTime) / 0.25f) );
    }
}
