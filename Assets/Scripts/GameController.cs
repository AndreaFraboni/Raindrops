using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using static Drop;
using static GameController;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string UserName;
        public int HighScore;
        public int HighLevel;
    }
    PlayerData mPlayerData = new PlayerData();

    private bool isPlaying = true;
    public bool IsPlaying { get { return isPlaying; } }
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    public int Score = 0;
    public int Lives = 3;
    public int CurrentLevel = 1;
    public float DropVelocityByLevel = 0.2f;
    public int CurrentScore;

    public int NumberOfDropsWin;

    public enum eOperationID
    {
        Sum,
        Sub,
        Mul,
        Div
    }
    public eOperationID IDOp;

    public GameObject Spawner;
    public GameObject Spw1, Spw2, Spw3, Spw4, Spw5, Spw6, Spw7, Spw8;

    public GameObject Bottombar;
    public Vector2 BackupBottombarPosition;

    public bool stopSpawing = false;
    public float spawnTime = 1.0f;
    public float spawnDelay = 3.5f;

    private float lifetime;

    public TMP_InputField InputFieldObj;
    public int CurrentUserNumber;

    public ObjectPool ObjectPoolRef;
    public GameObject BottomBar;

    public UIController UIController;
    public TMP_InputField InputPlayerFieldObj;

    private void Awake()
    {
        BackupBottombarPosition = BottomBar.transform.position;

        UIController.UpdateScoreText(Score);
        UIController.UpdateCurrentLevelText(CurrentLevel);
        UIController.UpdateLives(Lives);

        UIController.HidePausePanel();
        UIController.HideGameOver();

        LoadSaveDataPlayer(); // Carica o Salva/Crea DATA Player

        UIController.UpdatePlayerNameText(mPlayerData.UserName);

        isPlaying = true;
        isPaused = false;
        Time.timeScale = 1;
        InputFieldObj.ActivateInputField();
    }

    private void Start()
    {
        InputFieldObj.ActivateInputField();
        InputFieldObj.text = "";
        InputFieldObj.caretBlinkRate = 1000;

        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    private void Update()
    {

    }

    //**************************************************************//
    //**************** SPAWN OBJECT ********************************//
    //**************************************************************//
    public void SpawnObject()
    {
        GameObject instance = ObjectPool.instance.GetPooledObject();
        if (instance != null)
        {
            instance.GetComponent<Drop>().BottomBarObj = Bottombar;
            instance.GetComponent<Drop>().NumA = Random.Range(0, 10);
            instance.GetComponent<Drop>().NumB = Random.Range(0, 10);
            int NumA = instance.GetComponent<Drop>().NumA;
            int NumB = instance.GetComponent<Drop>().NumB;
            instance.GetComponent<Drop>().NumAObj.GetComponent<TextMeshPro>().SetText(NumA.ToString());
            instance.GetComponent<Drop>().NumBObj.GetComponent<TextMeshPro>().SetText(NumB.ToString());

            if (CurrentLevel == 1) DropVelocityByLevel = 0.2f;
            if (CurrentLevel == 2) DropVelocityByLevel = 0.4f;
            if (CurrentLevel == 3) DropVelocityByLevel = 0.8f;
            if (CurrentLevel == 4) DropVelocityByLevel = 1.0f;
            if (CurrentLevel == 5) DropVelocityByLevel = 1.5f;
            if (CurrentLevel == 6) DropVelocityByLevel = 1.7f;
            if (CurrentLevel == 7) DropVelocityByLevel = 2.0f;
            if (CurrentLevel == 8) DropVelocityByLevel = 2.2f;

            instance.GetComponent<Drop>().DropVelocityByLevel = DropVelocityByLevel;


            IDOp = (eOperationID)Random.Range(0, 4);
            switch (IDOp)
            {
                case eOperationID.Sum:
                    instance.GetComponent<Drop>().Operation.GetComponent<TextMeshPro>().SetText("+");
                    instance.GetComponent<Drop>().result = NumA + NumB;
                    break;
                case eOperationID.Sub:
                    instance.GetComponent<Drop>().Operation.GetComponent<TextMeshPro>().SetText("-");
                    instance.GetComponent<Drop>().result = NumA - NumB;
                    break;
                case eOperationID.Mul:
                    instance.GetComponent<Drop>().Operation.GetComponent<TextMeshPro>().SetText("*");
                    instance.GetComponent<Drop>().result = NumA * NumB;
                    break;
                case eOperationID.Div:
                    instance.GetComponent<Drop>().Operation.GetComponent<TextMeshPro>().SetText("/");
                    if (NumB == 0) NumB = Random.Range(1, 2);
                    instance.GetComponent<Drop>().result = NumA / NumB;
                    break;
            }

            int SpwNum = Random.Range(1, 10);
            switch (SpwNum)
            {
                case 1:
                    instance.transform.position = Spw1.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw1.GetComponent<Transform>().rotation;
                    break;
                case 2:
                    instance.transform.position = Spw2.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw2.GetComponent<Transform>().rotation;
                    break;
                case 3:
                    instance.transform.position = Spw3.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw3.GetComponent<Transform>().rotation;
                    break;
                case 4:
                    instance.transform.position = Spw4.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw4.GetComponent<Transform>().rotation;
                    break;
                case 5:
                    instance.transform.position = Spw5.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw5.GetComponent<Transform>().rotation;
                    break;
                case 6:
                    instance.transform.position = Spw6.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw6.GetComponent<Transform>().rotation;
                    break;
                case 7:
                    instance.transform.position = Spw7.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw7.GetComponent<Transform>().rotation;
                    break;
                case 8:
                    instance.transform.position = Spw8.GetComponent<Transform>().position;
                    instance.transform.rotation = Spw8.GetComponent<Transform>().rotation;
                    break;
            }

            instance.SetActive(true);
        }

        if (stopSpawing)
        {
            CancelInvoke("SpawnObject");
        }
    }

    //*************************************************************//
    //************* READ TEXT FILED INPUT USER NUMBER *************//
    //*************************************************************//
    public void ReadTextField(string s)
    {
        string input = s;
        Int32.TryParse(input, out CurrentUserNumber);
        InputFieldObj.ActivateInputField();

        if (isPlaying) { CheckDropsResult(CurrentUserNumber); }
    }

    //***************************************************************//
    //************** CHECK DROPS RESULTS WITH USER NUMBER ***********//
    //***************************************************************//
    public void CheckDropsResult(int UserNumber)
    {
        for (int i = 0; i < ObjectPoolRef.GetComponent<ObjectPool>().amountObjPool; i++)
        {
            GameObject instance = ObjectPool.instance.GetActivePooledObject(i);
            if (instance != null)
            {
                if (instance.activeInHierarchy)
                {
                    if (instance.GetComponent<Drop>().result == UserNumber)
                    {
                        instance.SetActive(false);
                        string OperationTxt = instance.GetComponent<Drop>().Operation.GetComponent<TextMeshPro>().text;
                        if (OperationTxt == "+") { AddScore(2); IncNumberOfDropsWin(); }
                        if (OperationTxt == "-") { AddScore(5); IncNumberOfDropsWin(); }
                        if (OperationTxt == "*") { AddScore(10); IncNumberOfDropsWin(); }
                        if (OperationTxt == "/") { AddScore(20); IncNumberOfDropsWin(); }

                    }
                }
            }
        }
    }

    public void CleanObjectPool()
    {
        for (int i = 0; i < ObjectPoolRef.GetComponent<ObjectPool>().amountObjPool; i++)
        {
            GameObject instance = ObjectPool.instance.GetActivePooledObject(i);
            if (instance != null)
            {
                if (instance.activeInHierarchy)
                {
                    instance.SetActive(false);
                }
            }
        }
    }

    public void IncNumberOfDropsWin()
    {
        NumberOfDropsWin += 1;
        if (CurrentLevel == 1)
        {
            if (NumberOfDropsWin == 10)
            {
                YouWinLevel(1);
            }
        }

        if (CurrentLevel == 2)
        {
            if (NumberOfDropsWin == 20)
            {
                YouWinLevel(2);
            }
        }

        if (CurrentLevel == 3)
        {
            if (NumberOfDropsWin == 30)
            {
                YouWinLevel(3);
            }
        }

        if (CurrentLevel == 4)
        {
            if (NumberOfDropsWin == 40)
            {
                YouWinLevel(4);
            }
        }

        if (CurrentLevel == 5)
        {
            if (NumberOfDropsWin == 50)
            {
                YouWinLevel(4);
            }
        }

        if (CurrentLevel == 6)
        {
            if (NumberOfDropsWin == 60)
            {
                YouWinLevel(6);
            }
        }

        if (CurrentLevel == 7)
        {
            if (NumberOfDropsWin == 70)
            {
                YouWinLevel(7);
            }
        }

        if (CurrentLevel == 8)
        {
            if (NumberOfDropsWin == 80)
            {
                YouWinLevel(8);
            }
        }


    }

    public void UpdateLevelNumber()
    {
        UIController.UpdateCurrentLevelText(CurrentLevel);
    }


    public void YouWinLevel(int numLevWin)
    {
        CurrentLevel += 1;
        if (CurrentLevel > mPlayerData.HighLevel) { mPlayerData.HighLevel = CurrentLevel; }
        UpdateLevelNumber();
        UIController.ShowWinLevel();

        Invoke("HideLevelUp", 2);
    }

    public void HideLevelUp()
    {
        UIController.HideWinLevel();
    }

    //*****************************************************************//
    //****************** UPDATE SCORE *********************************//
    //*****************************************************************//    
    public void AddScore(int _value)
    {
        Score += _value;
        if (Score > mPlayerData.HighScore) { mPlayerData.HighScore = Score; }
        UIController.UpdateScoreText(Score);
    }
    public void LiveLost()
    {
        // lose life
        Lives--;

        UIController.UpdateLives(Lives);

        if (Lives < 0)
        {
            //GameOver();
            Invoke("GameOver", 1);
        }
    }

    //*******************************************************************************************************//
    //************************* PAUSE GAME BUTTON ***********************************************************//
    //*******************************************************************************************************//
    public void PauseGameButton()
    {
        PauseGame();
        UIController.ShowPausePanel();
    }

    //*******************************************************************************************************//
    //************************* RESUME GAME BUTTON **********************************************************//
    //*******************************************************************************************************//
    public void ResumeGameButton()
    {
        UnPauseGame();
    }

    //*******************************************************************************************************//
    //************************* PAUSE EXIT GAME BUTTON ******************************************************//
    //*******************************************************************************************************//
    public void PauseGameExitButton()
    {
        UIController.HidePausePanel();
        stopSpawing = true;
        isPlaying = false;
        isPaused = true;
        SavePlayerData();
        SceneManager.LoadScene("MainMenu");
    }
    public void GameOverGameExitButton()
    {
        UIController.HideGameOver();
        stopSpawing = true;
        isPlaying = false;
        isPaused = true;
        SavePlayerData();
        SceneManager.LoadScene("MainMenu");
    }

    public void ReStartGameOverBUtton()
    {
        UIController.HideGameOver();
        stopSpawing = false;
        isPaused = false;
        Time.timeScale = 1;
        InputFieldObj.ActivateInputField();
        UIController.UpdateLives(Lives);
        UIController.UpdateScoreText(Score);
        UIController.UpdateCurrentLevelText(CurrentLevel);
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    //*******************************************************************************************************//
    //************************* PAUSE GAME ******************************************************************//
    //*******************************************************************************************************//
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        InputFieldObj.DeactivateInputField();
        isPlaying = false;
    }

    //*******************************************************************************************************//
    //************************* UNPAUSE GAME ****************************************************************//
    //*******************************************************************************************************//
    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        InputFieldObj.ActivateInputField();
        isPlaying = true;
        UIController.HidePausePanel();
    }

    //*******************************************************************************************************//
    //************************* GAMEOVER ********************************************************************//
    //*******************************************************************************************************//
    public void GameOver()
    {
        CleanObjectPool();
        BottomBar.transform.position = BackupBottombarPosition;
        PauseGame();
        Lives = 3;
        CurrentLevel = 1;
        DropVelocityByLevel = 0.2f;
        stopSpawing = true;
        if (CurrentScore > mPlayerData.HighScore) { mPlayerData.HighScore = CurrentScore; }
        if (CurrentLevel > mPlayerData.HighLevel) { mPlayerData.HighLevel = CurrentLevel; }
        UIController.ShowGameOver();
    }

    //**************************************************************************************************//
    //******************************** LOAD_SAVE_PLAYER_DATA *******************************************//
    //**************************************************************************************************//
    public void LoadSaveDataPlayer()
    {
        string saveFile = Application.persistentDataPath + "/gamedata.json";

        if (File.Exists(saveFile))
        {
            //Debug.Log("FILE EXISTS !!!");
            string loadPlayerData = File.ReadAllText(saveFile);
            mPlayerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
        }
        else
        {
            mPlayerData.UserName = "Player0";
            mPlayerData.HighScore = 0;
            mPlayerData.HighLevel = 1;

            //Debug.Log("FILE DOES NOT EXISTS !!!");
            string json = JsonUtility.ToJson(mPlayerData);
            File.WriteAllText(saveFile, json);
        }

    }

    public void SavePlayerData()
    {
        string saveFile = Application.persistentDataPath + "/gamedata.json";
        string json = JsonUtility.ToJson(mPlayerData);
        File.WriteAllText(saveFile, json);
    }

}
