using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetectorAbove : MonoBehaviour
{
    public Collider2D direction;
    public static bool detected = false;
    public static string name;
    // Start is called before the first frame update
    void Start()
    {
        name = direction.name;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Enemy")
        {
            //print("COLLISION: " + direction.name);
          //  print(collision.name);
            name = direction.name;
            detected = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name != "Enemy")
        {
           // print("COLLISION: " + direction.name);
          //  print(collision.name);
            name = direction.name;
            detected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Enemy")
        {
           // print("COLLISION: " + direction.name);
            //print(collision.name);
            name = direction.name;
            detected = false;
        }
    }
}
