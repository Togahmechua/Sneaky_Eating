using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Cat>
{
    public void OnEnter(Cat cat)
    {
        //Debug.Log("Idle State");
        cat.Eat();
    }

    public void OnExecute(Cat cat)
    {
        if (LevelManager.Ins.curLevel.swatter.isDead)
        {
            cat.TransitionToState(cat.looseState);
        }
    }

    public void OnExit(Cat cat)
    {
        
    }
}
