using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShift : MonoBehaviour
{
    public Collider2D collider;
    public bool isReal;

    private PlaneShift planeShift;

    private void Awake()
    {
        planeShift = GameObject.Find("GameManager").GetComponent<PlaneShift>();
    }

    private void OnEnable()
    {
        planeShift.OnShiftToReal?.AddListener(SetColliderReal);
        planeShift.OnShiftToImaginary?.AddListener(SetColliderImaginary);
    }

    private void OnDisable()
    {
        planeShift.OnShiftToImaginary?.RemoveListener(SetColliderImaginary);
        planeShift.OnShiftToReal?.RemoveListener(SetColliderReal);
    }

    public void Start()
    {
        collider.isTrigger = !isReal;
    }
    public void SetColliderReal()
    {
        collider.isTrigger = !isReal;
    }
    public void SetColliderImaginary()
    {
        collider.isTrigger = isReal;
    }
}
