using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour, IHitable
{
    Characters parent;

    private void Start()
    {
        parent = GetComponentInParent<Characters>();
        tag = transform.parent.tag;
    }

    public bool GetHit(HitInstance hit)
    {
        return parent.GetHit(hit);
    }
}