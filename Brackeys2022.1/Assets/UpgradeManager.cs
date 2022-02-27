using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum upgradeOptions
{
    DAMAGE,
    RANGE, 
    FIRERATE
}
public class UpgradeManager : MonoBehaviour
{
    public upgradeOptions options;

    public Gun gun;

        // upgrade behavior
    private int remainingChoiceCount;
    public int UpgradeChoiceCount;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textRange;
    public TextMeshProUGUI textFireRate;
    public TextMeshProUGUI textChoices;

    public Button btnDamage;
    public Button btnRange;
    public Button btnFireRate;
    public Button BtnNextLvl;
    
    private List<ComplexNumberData> availableChoices = new List<ComplexNumberData>();


    private void OnEnable()
    {
        
        textChoices.gameObject.SetActive(true);
        SetTexts();
        SetButtons(true);
    }

    public void SetTexts()
    {
        
        textDamage.text = "Damage: "+gun.Damage.RichPrint();
        textRange.text = "Range: "+gun.Range.RichPrint();
        textFireRate.text = "Fire Rate: "+gun.FireRate.RichPrint();
    }

    public void SetUpgradeQueueText()
    {
        textChoices.text = "Current Upgrade: \n";
        foreach (var number in availableChoices)
        {
            
            textChoices.text += ("Next Upgrade: "+number.RichPrint());
        }
    }
    
    public void Update(){
        
        
        // if (availableChoices != null){
        //     if (this.gameObject.activeSelf)
        //     {
        //         textDamage.text = gun.Damage.Print();
        //
        //
        //         var a = "";
        //         for (var i = 0; i < upgradeChoiceCount; i++)
        //         {
        //             if (i == 0)
        //                 a += "Current Upgrade: ";
        //             else
        //                 a += "Next Upgrade: ";
        //
        //             var item = availableChoices[i];
        //
        //             a += item.Print();
        //             a += "\n";
        //         }
        //         textChoices.text = a;
        //     }
        // }
    }
    public void GenerateUpgrades()
    {
        availableChoices = new List<ComplexNumberData>();
        textChoices.text = "Current Upgrade: ";
        for (int i = 0; i < UpgradeChoiceCount; i++)
        {
            ComplexNumberData newNumber = new ComplexNumberData(true);
            availableChoices.Add(newNumber);
            textChoices.text += ("\n"+newNumber.RichPrint()+"\nNextUpgrade: ");
        }
    }

    public void ButtonClick(int _stat)
    {
        //Button needs to know what stat
        /*switch (_stat)
        {
            case upgradeOptions.DAMAGE:
                gun.Damage = ComplexNumberData.Add(gun.Damage, availableChoices[0]);
                break;
            case upgradeOptions.RANGE:
                gun.Range = ComplexNumberData.Add(gun.Range, availableChoices[0]);
                break;
            case upgradeOptions.FIRERATE:
                gun.FireRate = ComplexNumberData.Add(gun.FireRate, availableChoices[0]);
                break;    
            default:
                Debug.LogError("NO GUN STAT CORRESPONDING TO UPGRADE BUTTON");
                break;
        }*/
        switch (_stat)
        {
            case 0 :
                gun.Damage = ComplexNumberData.Add(gun.Damage, availableChoices[0]);
                break;
            case 1:
                gun.Range = ComplexNumberData.Add(gun.Range, availableChoices[0]);
                break;
            case 2:
                gun.FireRate = ComplexNumberData.Add(gun.FireRate, availableChoices[0]);
                break;    
            default:
                Debug.LogError("NO GUN STAT CORRESPONDING TO UPGRADE BUTTON");
                break;
        }
        SetTexts();
        SetUpgradeQueueText();
        availableChoices.RemoveAt(0);
        if (availableChoices.Count <= 0)
        {
            SetButtons(false);
        }
    }

    public void SetButtons(bool _active)
    {
        textChoices.gameObject.SetActive(_active);
        btnDamage.interactable = _active;
        btnRange.interactable = _active;
        btnFireRate.interactable = _active;
        BtnNextLvl.interactable = !_active;
    }
    
    /*public void upgradeGunAtStatWith(int _stat, ComplexNumberData add)
    {
        switch (_stat)
        {
            //dmg
            case 0:
                var new_stat = ComplexNumberData.Add(gun.Damage, add);
                gun.Damage = new_stat;
                break;
            // range
            case 1:
                var new_stat2 = ComplexNumberData.Add(gun.Range, add);
                gun.Range = new_stat2;
                break;
            
            // fire rate
            case 2:
                var new_stat3 = ComplexNumberData.Add(gun.FireRate, add);
                gun.FireRate = new_stat3;
                break;
            default:
                break;
    }
    }*/

    /*
    public void upgradeGun(int _stat)
    {
        if (availableChoices.Count > 0)
        {
            upgradeGunAtStatWith(_stat, availableChoices[0]);

            availableChoices.RemoveAt(0);

            SetRemainingChoiceCount(remainingChoiceCount - 1);
        }

        

    }

    private void SetRemainingChoiceCount(int newVal)
    {
        remainingChoiceCount = newVal;
        if( remainingChoiceCount <= 0)
        {
            btnDamage.gameObject.SetActive(false);
            btnRange.gameObject.SetActive(false);
            btnFireRate.gameObject.SetActive(false);
        }
    }*/
}
