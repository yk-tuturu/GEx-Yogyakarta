using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NodeNavigator : MonoBehaviour
{
    public Node startingNode; 

    public float offset;
    public bool isNodeSelected;

    public TooltipController tooltip;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnDirectionInput += OnDirectionInput;
        InputManager.Instance.OnEnterPressed += OnEnterPressed;
        InputManager.Instance.OnEscPressed += OnEscPressed; 

        Vector3 pos = startingNode.transform.position;
        transform.position = new Vector3(pos.x, pos.y, -10);

        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;
        offset = width / 5f;

        MenuStateManager.Instance.currentNode = startingNode;
        MenuStateManager.Instance.OnEnterState += EnterState;

        tooltip.ShowTooltip(startingNode);
    }

    void OnDestroy() {
        InputManager.Instance.OnDirectionInput -= OnDirectionInput;
        InputManager.Instance.OnEnterPressed -= OnEnterPressed; 
        InputManager.Instance.OnEscPressed -= OnEscPressed; 
        MenuStateManager.Instance.OnEnterState -= EnterState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnterState(MenuState state) {
        if (state == MenuState.Panel) {
            ShiftToLeft();
        } else if (state == MenuState.Navigation) {
            ShiftBack();
        }
    }

    void OnEnterPressed() {
        MenuStateManager msm = MenuStateManager.Instance; 

        if (msm.currentState == MenuState.Panel || !msm.currentNode.unlocked) return;

        msm.ChangeState(MenuState.Panel);
    }

    void OnEscPressed() {
        if (MenuStateManager.Instance.currentState == MenuState.Navigation) return;

        MenuStateManager.Instance.ChangeState(MenuState.Navigation);
    }

    void ShiftToLeft() {
        DOTween.Kill(transform);

        Node currNode = MenuStateManager.Instance.currentNode;
        Vector3 pos = currNode.transform.position;
        transform.DOMove(new Vector3(pos.x + offset, pos.y, -10), 0.2f).SetEase(Ease.OutSine);
        isNodeSelected = true;
        tooltip.HideTooltip();
    }

    void ShiftBack() {
        DOTween.Kill(transform);

        Node currNode = MenuStateManager.Instance.currentNode;
        Vector3 pos = currNode.transform.position;
        
        isNodeSelected = false;

        transform.DOMove(new Vector3(pos.x, pos.y, -10), 0.2f).SetEase(Ease.OutSine).OnComplete(()=> {
            tooltip.ShowTooltip(currNode);
        });
        
    }

    void OnDirectionInput(Vector2 dir) {
        if (MenuStateManager.Instance.currentState != MenuState.Navigation) return;

        Node currNode = MenuStateManager.Instance.currentNode;
        Node prevNode = currNode;

        if (dir.x == 1 && currNode.right != null) {
            currNode = currNode.right;
        }

        if (dir.x == -1 && currNode.left != null) {
            currNode = currNode.left;
        }

        if (dir.y == 1 && currNode.up != null) {
            currNode = currNode.up;
        } 

        if (dir.y == -1 && currNode.down != null) {
            currNode = currNode.down;
        }

        MenuStateManager.Instance.currentNode = currNode;

        if (prevNode != currNode) {
            tooltip.HideTooltip();

            DOTween.Kill(transform);
            Vector3 pos = currNode.transform.position;
            transform.DOMove(new Vector3(pos.x, pos.y, -10), 0.2f).SetEase(Ease.OutSine).OnComplete(()=> {
                tooltip.ShowTooltip(currNode);
            });
            AudioManager.Instance.PlayOneShot("nodeSfx", true, 0.7f);
        }
        
    }

}
