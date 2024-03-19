using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public enum eOperationID
    {
        Sum,
        Sub,
        Mul,
        Div
    }
    public eOperationID IDOp;

    public GameObject BottomBarObj;
    public int NumA = 0;
    public int NumB = 0;
    public int result = 0;
    public GameObject NumAObj;
    public GameObject NumBObj;
    public GameObject Operation;

    public float DropVelocityByLevel = 0.5f;

    private void Awake()
    {
    }

    public void Update()
    {
        if (this.isActiveAndEnabled) MoveDrop();
    }

    public void MoveDrop()
    {
        Vector2 movimento = new Vector2(0f, -1f);
        this.transform.Translate(movimento * DropVelocityByLevel * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bottombar"))
        {
            BottomBarObj.GetComponent<Bottombar>().CollisionDrop();
            gameObject.SetActive(false);
        }
    }

}
