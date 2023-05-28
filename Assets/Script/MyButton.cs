using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton 
{
    public bool onPressing=false;
    public bool onPressed=false;
    public bool onReleased = false;
    public bool onExtending =false;
    public bool onDelaying = false;

    private bool curState =false;
    private bool lastState =false;

    private MyTimer extendTimer = new();
    private MyTimer delayTimer = new();
    public void Tick(bool input)
    {
        extendTimer.Tick();
        delayTimer.Tick();

        curState = input;

        onPressing = curState;

        onPressed = false;
        onReleased = false;
        onExtending = false;
        onDelaying = false;

        if(curState!=lastState)
        {
            if (curState)
            {
                onPressed = true;
                StartTimer(delayTimer, 0.15f);

            }
            else
            {
                onReleased = true;
                StartTimer(extendTimer, 0.15f);

            }
        }
        lastState = curState;
        onExtending = extendTimer.state == MyTimer.STATE.RUN;
        onDelaying = delayTimer.state == MyTimer.STATE.RUN;
    }
    private void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}
