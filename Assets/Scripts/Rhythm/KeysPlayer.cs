using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            PlaySound("saron2");
        } 
        else if (Input.GetKeyDown(KeyCode.D)) {
            PlaySound("saron2");
        } 
        else if (Input.GetKeyDown(KeyCode.F)) {
            PlaySound("saron3");
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            PlaySound("saron3");
        } 
        else if (Input.GetKeyDown(KeyCode.J)) {
            PlaySound("saron5");
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            PlaySound("saron6");
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            PlaySound("saron7");
        }
    }

    void PlaySound(string name) {
        AudioManager.Instance.PlayOneShot(name);
    }
}
