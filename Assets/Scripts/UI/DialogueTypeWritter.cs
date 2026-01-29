using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTypeWritter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private ScrollRect scrollRect;

    [Header("Typing")]
    [SerializeField] private float charsPerSecond = 30f;

    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool hasOverflowed;
    private string currentText;

    // ===================== UNITY =====================

    private void Start()
    {
        var gsm = GameStateManager.Instance;
        if (gsm == null)
        {
            Debug.LogError("GameStateManager.Instance is null");
            return;
        }

        gsm.OnStateChanged += OnGameStateChanged;

        if (gsm.State != null)
            ApplyState(gsm.State);
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnStateChanged -= OnGameStateChanged;
    }

    // ===================== GAME STATE =====================

    private void OnGameStateChanged(GameStateDto state)
    {
        ApplyState(state);
    }

    private void ApplyState(GameStateDto state)
    {
        if (state.prompt == null || string.IsNullOrEmpty(state.prompt.response))
            return;

        string newText = state.prompt.response;

        // если текст тот же
        if (newText == currentText)
        {
            // если печать идёт — просто допечатываем
            if (isTyping)
                FinishTyping();

            return;
        }

        currentText = newText;
        StartTyping(newText);
    }

    // ===================== TYPING =====================

    private void StartTyping(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeRoutine(text));
    }

    private IEnumerator TypeRoutine(string text)
    {
      isTyping = true;

      dialogueText.text = text;
      dialogueText.maxVisibleCharacters = 0;

      Canvas.ForceUpdateCanvases();

      int totalChars = dialogueText.textInfo.characterCount;
      float delay = 1f / charsPerSecond;

      const int scrollDelayChars = 350;

       // стартуем сверху
       scrollRect.verticalNormalizedPosition = 1f;

       int scrollableChars = Mathf.Max(1, totalChars - scrollDelayChars);

        for (int i = 0; i <= totalChars; i++)
        {
           dialogueText.maxVisibleCharacters = i;

           if (i > scrollDelayChars)
           {
              float t = (float)(i - scrollDelayChars) / scrollableChars;
              t = Mathf.Clamp01(t);

              scrollRect.verticalNormalizedPosition = 1f - t;
          }

        yield return new WaitForSeconds(delay);
    }

       // гарантированно в самый низ
       scrollRect.verticalNormalizedPosition = 0f;

       isTyping = false;
    }

    private void FinishTyping()
    {
        if (!isTyping)
            return;

        isTyping = false;

        dialogueText.maxVisibleCharacters = dialogueText.textInfo.characterCount;
        Canvas.ForceUpdateCanvases();

        if (IsOverflowing())
            ScrollToBottom();
    }

    // ===================== SCROLL =====================

    private bool IsOverflowing()
    {
        return scrollRect.content.rect.height > scrollRect.viewport.rect.height;
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        DebugScrollState("SCROLLED TO BOTTOM");
    }

    private void DebugScrollState(string label)
{
    var content = scrollRect.content;
    var viewport = scrollRect.viewport;

    Debug.Log(
        $"[{label}]\n" +
        $"Content height: {content.rect.height}\n" +
        $"Viewport height: {viewport.rect.height}\n" +
        $"Overflow: {content.rect.height > viewport.rect.height}\n" +
        $"Scroll pos (normalized): {scrollRect.verticalNormalizedPosition}\n" +
        $"Content pivot: {content.pivot}\n" +
        $"Viewport pivot: {viewport.pivot}\n" +
        $"ScrollRect pivot: {((RectTransform)scrollRect.transform).pivot}"
    );
}
}
