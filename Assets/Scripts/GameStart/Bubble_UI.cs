using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_UI : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * Random.Range(300,400);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
