using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;

    public RectTransform titleRect;
    public RectTransform subtitleRect;
    public RectTransform panel;

    public CanvasGroup cg;

    public float maxFontSize = 24f;
    public float minFontSize = 10f;
    public float maxWidth = 600f;

    public float maxSubFontSize = 18f;
    public float minSubFontSize = 8f;

    public float subtitleSpacing = 10f;
    public float horizontalPadding = 10f;
    public float verticalPadding = 10f;

    public float musicOffset = 80f;
    public float storyOffset = 60f;

    public Flowchart flowchart;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideTooltip() {
        cg.alpha = 0f;
    }

    public void ShowTooltip(Node node) {
        NodeData data = NodeInfoParser.Instance.GetNodeInfo(node.key);

        float offset;
        if (data.type == "music") {
            offset = musicOffset;
        } else {
            offset = storyOffset;
        }

        if (!node.unlocked) {
            SetText("Locked", "", offset);
        } else {
            SetText(data.title, data.subtitle, offset);
        }
        
        cg.alpha = 1f;
    }

    public void SetText(string title, string subtitle, float offset) {
        titleText.text = title;
        subtitleText.text = subtitle;

        AdjustTextSize(titleText, titleRect, maxWidth, maxFontSize, minFontSize);

        // subtitle font should always be smaller than the title font 
        maxSubFontSize = Mathf.Round(titleText.fontSize * 0.75f);
        AdjustTextSize(subtitleText, subtitleRect, maxWidth, maxSubFontSize, minSubFontSize);

        // set the title text to an offset below the center of the screen 
        Vector3 ogPos = new Vector3(0, 0, 0);
        titleRect.anchoredPosition = new Vector3(ogPos.x, ogPos.y - offset, ogPos.z);
        
        // set the subtitle to be exactly below the title textbox (adjust for height of both textboxes) + some potential spacing 
        float subtitleY = titleRect.anchoredPosition.y - titleRect.rect.height / 2f - subtitleRect.rect.height / 2f - subtitleSpacing;
        subtitleRect.anchoredPosition = new Vector3(ogPos.x, subtitleY, ogPos.z);

        // set the width and height of the backing panel 
        // width is just the max of title or subtitle 
        // height add the heights together + padding
        float panelWidth = Mathf.Max(titleRect.rect.width, subtitleRect.rect.width) + horizontalPadding;
        float panelHeight = titleRect.rect.height + subtitleRect.rect.height + verticalPadding;

        // position the panel to be exactly in the middle of both the title and subtitle heights 
        float topEdge = titleRect.anchoredPosition.y + titleRect.rect.height / 2f;
        float bottomEdge = subtitleRect.anchoredPosition.y - subtitleRect.rect.height / 2f;
        float panelY = topEdge + (bottomEdge - topEdge) / 2f;

        // set pos and size
        panel.anchoredPosition = new Vector3(ogPos.x, panelY, ogPos.z);

        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidth);
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelHeight);
    }

    void AdjustTextSize(TextMeshProUGUI text, RectTransform rect, float maxWidth, float maxFontSize, float minFontSize) {
        text.enableAutoSizing = false;
        text.fontSize = maxFontSize;
        
        // forcibly reduce text size until its either within the allowed width or we've reached the min font size 
        while (true) {
            text.ForceMeshUpdate();
            
            if (text.preferredWidth < maxWidth || text.fontSize <= minFontSize) break;

            text.fontSize -= 1f;
        }

        // set the width of the textbox to this preferred width 
        float newWidth = Mathf.Min(text.preferredWidth, maxWidth);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

        // refresh everything before setting height 
        text.ForceMeshUpdate();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

        float newHeight = text.preferredHeight;
        
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);

        text.ForceMeshUpdate();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        
    }
}
