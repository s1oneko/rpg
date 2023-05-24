using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
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

    [Header("Output Signals")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    public bool run;

    public bool jump = false;
    private bool lastJump = false;

    public bool lattack = false;
    public bool rattack = false;
    public bool isEquiped = false;
    private bool lastLAttack = false;
    private bool lastRAttack = false;

    [Header("Others")]
    public bool inputEnabled = true;
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    private float _Dup;
    private float _Dright;

    // Update is called once per frame
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
    }
    private void SquareMapToCircle(float x, float y)//���б�����ٶ�1.414����
    {
        _Dup = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        _Dright = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
    }
}
