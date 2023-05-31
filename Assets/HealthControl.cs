using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour,IDamagar
{
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected AudioSource audioSource;

    protected Transform currentAttacker;
    protected virtual void Awake()
    {

    }
    protected virtual void Update()
    {

    }
    public virtual void SetAttacker(Transform attacker)
    {
        if (currentAttacker != attacker || currentAttacker == null)
            currentAttacker = attacker;
    }
    // Start is called before the first frame update
    public virtual void TakeDamager(float damager)
    {
        throw new NotImplementedException();
    }

    public virtual void TakeDamager(string hitAnimationName)
    {
        animator.Play(hitAnimationName, 0, 0f);
        GameAssets.Instance.PlaySoundEffect(audioSource, SoundAssetsType.hit);
    }

    public virtual void TakeDamager(float damager, string hitAnimationName)
    {
        throw new NotImplementedException();
    }

    public virtual void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
    {
        SetAttacker(attacker);
    }

}
