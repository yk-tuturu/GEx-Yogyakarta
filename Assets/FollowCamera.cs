using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform my_camera;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(my_camera.position.x, my_camera.position.y, 0);
    }
}
