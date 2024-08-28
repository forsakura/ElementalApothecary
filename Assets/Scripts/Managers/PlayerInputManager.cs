using ProjectBase.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : SingletonByQing<PlayerInputManager>
{
    private PlayerInputActions PlayerInput;
    private UIInputActions UIInput;

    public PlayerInputActions.GamePlayActions GamePlay { get; private set; }
    public UIInputActions.FIghtUIActions FightUI { get; private set; }

    public PlayerInputManager()
    {
        PlayerInput = new PlayerInputActions();
        PlayerInput.Enable();
        UIInput = new UIInputActions();
        UIInput.Enable();
        GamePlay = PlayerInput.GamePlay;
        FightUI = UIInput.FIghtUI;
    }

    ~PlayerInputManager()
    { 
        PlayerInput?.Disable();
        UIInput?.Disable();
    }
}
