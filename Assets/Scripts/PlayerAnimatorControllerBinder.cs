using UnityEngine;

public class PlayerAnimatorControllerBinder : MonoBehaviour
{
    [SerializeField] private Transform[] players;                 // Player1..Player5
    [SerializeField] private AnimatorControllerMap controllerMap; // ScriptableObject

    // 🆕 материалы
    [SerializeField] private Material standardMaterial;
    [SerializeField] private Material strokeMaterial;

    void Start()
    {
        var gsm = GameStateManager.Instance;
        if (gsm == null)
        {
            Debug.LogError("GameStateManager.Instance is null");
            return;
        }

        gsm.OnStateChanged += OnGameStateChanged;

        // если состояние уже установлено bootstrap'ом
        if (gsm.State != null)
            ApplyState(gsm.State);
    }

    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStateDto state)
    {
        ApplyState(state);
    }

    private void ApplyState(GameStateDto state)
    {
        if (state.players == null)
        {
            Debug.LogError("GameState.players is null");
            return;
        }

        if (players.Length != state.players.Length)
        {
            Debug.LogError("Players count mismatch between scene and GameState");
            return;
        }

        int speakerIndex = state.game.speakerPosition - 1;

        for (int i = 0; i < players.Length; i++)
        {
            var view = players[i];
            var dto  = state.players[i];

            if (view == null || dto == null)
                continue;

            var animator = view.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning($"Animator not found on {view.name}");
                continue;
            }

            var controller = controllerMap.Get(dto.name);

            if (controller == null)
            {
                Debug.LogWarning($"No AnimatorController for {dto.name}");
                continue;
            }

            animator.runtimeAnimatorController = controller;

             // 2️⃣ Задаём роль
            var role = RoleIndex(dto.role);
            animator.SetInteger("Role", (int)role);

             // ---------- SpriteRenderer / Material ----------
            var spriteRenderer = view.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
                continue;

            // 🟢 выделяем speaker
            spriteRenderer.material =
                (i == speakerIndex)
                    ? strokeMaterial
                    : standardMaterial;
        }
    }

    private static int RoleIndex(string role)
    {
        return role switch
        {
            "Слуга Артура"    => 0,
            "Мерлин"          => 1,
            "Миньон Мордреда" => 2,
            "Ассасин"         => 3
        };
    }

}
