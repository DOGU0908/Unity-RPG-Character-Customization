using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteShadow : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.shadowCastingMode = ShadowCastingMode.On;
        spriteRenderer.receiveShadows = true;
    }
}
