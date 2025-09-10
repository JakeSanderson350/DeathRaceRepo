public interface IObjective
{
    ObjectiveGroup Group { get; }
    bool IsActivated { get; }

    /// <summary>Remember to add the class to the list of objectives, ObjectiveManager.objectives.Add(this);</summary>
    void EnableObjective();

    /// <summary>Remember to add the class to the list of objectives, ObjectiveManager.objectives.Remove(this);</summary>
    void DisableObjective();

    /// <summary>A good idea to check the objective status after adding one. ObjectiveManager.CheckObjectiveStatus(Group);</summary>
    void SetObjectiveState(bool active);
}

