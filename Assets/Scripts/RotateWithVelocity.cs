using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateWithVelocity : MonoBehaviour
{
    private Rigidbody2D rigid;
    public GameObject Target;

    public float MaxRotation = 3;
    public float GrowthFactor = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float currentRotateSpeed = rigid.velocity.magnitude * GrowthFactor;

        if (currentRotateSpeed > MaxRotation)
            currentRotateSpeed = MaxRotation;

        Target.transform.Rotate(0.0f, 0.0f, -currentRotateSpeed, Space.Self);
    }
}
