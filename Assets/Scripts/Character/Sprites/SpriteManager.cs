using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteManager : MonoBehaviour
{
    // body sprite renderer
    [SerializeField] private SpriteRenderer[] bodySprites;
    
    // body color
    [SerializeField] private Color bodyColor = new Color(1.0f, 0.878f, 0.741f, 1.0f);
    public Color BodyColor
    {
        get => bodyColor;
        set
        {
            bodyColor = value;
            
            ApplyColorToBodySprite();
        }
    }
    
    // body parameter
    private static readonly Vector3 BodyScale = Vector3.one;

    private void Start()
    {
        ApplyColorToBodySprite();
    }

    public void Flip(bool isFacingLeft)
    {
        transform.localScale = new Vector3(isFacingLeft ? -BodyScale.x : BodyScale.x, BodyScale.y, BodyScale.z);
    }

    private void ApplyColorToBodySprite()
    {
        foreach (SpriteRenderer bodySprite in bodySprites)
        {
            bodySprite.color = bodyColor;
        }
    }
}
