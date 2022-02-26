using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneShift : MonoBehaviour
{
    public UnityEvent OnShiftToImaginary;
    public UnityEvent OnShiftToReal;
    public UnityEvent OnShift;

    public static bool InReal = true;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (OnShiftToImaginary == null)
        {
            OnShift = new UnityEvent();
            OnShiftToReal = new UnityEvent();
            OnShiftToImaginary = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && OnShiftToReal != null)
        {
            FindObjectOfType<AudioManager>().Play("ShiftPlane");
            InReal = !InReal;
            if (InReal == true)
            {
                OnShiftToReal.Invoke();
            }
            else
            {
                OnShiftToImaginary.Invoke();
            }

            OnShift.Invoke();
        }
    }
}
