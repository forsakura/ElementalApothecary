using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSynthesis : MonoBehaviour
{
    ISynthesis synthesis;
    

    private void Start()
    {
        synthesis = GetComponent<Synthesis>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            IDataItem item = synthesis.output();
            item.DebugDisplayData();
        }
    }
}
