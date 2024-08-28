using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public bool GetHit(HitInstance hit);
}
