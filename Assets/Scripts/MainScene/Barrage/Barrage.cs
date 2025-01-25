using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class Barrage : MonoBehaviour
{
    public GameObject BarragePrefab;
    public float reachTime = 7.0f; // 導彈到達目標時間
    
    private bool isReach = false;
    
    void Start()
    {
        StartCoroutine(ReachTimer());
        StartCoroutine(Shooting());
    }

    public IEnumerator ReachTimer()
    {
        yield return new WaitForSeconds(reachTime);
        isReach = true;
    }

    public IEnumerator Shooting()
    {
        yield return new WaitUntil(() => isReach);
        FortUtils.shot(this.gameObject, "Barrage_Big", Vector3.down, new Vector3(0, 20, 0)); // 1顆導彈
        this.gameObject.SetActive(false); // 將物件關閉

        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}