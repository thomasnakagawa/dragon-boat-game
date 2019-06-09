using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingLine : MonoBehaviour
{

    public HUD hud;

    private void OnTriggerEnter(Collider other)
    {
        hud.endRace();
    }
}
