
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    public Text chooseText;

    public Button chooseBtn;

    public int ID;

    public List<MessionDataSO> messionDataSOList;


    private void Awake() 
    {
        chooseBtn.onClick.AddListener(UpdateTalk);
    }

    public void UpdateTalk()
    {
        DialogueSystem.Instance.ChooseClickUpdateDialogueSpeech(ID);
        DialogueSystem.UpdateDialogueSpeech();

        if(MessionSystem.Instance != null)
        {
            MessionSystem.Instance.InstanceMessionBtn(messionDataSOList);  //有任务时需要

            foreach (var item in messionDataSOList)
            {
                MessionSystem.Instance.messionDataSOList.Add(item);
            }
        }
        
    }

}
