using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    private Dictionary<string, State> states = new();
    private State currentState = null;
    private State previousState = null;

    //public string Name
    //{
    //    get => currentState.Name;
    //    set
    //    {
    //        if (value == Name || !states.TryGetValue(value, out State nextState))
    //        {
    //            return;
    //        }
    //        currentState.Exit();
    //        currentState = nextState;
    //        currentState.Enter();
    //    }
    //}

    public void AddState(string name, State state)
    {
        states.Add(name, state);
        state.Name = name;
    }

    public void SetInitState(string name)
    {
        if (states.TryGetValue(name, out currentState))
        {
            currentState.Enter();
        }

    }

    public void Excute()
    {
        if (currentState == null)
        {
            return;
        }
        currentState.Stay();
        // 当Func<bool>()返回true时说明需要转换状态了
        foreach (var item in currentState.LeavingCondition.Where(item => item.Value()))
        {
            if (states.TryGetValue(item.Key, out State next))
            {
                previousState = currentState;
                currentState.Exit();
                currentState = next;
                currentState.Enter();
                return;
            }
        }
    }

    public State GetState(string name)
    {
        return states[name];
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public State GetPreviousState()
    {
        return previousState;
    }

    public void Clear()
    {
        states.Clear();
    }
}


public class State
{
    public string Name;
    public Action Enter = () => { };
    public Action Stay = () => { };
    public Action Exit = () => { };

    // 离开当前状态机的条件
    public Dictionary<string, Func<bool>> LeavingCondition = new();

    public State SetEnter(Action action)
    {
        Enter = action;
        return this;
    }

    public State SetStay(Action action)
    {
        Stay = action;
        return this;
    }

    public State SetExit(Action action)
    {
        Exit = action;
        return this;
    }

    public State AddLeavingCondition(string name,  Func<bool> condition)
    {
        LeavingCondition.Add(name, condition);
        return this;
    }
}
