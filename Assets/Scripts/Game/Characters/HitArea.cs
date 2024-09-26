using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour, IHitable
{
    protected Characters parent;

    protected virtual void Start()
    {
        parent = GetComponentInParent<Characters>();
        tag = transform.parent.tag;
    }

    public virtual bool GetHit(HitInstance hit)
    {
        return parent.GetHit(hit);
    }
}
