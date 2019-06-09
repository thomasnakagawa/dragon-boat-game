using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatOnWater : MonoBehaviour
{
    [SerializeField] private float bobDownAmount = 0.1f;
    [SerializeField] private float time = 1f;
    private Vector3 startPos;

    private float randomTimeMod;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        randomTimeMod = Random.Range(0f, 100f);
    }

    private void Update()
    {
        transform.position = startPos - (new Vector3(0f, Mathf.Sin(time * Time.time + randomTimeMod), 0f) * bobDownAmount);
    }
}
