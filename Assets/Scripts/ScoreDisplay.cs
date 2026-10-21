using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;

    private Label scoreLabel;
    private Coroutine initRoutine;
    private bool subscribed;

    private void OnEnable()
    {
        if (scoreSystem == null)
        {
            Debug.LogError("Atribui o ScoreSystem ao ScoreDisplay no Inspector.", this);
            enabled = false;
            return;
        }
        initRoutine = StartCoroutine(InitializeWhenReady());
    }

    private IEnumerator InitializeWhenReady()
    {
        yield return null; // deixa o UIDocument construir a árvore

        var doc = GetComponent<UIDocument>();
        scoreLabel = doc.rootVisualElement.Q<Label>("score-label");
        if (scoreLabel == null)
        {
            Debug.LogError("Falta o Label 'score-label' no UXML.", this);
            initRoutine = null;
            yield break;
        }

        scoreSystem.ScoreChanged += UpdateDisplay;
        subscribed = true;

        UpdateDisplay(scoreSystem.CurrentScore); // lê estado atual
        initRoutine = null;
    }

    private void OnDisable()
    {
        if (initRoutine != null) { StopCoroutine(initRoutine); initRoutine = null; }
        if (subscribed && scoreSystem != null)
        {
            scoreSystem.ScoreChanged -= UpdateDisplay;
            subscribed = false;
        }
    }

    private void UpdateDisplay(int value)
    {
        if (scoreLabel != null)
            scoreLabel.text = $"Pontuação: {value}";
    }
}