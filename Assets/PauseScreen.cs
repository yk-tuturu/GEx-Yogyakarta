using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        PauseManager.Instance.onPause += ShowPanel;
        PauseManager.Instance.onUnpause += HidePanel;
    }

    void OnDestroy() {
        PauseManager.Instance.onPause -= ShowPanel;
        PauseManager.Instance.onUnpause -= HidePanel;
    }

    public void ShowPanel() {
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }

    public void HidePanel() {
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("menu");
    }

    public void Pause() {
        PauseManager.Instance.Pause();
        ShowPanel();
    }

    public void Continue() {
        PauseManager.Instance.Unpause();
        HidePanel();
    }
}
