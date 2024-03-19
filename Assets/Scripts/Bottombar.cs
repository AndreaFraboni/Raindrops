using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bottombar : MonoBehaviour
{
    public GameController controller;

    public float speed = 5;

    //**************************************************************************//
    //********************* COLLISION DROP *************************************//
    //**************************************************************************//
    public void CollisionDrop()
    {
        if (controller.Lives < 1) { controller.LiveLost(); }
        transform.Translate(new Vector2(0, 2) * speed * Time.deltaTime);
    }

    //*****************************************************************************************//
    //*****************************************************************************************//
    //*****************************************************************************************//
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level1"))
        {
            controller.LiveLost();
        }

        if (collision.gameObject.CompareTag("Level2"))
        {
            controller.LiveLost();
        }

        if (collision.gameObject.CompareTag("Level3"))
        {
            controller.LiveLost();
        }
    }

}
