using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
[CreateAssetMenu( menuName = "对话/DialogueNode"),Serializable]
public class DialogueNode : ScriptableObject
{
    [Tooltip("一个独有ID")]public int ID;
	[Tooltip("左边说话者")]public string speakerNameLeft;
	[Tooltip("右边说话者")]public string speakerNameRight;
	[Tooltip("左边角色头像或者立绘")][JsonIgnore]public Sprite speakerLeft;
	[Tooltip("右边角色头像或者立绘")][JsonIgnore]public Sprite speakerRight;
	[Tooltip("对话列表")]public List<Dialogue> dialogueList;

	[HideInInspector]public Vector3 position;
}

[Serializable]
public class Dialogue
{
	[Tooltip("选择角色方向")]public Row row;
	[TextArea(5,4)]
	[Tooltip("角色说的话")]public string speech;
	[Tooltip("自定义对话是否有选项列表，可多个")]public List<Choose> chooseList = new List<Choose>();//对话是否有选项
}
[Serializable]
public class Choose
{
    [Tooltip("跳转到相应对话节点的ID")]public int ID;
    [Tooltip("选项中的文字描述")]public string chooseBtnText;

	[Tooltip("自定义是否有任务吗，可多个")]public List<Mession> messionList = new List<Mession>();
}

[Serializable]
public class Mession
{
	[Tooltip("任务")]public MessionDataSO messionDataSO;
}


