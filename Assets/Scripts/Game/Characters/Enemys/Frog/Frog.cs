using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Frog
{
    public class Frog : EnemyBase
    {
        // Start is called before the first frame update
        void Start()
        {
            AfterGetHit += HaveTaunt;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}