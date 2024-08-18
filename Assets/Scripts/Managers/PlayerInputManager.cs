using ProjectBase.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : SingletonByQing<PlayerInputManager>
{
    private PlayerInputActions PlayerInput;

    public PlayerInputActions.GamePlayActions GamePlay { get; private set; }
    public PlayerInputActions.FIghtUIActions FightUI { get; private set; }

    public PlayerInputManager()
    {
        PlayerInput = new PlayerInputActions();
        PlayerInput.Enable();
        GamePlay = PlayerInput.GamePlay;
        FightUI = PlayerInput.FIghtUI;
    }

    ~PlayerInputManager()
    { 
        PlayerInput.Disable();
    }
}
