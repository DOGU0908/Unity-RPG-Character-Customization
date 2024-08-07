using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public void Flip(bool isFacingLeft)
    {
        Vector3 localScale = Vector3.one;
        localScale.x *= isFacingLeft ? -1 : 1;

        transform.localScale = localScale;
    }
}
