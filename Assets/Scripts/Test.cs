using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectBase.Scene;
using ProjectBase.UI;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneMgr.Instance);
        Debug.Log(UIManager.Instance);
    }
}
