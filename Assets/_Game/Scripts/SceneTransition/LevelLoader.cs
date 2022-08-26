using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    /**
     * This should be called from an in game checkpoint
     */
    public void LoadNextLevel()
    {
        //What happens on the last scene?
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start_Load");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelIndex);

        while(!asyncOperation.isDone)
        {
            yield return null;
        }

        transition.SetTrigger("End_Load");
    }
}
