using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float HealthActive = 100f;
    public float HealthInActive = 100f;
    
    public Image HealthBarFillActive;
    public TextMeshProUGUI CurrentHPActive;
    public TextMeshProUGUI CurrentHPInActive;
    public Image HealthBarFillInActive;
    
    //Invincibility?


    private PlaneShift planeShift;
    private void OnEnable()
    {
        planeShift = GameObject.Find("GameManager").GetComponent<PlaneShift>();
        planeShift.OnShift?.AddListener(OnShift);
    }

    public void TakeDamageReal(float _damage)
    {
        HealthActive -= _damage;
        if (HealthActive <= 0)
        {
            //GAME OVER
        }
        //update UI
        UpdateUI();
    }

    public void UpdateUI()
    {
        
        HealthBarFillActive.fillAmount = HealthActive / 100f;
        CurrentHPActive.text = HealthActive.ToString();
    }
    
    public void OnShift()
    {
        //swap images
        var imagehelper = HealthBarFillActive.sprite;
        HealthBarFillActive.sprite = HealthBarFillInActive.sprite;
        HealthBarFillInActive.sprite = imagehelper;

        //swap text
        var texthelper = CurrentHPActive.text;
        CurrentHPActive.text = CurrentHPInActive.text;
        CurrentHPInActive.text = texthelper;

        //swap health
        var healthhelper = HealthActive;
        HealthActive = HealthInActive;
        HealthInActive = healthhelper;
        UpdateUI();
    }
}
