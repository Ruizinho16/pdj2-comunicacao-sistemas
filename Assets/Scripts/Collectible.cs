using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField, Min(1)] private int points = 1;

    private bool collected;

    public void Interact(GameObject interactor)
    {
        if (collected) return;

        if (scoreSystem == null)
        {
            Debug.LogError("Atribui o ScoreSystem ao Collectible no Inspector.", this);
            return;
        }

        collected = true;                       // impede recolhas repetidas
        scoreSystem.AddPoints(points);          // chamada direta ao sistema
        gameObject.SetActive(false);            // item deixa de poder ser recolhido
    }
}