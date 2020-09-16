using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D info)
    {
        Debug.Log(info.name);
        Destroy(gameObject);
    }
}
