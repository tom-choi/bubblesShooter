using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Weapon weapon;
    public string moveType = "WASD";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        MoveTypeSelection();
        WeaponControl();
    }

    void WeaponControl()
    {
        // 手槍發射
        if (Input.GetKeyDown(KeyCode.E) && weapon.weaponType == Weapon.WeaponType.Pistol)
        {
            weapon.Fire();
        }

        // 充能控制
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.StartCharging();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            weapon.StopCharging();
        }
    }

    void MoveTypeSelection()
    {
        switch (moveType)
        {
            case "WASD":
                // WASD
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * player.speed);
                }
                break;
            case "UDLR":
                // UDLR
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.back * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * player.speed);
                }
                break;
            default:
                // WASD
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * player.speed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * player.speed);
                }
                break;
        }
    }
}
