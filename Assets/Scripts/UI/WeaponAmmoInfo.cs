using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmoInfo : MonoBehaviour
{
    public static WeaponAmmoInfo instance;

    public Text currentAmmoAmount;
    public Text remainAmmoAmount;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateCurrentAmmoAmount(int num)
    {
        currentAmmoAmount.text = num.ToString();
    }

    public void UpdateRemainAmmoAmount(int num)
    {
        remainAmmoAmount.text = num.ToString();
    }
}
