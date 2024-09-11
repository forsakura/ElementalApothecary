using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase.Scene;
using UnityEngine.SceneManagement;

public class OnLeaveCampsite : PlayerInteraction
{
    public Vector3 targetPos;
    public override void Interact()
    {
        PlayerController.Instance.transform.position = targetPos;
        SceneManager.LoadScene("Sample Level");
        //SceneMgr.Instance.LoadSceneAsync("Sample Level", false, () =>{
        //    PlayerController.Instance.transform.position = targetPos;
        //});
    }
}
