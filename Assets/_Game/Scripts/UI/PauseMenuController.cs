using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour, IPauseable
{
    [SerializeField]
    private Page page;

    private void Awake()
    {
        page = GetComponent<Page>();
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
}
