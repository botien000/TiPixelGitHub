using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    enum State
    {
        GamePlay,GameSetting,GameOver
    }
    private State curState;
    [SerializeField]
    private GamePlay gamePlay;
    [SerializeField]
    private GameSetting gameSetting;
    [SerializeField]
    private GameOver gameOver;
    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            
            instance = this;
        }
    }
    private void OnEnable()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        StateGame(State.GamePlay);
    }
    public void Score()
    {
        gamePlay.ShowTextScore(gamePlay.IncrScore());
    }
    
    public void ToGameSetting()
    {
        StateGame(State.GameSetting);
        AudioManager.instance.music.Pause();
        AudioManager.instance.sfx.mute = true;
    }
    
    public void ToGamePlay()
    {
        StateGame(State.GamePlay);
        AudioManager.instance.music.UnPause();
        AudioManager.instance.sfx.mute = false;
    }
    public void ToGameOver()
    {
        StateGame(State.GameOver);
        AudioManager.instance.music.Pause();
        AudioManager.instance.sfx.mute = true; 
    }
    private void StateGame(State state)
    {
        curState = state;
        switch (curState) 
        {
            case State.GamePlay:
                gamePlay.gameObject.SetActive(true);
                gameSetting.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);
                break;
            case State.GameSetting:
                gamePlay.gameObject.SetActive(false);
                gameSetting.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(false);
                break;
            case State.GameOver:
                gamePlay.gameObject.SetActive(false);
                gameSetting.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
                break;
        }
    }
    public void HitPlayer()
    {
        gamePlay.HitPlayer();
    }
}
