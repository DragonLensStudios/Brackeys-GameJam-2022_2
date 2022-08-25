using UnityEngine;
using System.Collections.Generic;

public class Achievements
{
    public List<Achievement> achievementList = new List<Achievement>();
}

public class Achievement
{
    public Achievement(string title, string description, bool isComplete)
    {
        this.title = title;
        this.description = description;
        this.isComplete = isComplete;
    }

    public string title;
    public string description;
    public bool isComplete;
}
