using System;
using Game.Level.TranslatePoints.Data;
using UnityEngine;

namespace Game.Level.TranslatePoints.View
{
    //显示隐藏，进行传送
    public class TransformPointViewBase : MonoBehaviour
    {

        public TransformPointDataBase data;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            
        }

        protected virtual void TransformToNext()
        {
            
        }
    }
}
