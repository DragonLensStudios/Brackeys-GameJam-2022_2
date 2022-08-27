using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject achievementsPanel;

    public void Start()
    {
        pauseMenu.SetActive(false);
        achievementsPanel.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadAchievements()
    {
        pauseMenu.SetActive(false);
        achievementsPanel.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        achievementsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
