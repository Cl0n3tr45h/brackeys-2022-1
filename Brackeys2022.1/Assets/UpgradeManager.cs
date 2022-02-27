using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum upgradeOptions{DAMAGE, RANGE, FIRERATE}
public class UpgradeManager : MonoBehaviour
{
    public upgradeOptions options;

    public Gun gun;

        // upgrade behavior
    private int remainingChoiceCount;
    public int upgradeChoiceCount;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textRange;
    public TextMeshProUGUI textFireRate;
    public TextMeshProUGUI textChoices;

    public Button btnDamage;
    public Button btnRange;
    public Button btnFireRate;
    
    private List<ComplexNumberData> availableChoices;


    public void Update(){
        if (availableChoices != null){
        if (this.gameObject.activeSelf)
        {
            textDamage.text = gun.Damage.Print();


            var a = "";
            for (var i = 0; i < upgradeChoiceCount; i++)
            {
                if (i == 0)
                    a += "Current Upgrade: ";
                else
                    a += "Next Upgrade: ";

                var item = availableChoices[i];

                a += item.Print();
                a += "\n";
            }
            textChoices.text = a;
        }
        }
    }
    public void GenerateUpgrades()
    {
        for (int i = 0; i < upgradeChoiceCount; i++)
        {
            Debug.Log("Gener");
            ComplexNumberObject newNumber = new ComplexNumberObject();
            if (availableChoices == null)
                availableChoices = new List<ComplexNumberData>();
            availableChoices.Add(newNumber.ComplexNumber);
            Debug.Log(newNumber.Print());
        }
    }

    public void upgradeGunAtStatWith(int _stat, ComplexNumberData add)
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
    }

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
    }
}
