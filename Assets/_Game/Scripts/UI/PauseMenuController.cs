using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour, IPauseable
{
    [SerializeField]
    private Page page;
    
    private PlayerController pc;
    private AchievenmentListIngame achivementList;
    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        page = GetComponent<Page>();
        achivementList = FindObjectOfType<AchievenmentListIngame>();
    }
    public void OnGamePaused()
    {
        if (page == null) { page = GetComponent<Page>(); }
        page.Enter(true);
    }

    public void OnGameUnpaused()
    {
        if (page == null) { page = GetComponent<Page>(); }
        page.Exit(true);
    }

    public void Pause()
    {
        pc.IsPaused = true;
    }

    public void Unpause()
    {
        pc.IsPaused = false;
    }

    public void ShowAchivements()
    {
        if(achivementList != null)
        {
            achivementList.OpenWindow();
        }
    }

    public void HideAchivements()
    {
        if(achivementList != null)
        {
            achivementList.CloseWindow();
        }
    }
}
