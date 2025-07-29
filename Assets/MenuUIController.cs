using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MenuUIController : MonoBehaviourSingleton<MenuUIController>
{
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


    // Start is called before the first frame update
    void Start()
    {
        RhythmGameLoader.Instance.fadeToBlack += FadeToBlack;

        nodePanel.gameObject.SetActive(false);
        nodePanelPos = nodePanel.anchoredPosition;
        nodePanel.anchoredPosition = new Vector3(nodePanelPos.x + offset, nodePanelPos.y, nodePanelPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNodePanel(string key) {
        nodePanel.gameObject.SetActive(true);
        nodePanel.DOAnchorPosX(nodePanelPos.x, 0.3f);

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

    public void LoadRhythmLevel(string mapName)
    {
        loadingPanel.gameObject.SetActive(true);
        loadingPanel.alpha = 0f;
        loadingPanel.DOFade(1f, 0.4f).OnComplete(()=> {
            RhythmGameLoader.Instance.LoadRhythmScene(mapName);
        }); 
    }

    public void FadeToBlack() {
        RhythmGameLoader.Instance.fadeToBlack -= FadeToBlack;
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0f;
        blackScreen.DOFade(1f, 0.4f);
    }

    
}
