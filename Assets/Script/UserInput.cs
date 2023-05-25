using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput : MonoBehaviour //û�취ʵ����
{
    [Header("Output Signals")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    public bool run;

    public bool jump = false;
    protected bool lastJump = false;

    public bool lattack = false;
    public bool rattack = false;
    public bool defence = false;
    public bool isEquiped = false;
    protected bool lastLAttack = false;
    protected bool lastRAttack = false;
    protected bool lastDefence = false;

    [Header("Others")]
    public bool inputEnabled = true;
    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;
    protected float _Dup;
    protected float _Dright;

    protected void SquareMapToCircle(float x, float y)//���б�����ٶ�1.414����
    {
        _Dup = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        _Dright = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
    }
}
