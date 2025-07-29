using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NodeNavigator : MonoBehaviour
{
    public Node currNode;
    public float offset;
    public bool isNodeSelected;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnDirectionInput += OnDirectionInput;
        InputManager.Instance.OnEnterPressed += OnEnterPressed; 
        InputManager.Instance.OnEscPressed += OnEscPressed;

        Vector3 pos = currNode.transform.position;
        transform.position = new Vector3(pos.x, pos.y, -10);

        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;
        offset = width / 5f;
    }

    void OnDestroy() {
        InputManager.Instance.OnDirectionInput -= OnDirectionInput;
        InputManager.Instance.OnEnterPressed -= OnEnterPressed; 
        InputManager.Instance.OnEscPressed -= OnEscPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnterPressed() {
        DOTween.Kill(transform);
        Vector3 pos = currNode.transform.position;
        transform.DOMove(new Vector3(pos.x + offset, pos.y, -10), 0.2f).SetEase(Ease.OutSine);
        isNodeSelected = true;
        MenuUIController.Instance.ShowNodePanel(currNode.key);
    }

    void OnEscPressed() {
        DOTween.Kill(transform);
        Vector3 pos = currNode.transform.position;
        transform.DOMove(new Vector3(pos.x, pos.y, -10), 0.2f).SetEase(Ease.OutSine);
        isNodeSelected = false;
        MenuUIController.Instance.HideNodePanel();
    }

    void OnDirectionInput(Vector2 dir) {
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

        DOTween.Kill(transform);
        Vector3 pos = currNode.transform.position;
        transform.DOMove(new Vector3(pos.x, pos.y, -10), 0.2f).SetEase(Ease.OutSine);
    }

}
