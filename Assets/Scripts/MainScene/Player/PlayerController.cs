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
        switch (moveType)
        {
            case "WASD":
                // 手槍發射
                if (Input.GetKeyDown(KeyCode.Q) && weapon.weaponType == Weapon.WeaponType.Pistol)
                {
                    weapon.Fire();
                }

                // 充能控制
                if (Input.GetKeyDown(KeyCode.E))
                {
                    weapon.StartCharging();
                }
                if (Input.GetKeyUp(KeyCode.P))
                {
                    weapon.StopCharging();
                }
                break;
            case "UDLR":
                // 手槍發射
                if (Input.GetKeyDown(KeyCode.O) && weapon.weaponType == Weapon.WeaponType.Pistol)
                {
                    weapon.Fire();
                }

                // 充能控制
                if (Input.GetKeyDown(KeyCode.P))
                {
                    weapon.StartCharging();
                }
                if (Input.GetKeyUp(KeyCode.P))
                {
                    weapon.StopCharging();
                }
                break;
            default:
                break;
        }
    }

    void MoveTypeSelection()
    {
        Vector3 moveDirection = Vector3.zero;

        switch (moveType)
        {
            case "WASD":
                // WASD
                if (Input.GetKey(KeyCode.W))
                {
                    moveDirection += Vector3.forward;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveDirection += Vector3.back;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveDirection += Vector3.left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveDirection += Vector3.right;
                }
                break;
            case "UDLR":
                // UDLR
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    moveDirection += Vector3.forward;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    moveDirection += Vector3.back;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveDirection += Vector3.left;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveDirection += Vector3.right;
                }
                break;
            default:
                // WASD
                if (Input.GetKey(KeyCode.W))
                {
                    moveDirection += Vector3.forward;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveDirection += Vector3.back;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveDirection += Vector3.left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveDirection += Vector3.right;
                }
                break;
        }

        // 移動角色
        if (moveDirection != Vector3.zero)
        {
            // 移動
            transform.Translate(moveDirection.normalized * Time.deltaTime * player.speed);

            // 旋轉角色
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            player.PlayerModel.transform.rotation = toRotation;
        }
    }
}
