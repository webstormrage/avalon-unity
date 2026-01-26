using UnityEngine;

[CreateAssetMenu(
    fileName = "AnimatorControllerMap",
    menuName = "Game/Animator Controller Map"
)]
public class AnimatorControllerMap : ScriptableObject
{
    public AnimatorMapEntry[] entries;

    public RuntimeAnimatorController Get(string name)
    {
        foreach (var e in entries)
        {
            if (e.playerName == name)
                return e.controller;
        }

        Debug.LogWarning($"AnimatorController not found for {name}");
        return null;
    }
}
