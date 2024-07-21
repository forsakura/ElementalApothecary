using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CreateAssetMenu(menuName = "对话/DialogueTree")]
public class DialogueTreeSO : ScriptableObject
{
    public List<DialogueNode> dialogueNodeList;

    private Dictionary<int,DialogueNode> DialogueNodeDic;
	public Dictionary<int,DialogueNode>  dialogueNodeDic=> DialogueNodeDic;
	

	public void Initialization()
	{
		DialogueNodeDic = new Dictionary<int, DialogueNode>();

		foreach (var dialogueNode in dialogueNodeList)
		{
			DialogueNodeDic.Add(dialogueNode.ID,dialogueNode);
		}
	}

#if UNITY_EDITOR
	public DialogueNode CreatDialogue(System.Type type)
	{
		DialogueNode node = ScriptableObject.CreateInstance<DialogueNode>() as DialogueNode;
		node.name = type.Name;
		dialogueNodeList.Add(node);
		if(!Application.isPlaying)
		{
			AssetDatabase.AddObjectToAsset(node, this);
		}
		AssetDatabase.SaveAssets();
		return node;
	}

	public DialogueNode DeletDialogue(DialogueNode node)
	{
		dialogueNodeList.Remove(node);
		AssetDatabase.RemoveObjectFromAsset(node);
		AssetDatabase.SaveAssets();

		return node;
	}

	
	
	public void ReName()
	{
		foreach (var item in dialogueNodeList)
		{
			AssetDatabase.RenameAsset("Assets/DialogueSystem_nuoyan/Data/Dialogue/DialogueNode", item.ID.ToString());
		}
		AssetDatabase.SaveAssets();
	}
	
#endif

}
