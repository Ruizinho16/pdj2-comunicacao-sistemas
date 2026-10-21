using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private ScoreDoor door;

    private VisualElement victoryPanel;
    private Coroutine initRoutine;
    private bool subscribed;

    private void OnEnable()
    {
        if (door == null)
        {
            Debug.LogError("Atribui a ScoreDoor ao VictoryScreen no Inspector.", this);
            enabled = false;
            return;
        }

        initRoutine = StartCoroutine(InitializeWhenReady());
    }

    private IEnumerator InitializeWhenReady()
    {
        yield return null; // espera um frame para o UIDocument estar pronto

        UIDocument doc = GetComponent<UIDocument>();
        VisualElement root = doc.rootVisualElement;
        victoryPanel = root.Q<VisualElement>("victory-panel");

        if (victoryPanel == null)
        {
            Debug.LogError("Falta o elemento 'victory-panel' no UXML.", this);
            initRoutine = null;
            yield break;
        }

        door.OnDoorEntered += ShowVictory;
        subscribed = true;
        initRoutine = null;
    }

    private void OnDisable()
    {
        if (initRoutine != null)
        {
            StopCoroutine(initRoutine);
            initRoutine = null;
        }

        if (subscribed && door != null)
        {
            door.OnDoorEntered -= ShowVictory;
            subscribed = false;
        }
    }

    private void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.style.display = DisplayStyle.Flex;
        }
    }
}