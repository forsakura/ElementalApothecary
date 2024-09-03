using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBarController : MonoBehaviour
{
    public Player player;

    Image bar;

    int maxCount;
    int currentCount;

    private void Start()
    {
        bar = GetComponent<Image>();
        maxCount = 1;
        currentCount = 0;
        bar.fillAmount = 1.0f * currentCount / maxCount;
        player.OnFill += OnBulletFill;
        player.OnShoot += OnBulletUse;
    }

    private void OnBulletFill(Characters go, int max)
    {
        maxCount = max;
        currentCount = maxCount;
        bar.fillAmount = 1.0f * currentCount / maxCount;
    }

    private void OnBulletUse(Characters go, int useCount)
    {
        currentCount -= useCount;
        bar.fillAmount = 1.0f * currentCount / maxCount;
    }
}
