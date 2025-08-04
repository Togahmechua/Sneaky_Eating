using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseState : IState<Cat>
{
    public void OnEnter(Cat cat)
    {
        //Debug.Log("Loose");
    }

    public void OnExecute(Cat cat)
    {

    }


    public void OnExit(Cat cat)
    {

    }
}
