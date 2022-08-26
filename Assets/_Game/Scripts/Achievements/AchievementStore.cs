using System.IO;
using UnityEngine;

public class AchievementStore : MonoBehaviour
{
    public Achievements achievements;
    public string path = "Assets/_Game/GameData/achievements.txt";

    public void Awake()
    {
        achievements = LoadAchievements();
    }

    public void SaveAchievementsAsync()
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Title:Description:IsComplete");
        for (int i = 0; i < achievements.achievementList.Count; i++)
        {
            Achievement achievement = achievements.achievementList[i];
            writer.WriteLine(achievement.title + ":" + achievement.description + ":" + achievement.isComplete.ToString());
        }
        writer.Close();
    }

    private Achievements LoadAchievements()
    {
        StreamReader reader = new(path);
        Achievements achievements = new();
        //Ignores Header line
        reader.ReadLine();
        while(!reader.EndOfStream)
        {
            string[] achievement = reader.ReadLine().Split(":");
            bool isComplete = achievement[2] == true.ToString();
            achievements.achievementList.Add(new(achievement[0], achievement[1], isComplete));
        }
        reader.Close();
        return achievements;
    }
}
