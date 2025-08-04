using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UICanvas
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextEffect timerEff;

    [SerializeField] private TextMeshProUGUI heathTxt;
    [SerializeField] private GameObject bloodyScreen;

    private int maxHP = 3;
    private Coroutine bloodyCoroutine;

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

        attackBtn.onClick.AddListener(() =>
        {
            if (LevelManager.Ins.curLevel.swatter.isDead)
                return;
            LevelManager.Ins.curLevel.swatter.PlayAnim();
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

    #region Bloody Screen
    public void Hit()
    {
        // Nếu đang có coroutine máu đang chạy → dừng lại trước
        if (bloodyCoroutine != null)
        {
            StopCoroutine(bloodyCoroutine);
            bloodyCoroutine = null;
        }

        // Bắt đầu hiệu ứng mới và lưu lại coroutine
        bloodyCoroutine = StartCoroutine(BloodyScreenEffect());
    }
    private IEnumerator BloodyScreenEffect()
    {
        if (!bloodyScreen.activeInHierarchy)
            bloodyScreen.SetActive(true);

        var image = bloodyScreen.GetComponentInChildren<Image>();

        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float t = 0f;

        while (t < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            t += Time.deltaTime;
            yield return null;
        }

        if (bloodyScreen.activeInHierarchy)
            bloodyScreen.SetActive(false);

        // ✅ Xóa biến coroutine sau khi xong
        bloodyCoroutine = null;
    }

    public void SetHealth(int hp)
    {
        heathTxt.text = hp.ToString();
    }

    public void DecreaseHealth()
    {
        maxHP--;
        SetHealth(maxHP);
    }

    public void ResetUI()
    {
        // Đặt lại thời gian và cập nhật text
        elapsedTime = 0f;
        timerTxt.text = "00:00";
        timerEff.Refresh();

        SetHealth(3);
        maxHP = 3;

        if (bloodyCoroutine != null)
        {
            StopCoroutine(bloodyCoroutine);
            bloodyCoroutine = null;
        }

        if (bloodyScreen.activeInHierarchy)
            bloodyScreen.SetActive(false);
    }
    #endregion
}
