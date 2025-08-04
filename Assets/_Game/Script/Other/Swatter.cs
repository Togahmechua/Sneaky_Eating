using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swatter : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public int MaxHP = 3;

    public bool isDead;

    public void PlayAnim()
    {
        anim.Play(CacheString.TAG_Attack_Swatter);
    }

    private void TakeDamge()
    {
        MaxHP--;
        if (MaxHP <= 0)
        {
            isDead = true;
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.dead);

            UIManager.Ins.mainCanvas.Hit();
            Debug.Log("Die");

            StartCoroutine(IELose());
        }
        else
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.hurt);

            UIManager.Ins.mainCanvas.Hit();
            Debug.Log("Take Damge");
        }


        UIManager.Ins.mainCanvas.SetHealth(MaxHP);
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
    }

    private IEnumerator IELose()
    {
        yield return new WaitForSeconds(1.5f);

        UIManager.Ins.TransitionUI<ChangeUICanvas, MainCanvas>(0.5f,
               () =>
               {
                   //LevelManager.Ins.DespawnLevel();
                   UIManager.Ins.OpenUI<LooseCanvas>();
               });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Cat cat = Cache.GetCat(other);
        if (cat != null)
        {
            cat.Hit();
            if (!cat.isAbleToHit)
            {
                TakeDamge();
            }
        }
    }
}
