using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Key Settings")]
    public string keyUp="w";
    public string keyDown="s";
    public string keyLeft="a";
    public string keyRight="d";

    public KeyCode keyA = KeyCode.LeftShift;
    public KeyCode keyB = KeyCode.Space;
    public string keyC;
    public string keyD;
    public string keyE;

    public string keyJUp;
    public string keyJDown;
    public string keyJLeft;
    public string keyJRight;

    [Header("Output Signals")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    public bool run;

    public bool jump=false;
    private bool lastJump=false;

    public bool lattack = false;
    public bool rattack = false;
    public bool isEquiped =false;
    private bool lastLAttack = false;
    private bool lastRAttack = false;

    [Header("Others")]
    public bool inputEnabled=true;
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    private float _Dup;
    private float _Dright;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移动
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);


        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);


        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        SquareMapToCircle(Dup, Dright);

        Dmag = Mathf.Sqrt((_Dup * _Dup) + (_Dright * _Dright));
        Dvec= _Dright * transform.right + _Dup * transform.forward;


        //跑步
        run = Input.GetKey(keyA);

        //跳跃
        bool tempJump = Input.GetKey(keyB);  
        if (tempJump!=lastJump)
        {
            jump= tempJump;
        }
        lastJump = tempJump;

        //攻击
        bool tempLAttack = Input.GetKey(keyC);
        if (tempLAttack != lastLAttack)
        {
            lattack = tempLAttack;
        }
        lastLAttack = tempLAttack;

        bool tempRAttack = Input.GetKey(keyD);
        if (tempRAttack != lastRAttack)
        {
            rattack = tempRAttack;
        }
        lastRAttack = tempRAttack;
    }
    private void SquareMapToCircle(float x ,float y)//解决斜方向速度1.414问题
    {
        _Dup = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        _Dright = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
    }
}
