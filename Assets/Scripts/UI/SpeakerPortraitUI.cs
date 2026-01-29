using UnityEngine;
using UnityEngine.UI;

public class SpeakerPortraitUI : MonoBehaviour
{
    [SerializeField] private Transform[] players;   // Player1..Player5
    [SerializeField] private Image portraitImage;   // UI Image

    void Start()
    {
        // 🔔 Ждём, когда Binder применит визуалы
        PlayerAnimatorControllerBinder.OnPlayersVisualsReady += OnPlayersVisualsReady;
    }

    void OnDestroy()
    {
        PlayerAnimatorControllerBinder.OnPlayersVisualsReady -= OnPlayersVisualsReady;
    }

    private void OnPlayersVisualsReady()
    {
        var gsm = GameStateManager.Instance;
        if (gsm == null || gsm.State == null)
            return;

        ApplyState(gsm.State);
    }

    private void ApplyState(GameStateDto state)
    {
        int speakerIndex = state.game.speakerPosition - 1;

        if (speakerIndex < 0 || speakerIndex >= players.Length)
        {
            Debug.LogError($"Invalid speakerPosition: {state.game.speakerPosition}");
            return;
        }

        var speaker = players[speakerIndex];
        if (speaker == null)
            return;

        var spriteRenderer = speaker.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning($"SpriteRenderer not found on {speaker.name}");
            return;
        }

        // 🔑 минимальная логика
        portraitImage.sprite = spriteRenderer.sprite;

        var rt = portraitImage.rectTransform;

        float posY = GetPortraitPosY(state.players[speakerIndex].name);

        rt.anchoredPosition = new Vector2(
             rt.anchoredPosition.x,
            posY
        );
    }

    private static float GetPortraitPosY(string playerName)
    {
      return playerName switch
      {
        // 👇 игрок со "спущенным" спрайтом
        "Тирион" => -100f,

        // примеры на будущее
        // "Серсея" => -120f,
        // "Варис"  => -140f,

        _ => -137.6f // дефолт
      };
   }

}
