using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : UICanvas
{
    [Header("===Effect===")]
    [SerializeField] private Image roateImg;
    [SerializeField] float speed = 90f;

    [Header("---Other Button---")]
    //[SerializeField] private Button nextBtn;
    [SerializeField] private Button menuBtn;

    private bool isClick;

    private void OnEnable()
    {
        //AudioManager.Ins.PlaySFX(AudioManager.Ins.win2);
    }

    private void Start()
    {

        menuBtn.onClick.AddListener(() =>
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, WinCanvas>(0.6f,
                () =>
                {
                    //LevelManager.Ins.DespawnMap();
                    UIManager.Ins.OpenUI<StartCanvas>();
                });


        });
    }
    private void Update()
    {
        roateImg.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}