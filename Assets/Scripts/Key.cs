using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door door;
    public SpriteRenderer renderer;

    void OnTriggerEnter2D()
    {
        door.open = true;
        renderer.enabled = false;
    }
}
