using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTreeView : GraphView
{
    public Action<DialogueView> OnDialogueSelected;
    public DialogueTreeSO dialogueTree;
    public class UxmlFactroy : UxmlFactory<DialogueTreeView,UxmlTraits>{}

    public DialogueTreeView()
    {
        Insert(0,new GridBackground());
        this.AddManipulator(new ContentZoomer());//放大缩小
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());//框选

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogueSystem_nuoyan/Editor/UI/DialogueTreeView.uss");
        styleSheets.Add(styleSheet);
    }


    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        var type = typeof(DialogueNode);


        evt.menu.AppendAction($"{type.Name}",(act) => CreatDialogue(type));
        
    }

    private void CreatDialogue(Type type)
    {
        DialogueNode node = dialogueTree.CreatDialogue(type);

        CreatDialogueView(node);
    }

    private void CreatDialogueView(DialogueNode node)
    {

        DialogueView dialogueView = new DialogueView(node);
        dialogueView.OnDialogueSelected = OnDialogueSelected;
        AddElement(dialogueView);
    }

    internal void PopulateDialogueTree(DialogueTreeSO dialogueTree)//构建树
    {
        graphViewChanged -= OnGraphViewChange;
        this.dialogueTree = dialogueTree;
        
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChange;

        if(dialogueTree != null)
            dialogueTree.dialogueNodeList.ForEach(view => CreatDialogueView(view));
    }

    public GraphViewChange OnGraphViewChange(GraphViewChange graphViewChange)
    {

        if(graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem => 
                                    {
                                        //删除节点的同时删除对应的data
                                        DialogueView dialogueView = elem as DialogueView;
                                        if(dialogueView != null)
                                        {
                                            dialogueTree.DeletDialogue(dialogueView.dialogueNode);
                                        }


                                        // Edge edge = elem as Edge;
                                        // if(edge != null)
                                        // {
                                        //     DialogueView parent = edge.output.node as DialogueView;
                                        //     DialogueView child = edge.input.node as DialogueView;
                                        //     foreach(Choose childView in parent.dialogueNode.chooseList)
                                        //     {
                                        //         if(childView.ID == parent.dialogueNode.ID)
                                        //             dialogueTree.RemoveChioce(parent.dialogueNode, childView);
                                        //     }
                                            
                                        // }
                                    }
                                                    );


                                    
        }

        // if(graphViewChange.edgesToCreate != null) 
        // {
        //     graphViewChange.edgesToCreate.ForEach(edge =>{

        //         DialogueView parent = edge.output.node as DialogueView;
        //         DialogueView child = edge.input.node as DialogueView;
        //         foreach(Choose childView in parent.dialogueNode.chooseList)
        //         {
        //             if(childView.ID == parent.dialogueNode.ID)
        //             {
        //                 dialogueTree.CreatEdge(parent.dialogueNode, childView);
        //             }
                    
        //         }
        //     });
        // }

        return graphViewChange;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)//检测输出端口是否兼容输入端口
    {
        return ports.ToList().Where(endport => endport.direction != startPort.direction && endport.node != startPort.node).ToList();
    }
}
