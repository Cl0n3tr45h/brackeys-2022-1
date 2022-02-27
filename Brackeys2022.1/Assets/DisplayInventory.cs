using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    
    public GameObject NumberSlot;

    private static GameObject[] panels;
    // Start is called before the first frame update
    void Start()
    {
        panels = new GameObject[PlayerInventoryManager.ComplexNumbers.Count];
        for (int i = 0; i < PlayerInventoryManager.ComplexNumbers.Count; i++)
        {
            var slot = Instantiate(NumberSlot, this.transform);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = PlayerInventoryManager.ComplexNumbers[i].Print();
            slot.GetComponentInChildren<PlayerInventorySlotData>().ComplexNumber = PlayerInventoryManager.ComplexNumbers[i];
            panels[i] = slot;
        }
    }

    public static GameObject FirstEmptyPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].transform.childCount == 0)
            {
                Debug.Log(i);
                return panels[i];
            }
        }
        Debug.Log("NO EMPTY PANELS FOUND");
        return null;
    }
}
