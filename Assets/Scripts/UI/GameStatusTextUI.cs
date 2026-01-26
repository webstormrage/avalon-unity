using TMPro;
using UnityEngine;

public class GameStatusTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;

    void Start()
    {
        var gsm = GameStateManager.Instance;

        gsm.OnStateChanged += OnGameStateChanged;

        // если state уже установлен (bootstrap / reconnect)
        if (gsm.State != null)
            OnGameStateChanged(gsm.State);
    }

    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnStateChanged -= OnGameStateChanged;
    }

    void OnGameStateChanged(GameStateDto state)
    {
        statusText.text = state.game.gameState;
    }

}