using UnityEngine;

public class LeaderTooltipUi : MonoBehaviour
{
    [SerializeField] private Transform[] players;
    [SerializeField] private Vector3 worldOffset = new Vector3(0, 1f, 0);

    private RectTransform rectTransform;
    private Camera worldCamera;
    private Transform target;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
      rectTransform = GetComponent<RectTransform>();
      worldCamera = Camera.main;

      var gsm = GameStateManager.Instance;
      if (gsm == null)
      {
         Debug.LogError("LeaderTooltipUi: GameStateManager.Instance is null");
         return;
      }

      gsm.OnStateChanged += OnGameStateChanged;

      // 🔥 ВАЖНЕЙШАЯ СТРОКА
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
        int leaderPos = state.game.leaderPosition; // 1..5

        int index = leaderPos - 1;

        if (index < 0 || index >= players.Length)
        {
            Debug.LogError($"Invalid LeaderPosition: {leaderPos}");
            return;
        }

        SetTarget(players[index]);
    }

    void SetTarget(Transform newTarget)
    {
        target = newTarget;
        spriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (target == null || worldCamera == null)
            return;

        float height = spriteRenderer != null
            ? spriteRenderer.bounds.size.y
            : 0f;

        Vector3 worldPos =
            target.position
            + Vector3.up * (height + worldOffset.y);
        worldPos.x += worldOffset.x;    

        Vector3 screenPos = worldCamera.WorldToScreenPoint(worldPos);
        screenPos.z = 0f;

        rectTransform.position = screenPos;
    }
}