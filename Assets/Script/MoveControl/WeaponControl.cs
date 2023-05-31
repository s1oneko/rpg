using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField]
    private GameObject backWeapon1;
    [SerializeField]
    private GameObject backWeapon2;
    [SerializeField]
    private GameObject handWeapon1;
    [SerializeField]
    private GameObject handWeapon2;

    public void WeaponSwitch()
    {
        backWeapon1.SetActive(!backWeapon1.activeSelf);
        backWeapon2.SetActive(!backWeapon2.activeSelf);
        handWeapon1.SetActive(!handWeapon1.activeSelf);
        handWeapon2.SetActive(!handWeapon2.activeSelf);
    }
}
