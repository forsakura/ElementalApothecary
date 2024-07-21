using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [Tooltip("说话者左侧")]public Text speakerNameLeft;
    [Tooltip("说话者右侧")]public Text speakerNameRight;
    [Tooltip("说话内容")]public Text speech;
    [Header("------")]
    [Tooltip("说话角色的立绘或者头像左侧")]public Image rowImageLeft;
    [Tooltip("说话角色的立绘或者头像右侧")]public Image rowImageRight;
    [Header("------")]
    [Tooltip("继续下一句话按钮")]public Button continueSpeechBtn;
    [Header("------")]
    [Tooltip("生成选项预制体")]public ChooseButton chooseButtonPre;
    [Header("------")]
    [Tooltip("选择按钮的生成位置")]public Transform chooseParent;
    [Tooltip("角色立绘位置")]public Transform rowDetailsTrans;

    private void Awake() 
    {
        continueSpeechBtn.onClick.AddListener(DialogueSystem.UpdateDialogueSpeech);
    }


    

    
}
