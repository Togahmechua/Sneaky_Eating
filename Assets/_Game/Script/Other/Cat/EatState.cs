using UnityEngine;

public class EatState : IState<Cat>
{
    public void OnEnter(Cat cat)
    {
        //Debug.Log("Eat");
        cat.ChangeAnim(CacheString.TAG_Eat_Cat);
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
