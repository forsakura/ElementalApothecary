using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase.Scene;
using UnityEngine.SceneManagement;
using ProjectBase.UI;

public class OnLeaveCampsite : PlayerInteraction
{
    public Vector3 targetPos;
    public override void Interact()
    {
        //PlayerController.Instance.transform.position = targetPos;
        //UIManager.Instance.ShowPanel<FightingUIPanel>("FightingUI", E_UI_Layer.top);
        //SceneManager.LoadScene("Sample Level");
        SceneMgr.Instance.LoadSceneAsync("Sample Level", false, () =>
        {
            PlayerController.Instance.transform.position = targetPos;
            UIManager.Instance.ShowPanel<FightingUIPanel>("FightingUI", E_UI_Layer.top);
        });
    }
}
