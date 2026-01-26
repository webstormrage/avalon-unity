using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameStateDto State { get; private set; }

    public event Action<GameStateDto> OnStateChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetState(GameStateDto newState)
    {
        State = newState;
        OnStateChanged?.Invoke(State);
    }
}
