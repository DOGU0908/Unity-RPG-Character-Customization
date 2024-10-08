using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    private static readonly Dictionary<BodyPartType, string> BodyPartTypeDisplayName =
        new()
        {
            { BodyPartType.Hair, "Hair" },
            { BodyPartType.Ear, "Ears" },
            { BodyPartType.Eye, "Eyes" },
            { BodyPartType.Eyebrow, "Eyebrows" },
            { BodyPartType.Mouth, "Mouth" },
        };

    [Serializable]
    private abstract class BodyCustomizer
    {
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        
        [SerializeField] protected TextMeshProUGUI text;
        
        private int _index = 0;
        public int Index => _index;

        protected void SetupButtonOnClickEvents(Action updateAction, int optionCount)
        {
            previousButton.onClick.AddListener(() =>
            {
                _index = (_index - 1 + optionCount) % optionCount;
                updateAction();
            });
            
            nextButton.onClick.AddListener(() =>
            {
                _index = (_index + 1) % optionCount;
                updateAction();
            });
        }
        
        protected abstract void UpdateText();
    }
    
    // body sprite customization
    [Serializable]
    private class BodySpriteCustomizer : BodyCustomizer
    {
        [SerializeField] private BodyPartType bodyPartType;
        public BodyPartType BodyPartType => bodyPartType;

        public void Initialize(Action<BodyPartType, int> onIndexChange)
        {
            onIndexChange(bodyPartType, Index);
            UpdateText();
            
            int optionCount = BodySpriteCollection.Instance.GetSpritesCount(bodyPartType);

            SetupButtonOnClickEvents(() =>
            {
                onIndexChange(bodyPartType, Index);
                UpdateText();
            }, optionCount);
        }

        protected override void UpdateText()
        {
            text.text = $"{BodyPartTypeDisplayName[bodyPartType]} {Index}";
        }
    }

    [SerializeField] private BodySpriteCustomizer[] bodySpriteCustomizers;
    
    // body color customization
    [Serializable]
    private class BodyColorCustomizer : BodyCustomizer
    {
        [SerializeField] private ColorType colorType;
        
        public void Initialize(Action<Color> onIndexChange)
        {
            onIndexChange(BodySpriteCollection.Instance.GetColorData(colorType, Index).Color);
            UpdateText();
            
            int optionCount = BodySpriteCollection.Instance.GetColorDataCount(colorType);

            SetupButtonOnClickEvents(() =>
            {
                onIndexChange(BodySpriteCollection.Instance.GetColorData(colorType, Index).Color);
                UpdateText();
            }, optionCount);
        }

        protected override void UpdateText()
        {
            text.text = BodySpriteCollection.Instance.GetColorData(colorType, Index).Name;
        }
    }

    [SerializeField] private BodyColorCustomizer hairColorCustomizer;
    [SerializeField] private BodyColorCustomizer eyeColorCustomizer;
    [SerializeField] private BodyColorCustomizer skinColorCustomizer;
    
    // confirm button
    [SerializeField] private Button confirmButton;
    
    // display character sprite manager
    [SerializeField] private SpriteManager spriteManager;
    
    // character base info
    [SerializeField] private int baseId = 0;
    [SerializeField] private string baseName;
    [SerializeField] private StatSet baseStats;
    [SerializeField] private int baseLevel;
    [SerializeField] private int baseWeaponId;
    [SerializeField] private int baseArmorId;
    
    // use Start because display character that stores spriteManager might not be instantiated
    private void Start()
    {
        foreach (BodySpriteCustomizer bodySpriteCustomizer in bodySpriteCustomizers)
        {
            bodySpriteCustomizer.Initialize(ChangeDisplayCharacterSprite);
        }
        
        hairColorCustomizer.Initialize(spriteManager.SetHairColor);
        eyeColorCustomizer.Initialize(spriteManager.SetEyeColor);
        skinColorCustomizer.Initialize(spriteManager.SetBodyColor);
        
        spriteManager.SetWeaponSprite(WeaponCollection.Instance.GetWeapon(baseWeaponId).Sprite);
        spriteManager.SetArmorSprite(ArmorCollection.Instance.GetArmor(baseArmorId).Sprite);
        
        confirmButton.onClick.AddListener(() =>
        {
            BodyPart[] bodyParts = new BodyPart[bodySpriteCustomizers.Length];

            for (int i = 0; i < bodySpriteCustomizers.Length; ++i)
            {
                bodyParts[i] = new BodyPart(bodySpriteCustomizers[i].BodyPartType,
                    bodySpriteCustomizers[i].Index);
            }

            CharacterInfo newCharacterInfo = new CharacterInfo(baseId, baseName, bodyParts, hairColorCustomizer.Index,
                eyeColorCustomizer.Index, skinColorCustomizer.Index, baseStats, baseLevel, baseWeaponId, baseArmorId);

            MainMenuManager.Instance.ConfirmCustomization(newCharacterInfo);
        });
    }

    private void ChangeDisplayCharacterSprite(BodyPartType bodyPartType, int index)
    {
        spriteManager.SetBodySprite(bodyPartType, BodySpriteCollection.Instance.GetSprite(bodyPartType, index));
    }
}
