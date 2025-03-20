using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void Option()
    {
        
    }
    public void Score()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
    }
}
