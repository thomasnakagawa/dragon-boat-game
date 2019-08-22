using UnityEngine;
using System.Collections;

public class WaterTextureScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time) * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
