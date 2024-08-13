using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BodySpriteCollection", menuName = "SpriteCollection/BodySpriteCollection")]
public class BodySpriteCollection : ScriptableObject
{
    private static BodySpriteCollection _instance;

    // body part sprites
    [Serializable]
    private class BodyPartSprites
    {
        [SerializeField] private BodyPartType bodyPartType;
        public BodyPartType BodyPartType => bodyPartType;
        
        [SerializeField] private Sprite[] sprites;
        public Sprite[] Sprites => sprites;
    }

    [SerializeField] private BodyPartSprites[] bodyPartSpriteData;

    public Sprite GetSprite(BodyPartType bodyPartType, int index)
    {
        BodyPartSprites bodyPartSprites = GetBodyPartSprites(bodyPartType);

        if (bodyPartSprites != null && index >= 0 && index < bodyPartSprites.Sprites.Length)
        {
            return bodyPartSprites.Sprites[index];
        }

        return null;
    }

    public int GetSpritesCount(BodyPartType bodyPartType)
    {
        BodyPartSprites bodyPartSprites = GetBodyPartSprites(bodyPartType);

        return bodyPartSprites?.Sprites.Length ?? 0;
    }

    private BodyPartSprites GetBodyPartSprites(BodyPartType bodyPartType)
    {
        return bodyPartSpriteData.FirstOrDefault(bodyPartSprites => bodyPartSprites.BodyPartType == bodyPartType);
    }
    
    // body colors
    [SerializeField] private ColorData[] skinColorData;
    [SerializeField] private ColorData[] baseColorData;

    public ColorData GetColorData(ColorType colorType, int index)
    {
        ColorData[] colorData = colorType == ColorType.Skin ? skinColorData : baseColorData;

        if (index >= 0 && index < colorData.Length)
        {
            return colorData[index];
        }

        return colorData[0];
    }

    public int GetColorDataCount(ColorType colorType)
    {
        return colorType == ColorType.Skin ? skinColorData.Length : baseColorData.Length;
    }
   
    public static BodySpriteCollection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<BodySpriteCollection>("BodySpriteCollection");
            }

            return _instance;
        }
    }
}

public enum BodyPartType
{
    Hair,
    Eye,
    Eyebrow,
    Ear,
    Mouth,
}

public enum ColorType
{
    Skin,
    Base
}

[Serializable]
public struct ColorData
{
    [SerializeField] private Color color;
    public Color Color => color;

    [SerializeField] private string name;
    public string Name => name;
}