using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteManager : MonoBehaviour
{
    // body sprite renderer
    [SerializeField] private SpriteRenderer headSprite;
    [SerializeField] private SpriteRenderer torsoSprite;

    public void Flip(bool isFacingLeft)
    {
        Vector3 localScale = Vector3.one;
        localScale.x *= isFacingLeft ? -1 : 1;

        transform.localScale = localScale;
    }
}
