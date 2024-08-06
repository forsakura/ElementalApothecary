using UnityEditor.Experimental.GraphView;
using System;
using UnityEngine;

public class DialogueView : Node
{
    public DialogueNode dialogueNode;
    public Port input;
    public Port output;

    public Action<DialogueView> OnDialogueSelected;

    public DialogueView(DialogueNode node):base("Assets/DialogueSystem_nuoyan/Editor/UI/DialogueNodeView.uxml")
    {
        dialogueNode = node;
        this.title = node.name;
        this.viewDataKey = node.ID.ToString();
        style.left = node.position.x;
        style.top = node.position.y;

        CreatInputPort();
        CreatOutputPort();
    }


    /*将节点入口设置为 
        接口链接方向 横向Orientation.Vertical  竖向Orientation.Horizontal
        接口可链接数量 Port.Capacity.Single
        接口类型 typeof(bool)
    */
    // 默认所有节点为多入口类型
    public void CreatInputPort()
    {
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        if(input != null)
        {
            // 将端口名设置为空
            input.portName = "";
            inputContainer.Add(input);
        }
    }
    public void CreatOutputPort()
    {
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        if(output != null)
        {
            output.portName = "";
            outputContainer.Add(output);
        }
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if(OnDialogueSelected != null)
            OnDialogueSelected.Invoke(this);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        dialogueNode.position.x = newPos.xMin;
        dialogueNode.position.y = newPos.yMin;
    }
}
