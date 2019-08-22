using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Rigidbody boatRB;
    public TMPro.TMP_Text speedText;
    public TMPro.TMP_Text distText;
    public TMPro.TMP_Text timeText;
    public Leaderboard leaderboard;

    [SerializeField] private AudioClip WinSound = default;
    [SerializeField] private AudioClip AltWinSound = default;

    private float elapsedTime = 0f;
    private bool raceStarted = false;
    private float startedAt;

    private bool raceEnded = false;
    private float endedAt;

    private Vector3 boatInitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        boatInitialPosition = boatRB.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = boatRB.velocity.magnitude.ToString("0.00") + " m/s";
        distText.text = Vector3.Distance(boatInitialPosition, boatRB.transform.position).ToString("0.00") + "m / 200m";
        if (raceEnded)
        {
            timeText.text = (endedAt - startedAt).ToString("0.00") + "s";
        }
        else if (raceStarted)
        {
            timeText.text = (Time.time - startedAt).ToString("0.00") + "s";
        }

        if (!raceStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                raceStarted = true;
                timeText.color = Color.red;
                startedAt = Time.time;
            }
        }

        if (leaderboard.isOpen() == false &&  Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            leaderboard.open(999);
        }
    }

    public void endRace()
    {
        timeText.color = Color.white;
        raceEnded = true;
        endedAt = Time.time;

        leaderboard.open(endedAt - startedAt);

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            if (endedAt - startedAt > 90f)
            {
                audioSource.PlayOneShot(AltWinSound);
            }
            else
            {
                audioSource.PlayOneShot(WinSound);
            }
        }
    }
}
