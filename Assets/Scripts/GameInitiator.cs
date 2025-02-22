using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    GameManager gameManager;
    SoundManager soundManager;
    UIManager uiManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        uiManager = UIManager.Instance;
    }
}
