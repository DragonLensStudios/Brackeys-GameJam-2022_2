using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueTriggerZone : MonoBehaviour
{
    [SerializeField]
    private BlockReference block;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            block.Execute();
        }
    }
}
