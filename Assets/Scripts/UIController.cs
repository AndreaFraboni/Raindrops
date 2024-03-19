using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text PlayerNameText;
    public TMP_Text ScoreText;
    public TMP_Text CurrentLevelText;
    public List<GameObject> LifeIcons = new List<GameObject>();

    public GameObject PauseGamePanel;
    public GameObject GameOverPanel;
    public GameObject LevelUP;

    private void Start()
    {

    }
    public void UpdatePlayerNameText(string _value)
    {
        PlayerNameText.text = "Player Name : " + _value;
    }

    public void UpdateScoreText(int _value)
    {
        ScoreText.text = "SCORE : " + _value.ToString();
    }

    public void UpdateCurrentLevelText(int _value)
    {
        CurrentLevelText.text = "LEVEL : " + _value.ToString();
    }

    public void UpdateLives(int _value)
    {
        for (int i = LifeIcons.Count - 1; i >= 0; i--)
        {
            LifeIcons[i].SetActive(_value >= i);
        }
    }
    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        GameOverPanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        PauseGamePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        PauseGamePanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {

    }

    public void ShowWinLevel()
    {
        LevelUP.SetActive(true);
    }
    public void HideWinLevel()
    {
        LevelUP.SetActive(false);
    }

}
