using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    bool _isRunning = false;
    PlayerController _player;

    public event EventHandler onSetPlayer;

    public CameraControl cameraControl;
    public GameObject inGameCanvas;
    public bool IsRunning => _isRunning;
    public PlayerController Player 
    { 
        get { return _player; } 
        set 
        { 
            _player = value;
            onSetPlayer.Invoke(this, EventArgs.Empty);
        } 
    }

    public void GameClear()
    {
        StartCoroutine(GameClearCoroutine());
    }

    IEnumerator GameClearCoroutine()
    {
        UIManager.Instance.gameClearPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        UIManager.Instance.gameClearPanel.SetActive(false);
        //load next scene
    }

    public void GameOver()
    {
        _isRunning = false;
        inGameCanvas.SetActive(false);
        UIManager.Instance.gameOverPanel.SetActive(true);
    }

    public void GameStart()
    {
        cameraControl.CalculateStageLimitX();
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        UIManager.Instance.gameStartPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.gameStartPanel.SetActive(false);
        _isRunning = true;
        inGameCanvas.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
