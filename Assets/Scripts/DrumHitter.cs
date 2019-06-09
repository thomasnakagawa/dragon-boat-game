using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumHitter : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private Paddler[] paddlers;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Hit");
            audioSource.Play();
            foreach (Paddler paddler in paddlers)
            {
                if (paddler != null && paddler.gameObject.activeInHierarchy)
                {
                    paddler.paddle();
                }
            }
        }
    }
}
