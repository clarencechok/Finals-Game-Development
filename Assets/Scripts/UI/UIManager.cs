using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //adding a reference to the ghame over screen to activate or deactivate
    [SerializeField] private GameObject gameOverScreen;

    //this music will be played when the game over menu appears
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        //deactivate game over screen
        gameOverScreen.SetActive(false);
    }

    //this will be responsible for handling the gameover screen and playing game over sound
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Restarting of level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
    }
}
