using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectable : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private TMP_Text text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            cherries++;
            Destroy(collision.gameObject);
            text.text = $"Cherries {cherries}";
        }
    }
}
