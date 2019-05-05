using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   //the player

    

    private void LateUpdate()   //updates at the end
    {
        //makes it so the x position shifts only a certain amount and that y and z don't really shift.
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5f, 5f), Mathf.Clamp(transform.position.y, 0, 0), transform.position.z);
    }
}
