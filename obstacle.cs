using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            transform.position = startPos;

        }


    }



}
