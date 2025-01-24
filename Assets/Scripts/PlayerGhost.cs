using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Position of the ghost is between the two players
        transform.position = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
    }
}