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
    public MyButton L1 = new(); //�ܲ�
    public MyButton X  = new(); //��Ծ
    public MyButton R1 = new(); //�ṥ��
    public MyButton R2 = new(); //�ع���
    public MyButton L2 = new(); //����
    public MyButton R3 = new(); //����

    void Update()
    {
        L1.Tick(Input.GetButton("L1"));
        L2.Tick(Input.GetButton("L2"));
        R1.Tick(Input.GetButton("R1"));
        R2.Tick(Input.GetButton("R2"));
        X.Tick(Input.GetButton("X"));
        R3.Tick(Input.GetButton("R3"));
        //�ƶ�
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

        //�ܲ�
        run = (L1.onPressing&&!L1.onDelaying)||L1.onExtending;
        //��Ծ
        jump = X.onPressed;
        //����
        roll = L1.onReleased && L1.onDelaying;
        //����
        lattack=R1.onPressed;
        rattack=R2.onPressed;
        //����
        defence = L2.onPressed;
        //����
        lockon = R3.onPressed;
    }
}
