using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrummer : MonoBehaviour
{
    public Vector3 destination;
    public Vector3 destRotation;

    private bool flying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fly();
        }
        if (flying)
        {
            if (Vector3.Distance(transform.localPosition, destination) > 0.1)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, 700f * Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(destRotation), 0.8f * Time.deltaTime);
            }else
            {
                transform.localPosition = destination;
                transform.localRotation = Quaternion.Euler(destRotation);
                Destroy(this);
            }
        }
    }

    public void fly()
    {
        flying = true;
    }
}
