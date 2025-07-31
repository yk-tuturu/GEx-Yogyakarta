using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController Instance;

    public CanvasGroup loadingPanel; 

    public CanvasGroup blackScreen;

    public RectTransform nodePanel;
    public Vector3 nodePanelPos;
    public float offset = 700f;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nodeTypeText;
    public string mapName;

    public int panelActiveButton = 0;
    public SidePanelButton[] sidePanelButtons;

    void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RhythmGameLoader.Instance.fadeToBlack += FadeToBlack;

        nodePanel.gameObject.SetActive(false);
        nodePanelPos = nodePanel.anchoredPosition;
        nodePanel.anchoredPosition = new Vector3(nodePanelPos.x + offset, nodePanelPos.y, nodePanelPos.z);

        MenuStateManager.Instance.OnEnterState += EnterState;
        MenuStateManager.Instance.OnExitState += ExitState;
        InputManager.Instance.OnDirectionInput += OnDirectionInput;
        InputManager.Instance.OnEnterPressed += OnEnterPressed;
    }

    void OnDestroy() {
        MenuStateManager.Instance.OnEnterState -= EnterState;
        MenuStateManager.Instance.OnExitState -= ExitState;
        InputManager.Instance.OnDirectionInput -= OnDirectionInput;
        InputManager.Instance.OnEnterPressed -= OnEnterPressed;
    }

    void EnterState(MenuState state) {
        if (state == MenuState.Panel) {
            ShowNodePanel();
        }
    }

    void ExitState(MenuState state) {
        if (state == MenuState.Panel) {
            HideNodePanel();
        }
    }

    void OnDirectionInput(Vector2 dir) {
        if (MenuStateManager.Instance.currentState != MenuState.Panel) return; 

        if (dir.x == -1) {
            panelActiveButton = Mathf.Max(0, panelActiveButton - 1);
        } else if (dir.x == 1) {
            panelActiveButton = Mathf.Min(sidePanelButtons.Length - 1, panelActiveButton + 1);
        }

        UpdateButtons();
    }

    void OnEnterPressed() {
        if (MenuStateManager.Instance.currentState != MenuState.Panel) return; 

        sidePanelButtons[panelActiveButton].Press();
    }

    public void ShowNodePanel() {
        nodePanel.gameObject.SetActive(true);
        nodePanel.DOAnchorPosX(nodePanelPos.x, 0.3f);

        string key = MenuStateManager.Instance.currentNode.key;
        NodeData data = NodeInfoParser.Instance.GetNodeInfo(key);

        titleText.text = data.title;
        subtitleText.text = data.subtitle;
        descriptionText.text = data.description;
        
        string type = data.type;
        nodeTypeText.text = char.ToUpper(type[0]) + type.Substring(1) + " Node";
    }

    public void HideNodePanel() {
        nodePanel.DOAnchorPosX(nodePanelPos.x + offset, 0.3f).OnComplete(()=>{
            nodePanel.gameObject.SetActive(false);
        });
    }

    public void UpdateButtons() {
        foreach (var button in sidePanelButtons) {
            button.UpdateHighlight(panelActiveButton);
        }
    }

    public void LoadNode() {
        Node currentNode = MenuStateManager.Instance.currentNode;
        NodeData node = NodeInfoParser.Instance.GetNodeInfo(currentNode.key);

        if (node.type == "music") {
            LoadRhythmLevel(node.key);
        } else if (node.type == "story") {
            LoadStoryLevel(node.key);
        }
    }

    public void LoadRhythmLevel(string mapName)
    {
        Debug.Log("loading rhythm");
        loadingPanel.gameObject.SetActive(true);
        loadingPanel.alpha = 0f;
        loadingPanel.DOFade(1f, 0.4f).OnComplete(()=> {
            RhythmGameLoader.Instance.LoadRhythmScene(mapName);
        }); 
    }

    public void LoadStoryLevel(string name) {
        Debug.Log("loading story");
    }

    public void FadeToBlack() {
        RhythmGameLoader.Instance.fadeToBlack -= FadeToBlack;
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0f;
        blackScreen.DOFade(1f, 0.4f);
    }

    
}
