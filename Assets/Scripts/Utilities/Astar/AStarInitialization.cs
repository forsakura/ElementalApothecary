using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarPathFinding
{
    public class AStarInitialization : MonoBehaviour
    {
        private static AStarInitialization instance;

        public static AStarInitialization Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<AStarInitialization>();
                }
                else
                {
                    Destroy(instance.gameObject);
                }
                return instance;
            }
        }


    }
}

