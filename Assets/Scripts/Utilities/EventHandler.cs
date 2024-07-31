using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class EventHandler
{
    public static event Action<InventoryLocation, List<LegacyInventoryItem>> UpdateInventoryUI;
    public static void CallUpdateInventoryUI(InventoryLocation location, List<LegacyInventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);
    }

    public static event Action<int, Vector3> InstantiateItemInScene;
    public static void CallInstantiateItemInScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }

    public static event Action<int, Vector3, ItemType> DropItemEvent;
    public static void CallDropItemEvent(int ID, Vector3 pos, ItemType itemType)
    {
        DropItemEvent?.Invoke(ID, pos, itemType);
    }

    public static event Action<LegacyItemDetails, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(LegacyItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<string, Vector3> TransitionEvent;
    public static void CallTransitionEvent(string sceneName, Vector3 pos)
    {
        TransitionEvent?.Invoke(sceneName, pos);
    }

    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<Vector3> MoveToPosition;
    public static void CallMoveToPosition(Vector3 targetPosition)
    {
        MoveToPosition?.Invoke(targetPosition);
    }

    public static event Action<Vector3, LegacyItemDetails> MouseClickedEvent;
    public static void CallMouseClickedEvent(Vector3 pos, LegacyItemDetails itemDetails)
    {
        MouseClickedEvent?.Invoke(pos, itemDetails);
    }

    public static event Action<Vector3, LegacyItemDetails> ExecuteActionAfterAnimation;
    public static void CallExecuteActionAfterAnimation(Vector3 pos, LegacyItemDetails itemDetails)
    {
        ExecuteActionAfterAnimation?.Invoke(pos, itemDetails);
    }

    public static event Action<int> HarvestAtPlayerPosition;
    public static void CallHarvestAtPlayerPosition(int ID)
    {
        HarvestAtPlayerPosition?.Invoke(ID);
    }

    public static event Action RefreshCurrentMap;
    public static void CallRefreshCurrentMap()
    {
        RefreshCurrentMap?.Invoke();
    }


    public static event Action GenerateCropEvent;
    public static void CallGenerateCropEvent()
    {
        GenerateCropEvent?.Invoke();
    }

    //public static event Action<DialoguePiece> ShowDialogueEvent;
    //public static void CallShowDialogueEvent(DialoguePiece piece)
    //{
    //    ShowDialogueEvent?.Invoke(piece);
    //}

    
    public static event Action<ContainerType, InventoryBag_SO> BaseBagOpenEvent;
    public static void CallBaseBagOpenEvent(ContainerType slotType, InventoryBag_SO bag_SO)
    {
        BaseBagOpenEvent?.Invoke(slotType, bag_SO);
    }

    public static event Action<ContainerType, InventoryBag_SO> BaseBagCloseEvent;
    public static void CallBaseBagCloseEvent(ContainerType slotType, InventoryBag_SO bag_SO)
    {
        BaseBagCloseEvent?.Invoke(slotType, bag_SO);
    }

    //public static event Action<GameState> UpdateGameStateEvent;
    //public static void CallUpdateGameStateEvent(GameState gameState)
    //{
    //    UpdateGameStateEvent?.Invoke(gameState);
    //}

    public static event Action<LegacyItemDetails, bool> ShowTradeUI;
    public static void CallShowTradeUI(LegacyItemDetails item, bool isSell)
    {
        ShowTradeUI?.Invoke(item, isSell);
    }
    public static event Action<InventoryLocation, int, InventoryLocation, int> ShowAltarUI;
    public static void CallShowAltarUI(InventoryLocation locationFrom, int fromIndex, InventoryLocation locationTarget, int targetIndex)
    {
        ShowAltarUI?.Invoke(locationFrom, fromIndex, locationTarget, targetIndex);
    }

    


    public static event Action<int> StartNewGameEvent;
    public static void CallStartNewGameEvent(int index)
    {
        StartNewGameEvent?.Invoke(index);
    }
    public static event Action EndGameEvent;
    public static void CallEndGameEvent()
    {
        EndGameEvent?.Invoke();
    }
}
