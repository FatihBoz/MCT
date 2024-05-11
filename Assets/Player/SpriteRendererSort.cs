using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererSort : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.sortingOrder = -1* Mathf.RoundToInt(transform.position.y);
    }
}
