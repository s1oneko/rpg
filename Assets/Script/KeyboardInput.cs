using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : UserInput
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


    // Update is called once per frame
    void Update()
    {
        //ÒÆ¶¯
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


        //ÅÜ²½
        run = Input.GetKey(keyA);

        //ÌøÔ¾
        bool tempJump = Input.GetKey(keyB);  
        if (tempJump!=lastJump)
        {
            jump= tempJump;
        }
        lastJump = tempJump;

        //¹¥»÷
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
}
