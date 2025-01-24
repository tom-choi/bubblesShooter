using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerName;
    public string moveType = "WASD";
    public GameObject holo; // Holocube
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTypeSelection();
    }

    void MoveTypeSelection()
    {
        switch (moveType)
        {
            case "WASD":
                // WASD
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * 10);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * Time.deltaTime * 10);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * 10);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 10);
                }
                break;
            case "UDLR":
                // UDLR
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * 10);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.back * Time.deltaTime * 10);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * 10);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 10);
                }
                break;
            default:
                break;
        }
    }
}
