using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteManager : MonoBehaviour
{
    // base body sprite renderers
    [SerializeField] private SpriteRenderer[] bodySpriteRenderers;

    // customizable body sprite renderers
    [Serializable]
    private class CustomizableSpriteRenderer
    {
        [SerializeField] private BodyPartType bodyPartType;
        public BodyPartType BodyPartType => bodyPartType;
            
        [SerializeField] private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }

    [SerializeField] private CustomizableSpriteRenderer[] customizableSpriteRenderers;
    
    // equipment sprite renderers
    // TODO
    
    // body parameter
    private static readonly Vector3 BodyScale = Vector3.one;

    public void Flip(bool isFacingLeft)
    {
        transform.localScale = new Vector3(isFacingLeft ? -BodyScale.x : BodyScale.x, BodyScale.y, BodyScale.z);
    }
    public void ChangeBodySprite(BodyPartType bodyPartType, Sprite sprite)
    {
        GetCustomizableSpriteRenderer(bodyPartType).SpriteRenderer.sprite = sprite;
    }

    public void ChangeBodyColor(Color color)
    {
        foreach (SpriteRenderer spriteRenderer in bodySpriteRenderers)
        {
            spriteRenderer.color = color;
        }

        GetCustomizableSpriteRenderer(BodyPartType.Ear).SpriteRenderer.color = color;
    }

    public void ChangeHairColor(Color color)
    {
        GetCustomizableSpriteRenderer(BodyPartType.Hair).SpriteRenderer.color = color;
    }

    public void ChangeEyeColor(Color color)
    {
        GetCustomizableSpriteRenderer(BodyPartType.Eye).SpriteRenderer.color = color;
    }

    private CustomizableSpriteRenderer GetCustomizableSpriteRenderer(BodyPartType bodyPartType)
    {
        return customizableSpriteRenderers.FirstOrDefault(customizableSpriteRenderer =>
            customizableSpriteRenderer.BodyPartType == bodyPartType);
    }
}
