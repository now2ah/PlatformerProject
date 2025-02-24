using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region PRIVATE
    bool _isRunning = false;
    PlayerController _player;
    CameraControl _cameraControl;
    #endregion

    #region EVENT
    public event EventHandler onSetPlayer;
    #endregion

    #region PROPERTIES
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
    #endregion

    [SerializeField]
    Vector2 _startPosition;

    public void GameClear()
    {
        StartCoroutine(GameClearCoroutine());
    }

    IEnumerator GameClearCoroutine()
    {
        UIManager.Instance.GameClearPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        UIManager.Instance.GameClearPanel.SetActive(false);
        //load next scene
    }

    public void GameOver()
    {
        _isRunning = false;
        //inGameCanvas.SetActive(false);
        UIManager.Instance.GameOverPanel.SetActive(true);
    }

    public void GameStart()
    {
        _cameraControl.CalculateStageLimitX();
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        SoundManager.Instance.PlayBGM(eBGM.GAME);
        UIManager.Instance.GameStartPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.GameStartPanel.SetActive(false);
        _SpawnPlayer();
        _isRunning = true;
        
        //inGameCanvas.SetActive(true);
    }

    void _SetMainCamera(Scene scene, LoadSceneMode mode)
    {
        _cameraControl = Camera.main.AddComponent<CameraControl>();
    }

    void _SpawnPlayer()
    {
        if (null == _player)
        {
            GameObject playerObjPrefab = Resources.Load("Prefabs/Player") as GameObject;
            GameObject playerObj = Instantiate(playerObjPrefab) as GameObject;
            if (playerObj.TryGetComponent<PlayerController>(out PlayerController player))
            {
                _player = player;
                onSetPlayer.Invoke(this, EventArgs.Empty);
            }
        }

        _player.transform.position = _startPosition;
    }

    private void Awake()
    {
        _startPosition = new Vector2(0, 5);
    }

    void Start()
    {
        SceneManager.sceneLoaded += _SetMainCamera;
    }
}
