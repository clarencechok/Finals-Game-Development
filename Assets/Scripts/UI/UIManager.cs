using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //adding a reference to the ghame over screen to activate or deactivate
    [SerializeField] private GameObject gameOverScreen;
    [Header ("Game Over")]
    //this music will be played when the game over menu appears
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        //deactivate game over screen
        gameOverScreen.SetActive(false);
        //deactivate pause screen
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        //check if the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //account for scenario where the game is already paused
            PauseGame(!pauseScreen.activeInHierarchy);

        }
    }

    #region Game Over
    // be responsible for handling the gameover screen and playing game over sound
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
        //Quits the game but this code only work in build
        Application.Quit();

        #if UNITY_EDITOR
        //Exits play mode but only work in the editor
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion


    #region Pause

    public void PauseGame(bool status)
    {
        //if true will pause game but if false will unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)
        //when it's false change it back to 1 (time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}