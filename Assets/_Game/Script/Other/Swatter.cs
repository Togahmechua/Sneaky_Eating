using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swatter : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Update()
    {
#if UNITY_EDITOR || UNITY_IOS
        if (Input.GetMouseButtonDown(0))
        {
            PlayAnim();
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlayAnim();
        }
#endif
    }

    public void PlayAnim()
    {
        anim.Play(CacheString.TAG_Attack_Swatter);
    }
}
