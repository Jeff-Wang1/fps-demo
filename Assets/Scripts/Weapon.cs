using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerControl playerControl;

    public Animator weaponAnimator;
    


    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        playerControl = PlayerControl._instance;
    }
    
    void Update()
    {
        TestWeaponState();
    }

    private void TestWeaponState()
    {
        weaponAnimator.SetBool("isRunning", playerControl.isRunning);
        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimator.SetTrigger("fire");
        }
    }
}
