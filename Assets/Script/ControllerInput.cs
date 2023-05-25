using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : UserInput
{
    [Header("Controller Setting")]
    public string axisX = "Dright";
    public string axisY = "Dup";
    public string axis3 = "Jright";
    public string axis6 = "Jup";
    public string L1 = "L1"; //�ܲ�
    public string X = "X";   //��Ծ
    public string R1 = "R1"; //�ṥ��
    public string R2 = "R2"; //�ع���
    public string L2 = "L2";

    void Update()
    {
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

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        SquareMapToCircle(Dup, Dright);

        Dmag = Mathf.Sqrt((_Dup * _Dup) + (_Dright * _Dright));
        Dvec = _Dright * transform.right + _Dup * transform.forward;

        //�ܲ�
        run = Input.GetButton(L1);

        //��Ծ
        bool tempJump = Input.GetButton(X);
        if (tempJump != lastJump)
        {
            jump = tempJump;
        }
        lastJump = tempJump;

        //����
        bool tempLAttack = Input.GetButton(R1);
        if (tempLAttack != lastLAttack)
        {
            lattack = tempLAttack;
        }
        lastLAttack = tempLAttack;

        bool tempRAttack = Input.GetButton(R2);
        if (tempRAttack != lastRAttack)
        {
            rattack = tempRAttack;
        }
        lastRAttack = tempRAttack;

        bool tempDefence = Input.GetButton(L2);
        if (tempDefence != lastDefence)
        {
            defence= tempDefence;
        }
        lastDefence = tempDefence;
    }
}
