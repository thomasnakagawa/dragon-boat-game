using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRenderer : MonoBehaviour
{
    [SerializeField] private Transform GrabPosition = default;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            throw new MissingComponentException("Needs line renderer");
        }
        if (GrabPosition == null)
        {
            throw new MissingReferenceException("Needs grab transform");
        }
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GrabPosition.position);
    }
}
