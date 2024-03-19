using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VirtualKeyboard : Monosingleton<VirtualKeyboard>
{
    public GameObject UserNumberObj;
    
    public string CurrentTextNumber = "0";
    public int CurrentNum = 1;

    private void Awake()
    {
        //UserNumberObj.GetComponent<TextMeshPro>().SetText(CurrentNum.ToString());
    }

    private void Start()
    {
        
    }

    public void Button1()
    {
        CurrentTextNumber = CurrentTextNumber + "1";
    }
    public void Button2()
    {
        CurrentTextNumber = CurrentTextNumber + "2";
    }
    public void Button3()
    {
        CurrentTextNumber = CurrentTextNumber + "3";
    }
    public void Button4()
    {
        CurrentTextNumber = CurrentTextNumber + "4";
    }
    public void Button5()
    {
        CurrentTextNumber = CurrentTextNumber + "5";
    }
    public void Button6()
    {
        CurrentTextNumber = CurrentTextNumber + "6";
    }
    public void Button7()
    {
        CurrentTextNumber = CurrentTextNumber + "7";
    }
    public void Button8()
    {
        CurrentTextNumber = CurrentTextNumber + "8";
    }
    public void Button9()
    {
        CurrentTextNumber = CurrentTextNumber + "9";
    }
    public void Button0()
    {
        CurrentTextNumber = CurrentTextNumber + "0";

    }
    public void Button_Enter()
    {

    }
    public void Button_X()
    {

    }

    public void UpdateNumberText()
    {
       // UserNumberObj.GetComponent<TextMeshPro>().SetText(CurrentNum.ToString());
    }

    private void Update()
    {

    }


}
