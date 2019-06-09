using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPusher : MonoBehaviour
{
    public float boostAmount;
    public float boostMultiplier = 5f;

    public Transform bladeTransform;

    public Rigidbody boatRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 force = bladeTransform.forward * boostAmount * boostMultiplier;
        Vector3 position = bladeTransform.position;
        boatRB.AddForceAtPosition(force, position);
    }
}
