using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.TextCore;

public class Block : MonoBehaviour
{
    public Material originalBlockMaterial;
    public Material lavaBlockMaterial;
    public bool isBlockLava = false;

    public IEnumerator FloorIsLava(float time = 5.0f)
    {
        this.gameObject.GetComponent<Renderer>().material = lavaBlockMaterial;
        isBlockLava = true;
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Renderer>().material = originalBlockMaterial;
        isBlockLava = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isBlockLava)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SubtractHealth(10); // 每秒扣10点血
            }
        }
    }


    private void Update() {
        
    }
    
}