using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float secondsForLoadingScreen;

    /**
     * This should be called from an in game checkpoint
     */
    public void LoadNextLevel()
    {
        //What happens on the last scene?
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start_Load");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelIndex);
        
        float currentTime = 0;

        while (!asyncOperation.isDone && currentTime <= secondsForLoadingScreen)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForSeconds(1);
        }

        transition.SetTrigger("End_Load");
    }
}
