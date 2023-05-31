using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossBasic
{
    protected GameObject target;

    protected float target_hp;
    protected float target_current_hp;
    protected float target_atk;
    protected float target_spd;
    protected float target_def;
    protected float target_avd;
    protected float target_rng;

    protected float self_hp;
    protected float self_current_hp;
    protected float self_atk;
    protected float self_spd;
    protected float self_def;
    protected float self_avd;
    protected float self_rng;

    protected bool isEffect=true;
    public void damage_operate()
    {

    }
    protected void take_move(int number)
    {
        animator.SetInteger("skill_number", number);
        damage_operate();
    }
    protected TaskStatus hitOrMiss()
    {
        if (isEffect)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

}
