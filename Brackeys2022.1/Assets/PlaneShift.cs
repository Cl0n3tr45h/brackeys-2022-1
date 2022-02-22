using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneShift : MonoBehaviour
{
    public UnityEvent OnShiftToImaginary;
    public UnityEvent OnShiftToReal;
    public UnityEvent OnShift;

    private bool InReal = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
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
