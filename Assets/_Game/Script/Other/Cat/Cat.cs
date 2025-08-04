using EasyTextEffects;
using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public bool isAbleToHit;

    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI countTxt;
    [SerializeField] private TextEffect countTextEff;

    [Header("State")]
    public IState<Cat> currentState;
    public IdleState idleState;
    public EatState eatState;
    public LooseState looseState;

    private Coroutine countCor;
    private Coroutine waitCor;

    #region State Machine
    private void Start()
    {
        idleState = new IdleState();
        eatState = new EatState();
        looseState = new LooseState();

        TransitionToState(idleState);
    }
    private void Update()
    {
        currentState?.OnExecute(this);
    }

    public void TransitionToState(IState<Cat> newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
    #endregion

    #region Animation
    public void ChangeAnim(string  name)
    {
        anim.Play(name);
    }

    public void Hit()
    {
        if (countCor != null)
        {
            StopCoroutine(countCor);
        }
        else if (waitCor != null)
        {
            StopCoroutine(waitCor);
        }

        TransitionToState(idleState);
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);

        ChangeAnim(CacheString.TAG_Hit_Cat);
    }

    public void Eat()
    {
        int rand = Random.Range(3, 6);
        waitCor = StartCoroutine(IEWait(rand));
    }

    private IEnumerator IEWait(int time)
    {
        yield return new WaitForSeconds(time);
        TransitionToState(eatState);
    }
    #endregion

    #region Animation Event
    public void StartCounting()
    {
        int rand = Random.Range(1, 3); // random từ 3 đến 6
        countCor = StartCoroutine(CountdownCoroutine(rand));
    }

    private IEnumerator CountdownCoroutine(int seconds)
    {
        while (seconds > 0)
        {
            countTxt.text = "0" + seconds.ToString();
            countTextEff.Refresh();
            yield return new WaitForSeconds(1f);
            seconds--;
        }

        countTxt.text = "00";
        countTextEff.Refresh();
        // Nếu bạn muốn làm gì đó sau khi đếm xong, thêm ở đây
        yield return new WaitForSeconds(1f); 
        Debug.Log("Lose");

        UIManager.Ins.TransitionUI<ChangeUICanvas, MainCanvas>(0.5f,
             () =>
             {
                 //LevelManager.Ins.DespawnLevel();
                 UIManager.Ins.OpenUI<LooseCanvas>();
             });
    }

    public void AbleToHit()
    {
        isAbleToHit = true;
    }

    public void UnAbleToHit()
    {
        isAbleToHit = false;
    }
    #endregion
}
