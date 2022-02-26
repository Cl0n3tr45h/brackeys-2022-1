using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Gun Gun;
    private void OnEnable()
    {
        Text?.GetComponentInChildren<TextMeshProUGUI>();
        Text.text = "Damage: " + Gun.Damage.Print() + "\n Fire Rate: " + Gun.FireRate.Print() + "\n Range: " +Gun.Range.Print();
    }
}
