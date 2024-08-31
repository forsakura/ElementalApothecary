using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSynthesis : MonoBehaviour
{
    ISynthesis synthesis;

    private void Start()
    {
        synthesis = GetComponent<Synthesis>();
    }

    private void Update()
    {
        
    }
}
