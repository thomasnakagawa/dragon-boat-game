using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddler : MonoBehaviour
{
    public float delay = 0.5f;
    public float speedVarience = 0.5f;
    private Animator animator;
    private AudioSource audioSource;
    private float initialPitch;
    private float lastHitAt = float.MinValue;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        initialPitch = audioSource.pitch;

        transform.Find("Person").transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void paddle()
    {

        float interval = Time.time - lastHitAt;
        lastHitAt = Time.time;
        if (interval < 2.5f)
        {
            StartCoroutine(paddleWithDelay(Random.Range(0f, delay)));
            animator.speed = 0.75f / (interval + Random.Range(0f, speedVarience));
            audioSource.pitch = (1f / interval) + Random.Range(0f, 0.5f);
            audioSource.volume = Random.Range(0.1f, 1f);
        }
    }

    private IEnumerator paddleWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Paddle");
    }
}
