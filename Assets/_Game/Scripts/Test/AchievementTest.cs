using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AchievementTest : MonoBehaviour
{
    public PlayerInput input;

    private void OnEnable()
    {
        input.actions["Activate"].performed += AchievementTest_performed;
    }



    private void OnDisable()
    {
        input.actions["Activate"].performed -= AchievementTest_performed;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AchievementTest_performed(InputAction.CallbackContext input)
    {
        AchievementManager.instance.AddAchievementProgress("TutorialPuzzle", 1);
    }
}
