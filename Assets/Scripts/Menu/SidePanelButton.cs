using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePanelButton : MonoBehaviour
{
    public int buttonIndex;
    public GameObject highlight;

    public void UpdateHighlight(int activeButton) {
        if (buttonIndex == activeButton) {
            highlight.SetActive(true);
        } else {
            highlight.SetActive(false);
        }
    }

    public void Press() {
        if (buttonIndex == 0) {
            MenuUIController.Instance.LoadNode();
        } else if (buttonIndex == 1) {
            Debug.Log("opening settings");
        }
    }
}
