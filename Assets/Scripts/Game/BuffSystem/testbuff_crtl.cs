using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbuff_crtl : MonoBehaviour
{
    IBuffManager buffManager;
    public BuffSO buffSO;
    // Start is called before the first frame update
    void Start()
    {
        buffManager = GetComponent<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            buffManager.Add(buffSO.GetBuffById(1));
            Debug.Log("Space!!");
        }
    }
}
