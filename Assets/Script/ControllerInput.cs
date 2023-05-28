using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInput : UserInput
{
    [Header("Controller Setting")]
    public string axisX = "Dright";
    public string axisY = "Dup";
    public string axis3 = "Jright";
    public string axis6 = "Jup";
    public MyButton L1 = new(); //ÅÜ²½
    public MyButton X  = new(); //ÌøÔ¾
    public MyButton R1 = new(); //Çá¹¥»÷
    public MyButton R2 = new(); //ÖØ¹¥»÷
    public MyButton L2 = new(); //·ÀÓù
    public MyButton R3 = new(); //Ëø¶¨

    void Update()
    {
        L1.Tick(Input.GetButton("L1"));
        L2.Tick(Input.GetButton("L2"));
        R1.Tick(Input.GetButton("R1"));
        R2.Tick(Input.GetButton("R2"));
        X.Tick(Input.GetButton("X"));
        R3.Tick(Input.GetButton("R3"));
        //ÒÆ¶¯
        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX);


        Jup = Input.GetAxis(axis6);
        Jright =Input.GetAxis(axis3);


        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.2f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.2f);

        SquareMapToCircle(Dup, Dright);

        Dmag = Mathf.Sqrt((_Dup * _Dup) + (_Dright * _Dright));
        Dvec = _Dright * transform.right + _Dup * transform.forward;

        //ÅÜ²½
        run = (L1.onPressing&&!L1.onDelaying)||L1.onExtending;
        //ÌøÔ¾
        jump = X.onPressed;
        //·­¹ö
        roll = L1.onReleased && L1.onDelaying;
        //¹¥»÷
        lattack=R1.onPressed;
        rattack=R2.onPressed;
        //·ÀÓù
        defence = L2.onPressed;
        //Ëø¶¨
        lockon = R3.onPressed;
    }
}
