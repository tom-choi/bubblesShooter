using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public string levelname;

    public void newGame()
    {
        SceneManager.LoadScene(levelname);
    }

    public void endGame()
    {
        Application.Quit();
    }
}
