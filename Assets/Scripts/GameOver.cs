using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOver : MonoBehaviour
{
    public void Awake()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>()
            .gameObject
            .LeanScale(new Vector3(1.2f, 1.2f), 0.3f)
            .setLoopPingPong();
    }

    public void OnEnable() {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>()
            .LeanAlpha(1, 0.2f);
    }

    public void ReStart()
    {
        GetComponent<CanvasGroup>()
            .LeanAlpha(0, 0.2f)
            .setOnComplete(OnReStart);
    }

    private void OnReStart()
    {
        GameManager.Run();
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}
