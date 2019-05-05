using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterFaith : MonoBehaviour
{
    public static bool activeGreaterFaith = false;
    //lasts for 10 seconds
    private float time = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeGreaterFaith==true)
        {
            //if active, counts down
            print("Time is: " + time);
            time -= Time.deltaTime;
        }

        if(time < 0)
        {
            //and deactives when complete.
            activeGreaterFaith = false;
        }
    }
}
