using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }
}
