using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UICanvas
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextEffect timerEff;

    private float elapsedTime = 0f;

    private void OnEnable()
    {
        UIManager.Ins.mainCanvas = this;
    }

    private void Start()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.OpenUI<PauseCanvas>();
            UIManager.Ins.CloseUI<MainCanvas>();
        });
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerTxt.text = $"{minutes:00}:{seconds:00}";
        timerEff.Refresh();
    }

    public void RefreshTimer()
    {
        elapsedTime = 0f;
    }

    public void ResetUI()
    {
        // Đặt lại thời gian và cập nhật text
        elapsedTime = 0f;
        timerTxt.text = "00:00";
        timerEff.Refresh();
    }
}
