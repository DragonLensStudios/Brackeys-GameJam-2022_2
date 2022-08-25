using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsToText : MonoBehaviour
{
    public AchievementStore achievementStore;
    public int achievementsLayer;
    public float spacingBetweenAchievements;
    public Vector2 titleBasePosition;
    public Vector2 descriptionBasePosition;
    public Vector2 isCompleteBasePosition;

    public void Awake()
    {
        GenerateUIForAchievements();
    }

    public void GenerateUIForAchievements()
    {
        List<Achievement> achievementList = achievementStore.achievements.achievementList;
        GameObject scrollable = this.gameObject;
        GameObject parent = scrollable.transform.parent.gameObject;

        
        for (int i = 0; i < achievementList.Count; i++)
        {
            Achievement achievement = achievementList[i];
            int achievementNumber = i + 1;
            GameObject title = CreateGameObeject("Title " + achievementNumber, achievement.title, parent, titleBasePosition);
            title.transform.parent = scrollable.transform;
            GameObject description = CreateGameObeject("Description " + achievementNumber, achievement.description, parent, descriptionBasePosition);
            description.transform.parent = scrollable.transform;
            GameObject isComplete = CreateGameObeject("Completion status " + achievementNumber, achievement.isComplete, parent, isCompleteBasePosition);
            isComplete.transform.parent = scrollable.transform;
        }
    }

    private GameObject CreateGameObeject(string gameObjectName, string dispayText, GameObject parent, Vector2 basePosition)
    {
        GameObject gameObject = new GameObject(gameObjectName);
        gameObject.layer = achievementsLayer;
        
        RectTransform uiPosition = gameObject.AddComponent<RectTransform>();
        basePosition.y -= spacingBetweenAchievements;
        uiPosition.position = basePosition;
        

        TMPro.TextMeshProUGUI uiText = gameObject.AddComponent<TMPro.TextMeshProUGUI>();
        uiText.text = dispayText;
        uiText.overflowMode = TMPro.TextOverflowModes.Overflow;
        uiText.color = Color.black;
        uiText.alignment = TMPro.TextAlignmentOptions.Center;
        uiText.fontSize = 22;
        return gameObject;
    }

    private GameObject CreateGameObeject(string gameObjectName, bool isComplete, GameObject parent, Vector2 basePosition)
    {
        GameObject gameObject = new GameObject(gameObjectName);
        gameObject.layer = achievementsLayer;

        RectTransform uiPosition = gameObject.AddComponent<RectTransform>();
        basePosition.y -= spacingBetweenAchievements;
        uiPosition.position = basePosition;

        Image image = gameObject.AddComponent<Image>();
        image.color = isComplete ? Color.green : Color.gray;
        
        return gameObject;
    }
}
