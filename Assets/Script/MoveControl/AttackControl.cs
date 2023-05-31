using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField, Header("AttackDetection")] protected Transform attackDetectionCenter;
    [SerializeField] protected float attackDetectionRang;
    [SerializeField] protected LayerMask enemyLayer;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void OnAnimationAttack(string msg)
    {
        Collider[] attackDetectionTargets = new Collider[4];
        int counts = Physics.OverlapSphereNonAlloc(attackDetectionCenter.position, attackDetectionRang,
            attackDetectionTargets, enemyLayer);
        if (counts > 0)
        {
            for (int i = 0; i < counts; i++)
            {
                if (attackDetectionTargets[i].TryGetComponent(out IDamagar damagar))
                {
                    damagar.TakeDamager(msg);
                }
            }
        }
        PlayWeaponEffect();
    }
    private void PlayWeaponEffect() 
    {
        if (animator.CheckAnimationTag("Attack"))
        {
            GameAssets.Instance.PlaySoundEffect(audioSource, SoundAssetsType.swordWave);
        }
        if (animator.CheckAnimationTag("RAttack"))
        {
            GameAssets.Instance.PlaySoundEffect(audioSource, SoundAssetsType.hSwordWave);
        }

    }
}
