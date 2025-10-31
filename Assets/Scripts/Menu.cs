using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameManager gameManager;

    public void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>()
            .gameObject
            .LeanScale(new Vector3(1.2f, 1.2f), 0.3f)
            .setLoopPingPong();
    }

    public void Play()
    {
        GetComponent<CanvasGroup>()
            .LeanAlpha(0, 0.2f)
            .setOnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameManager.Enable();
        Destroy(gameObject);
    }
}
