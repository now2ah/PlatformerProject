using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    Canvas canvas;
    CanvasScaler scaler;
    EventSystem eventSystem;
    [SerializeField] GameObject gameStartPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearPanel;

    public GameObject GameStartPanel { get => gameStartPanel; set => gameStartPanel = value; }
    public GameObject GameOverPanel { get => gameOverPanel; set => gameOverPanel = value; }
    public GameObject GameClearPanel { get => gameClearPanel; set => gameClearPanel = value; }
    public Canvas Canvas { get => canvas; set => canvas = value; }

    void _LoadUIAssets()
    {
        string rootPath = "Prefabs/UI/";
        GameObject gameStartPanelPrefab = Resources.Load(rootPath + "GameStartPanel") as GameObject;
        GameObject gameOverPanelPrefab = Resources.Load(rootPath + "GameOverPanel") as GameObject;
        GameObject gameClearPanelPrefab = Resources.Load(rootPath + "ClearPanel") as GameObject;

        gameStartPanel = Instantiate(gameStartPanelPrefab);
        gameOverPanel  = Instantiate(gameOverPanelPrefab);
        gameClearPanel = Instantiate(gameClearPanelPrefab);
    }

    void _CreateCanvas()
    {
        GameObject canvasObj = new GameObject("Canvas");
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.transform.SetParent(transform, false);
        scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(800, 600);
        GameObject eventSystemObj = new GameObject("EventSystem");
        eventSystemObj.AddComponent<EventSystem>();
        eventSystemObj.AddComponent<InputSystemUIInputModule>();
        eventSystemObj.transform.SetParent(transform, false);
    }

    private void Start()
    {
        gameStartPanel.transform.SetParent(canvas.transform, false);
        gameOverPanel.transform.SetParent(canvas.transform, false);
        gameClearPanel.transform.SetParent(canvas.transform, false);
    }

    private void Awake()
    {
        _CreateCanvas();
        _LoadUIAssets();
    }
}
