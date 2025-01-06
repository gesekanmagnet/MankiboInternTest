using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InterfaceHandle : MonoBehaviour
{
    [SerializeField] private RectTransform hoverBullet;
    [SerializeField] private RectTransform[] bulletIcons;

    [SerializeField] private Text timerText;
    [SerializeField] private Text healthText, resultText;

    [SerializeField] private RectTransform menuPos1, menuPos2, gameplayPos1, gameplayPos2, menuPanel, gameplayPanel;
    [SerializeField] private Animator textAnim;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += ShowGameplay;
        GameManager.Instance.OnGameStart += UpdateHealthText;
        GameManager.Instance.OnGameOver += ShowMenu;
        GameManager.Instance.OnSwitchBullet += SwitchBullet;
        GameManager.Instance.OnGameUpdate += UpdateTimerText;
        GameManager.Instance.OnHit += UpdateHealthText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= ShowGameplay;
        GameManager.Instance.OnGameStart -= UpdateHealthText;
        GameManager.Instance.OnGameOver -= ShowMenu;
        GameManager.Instance.OnSwitchBullet -= SwitchBullet;
        GameManager.Instance.OnGameUpdate -= UpdateTimerText;
        GameManager.Instance.OnHit -= UpdateHealthText;
    }

    private void UpdateHealthText()
    {
        healthText.text = GameManager.Instance.currentHealth.ToString();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(GameManager.Instance.currentTime / 60); 
        int seconds = Mathf.FloorToInt(GameManager.Instance.currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SwitchBullet(int x)
    {
        hoverBullet.DOMove(bulletIcons[x].position, .5f);
    }

    private void ShowMenu(int x)
    {
        menuPanel.DOMove(menuPos2.position, 1);
        gameplayPanel.DOMove(gameplayPos1.position, 1);

        ShowResult(x);
    }

    private void ShowGameplay()
    {
        gameplayPanel.DOMove(gameplayPos2.position, 1);
        menuPanel.DOMove(menuPos1.position, 1);
    }

    private void ShowResult(int x)
    {
        resultText.text = x == 1 ? "VICTORY" : "DEFEAT";
        textAnim.Play("Text");
        Debug.Log("gg");
    }
}
