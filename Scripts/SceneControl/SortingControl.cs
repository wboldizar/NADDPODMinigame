using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingControl : MonoBehaviour
{
    public void ChangeLayer(string newLayer, SpriteRenderer sprite)
    {
        sprite.sortingLayerName = newLayer;
    }

    public void ChangeGroupLayer(string newLayer, SpriteRenderer[] sprites)
    {
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.sortingLayerName = newLayer;
        }
    }
}
