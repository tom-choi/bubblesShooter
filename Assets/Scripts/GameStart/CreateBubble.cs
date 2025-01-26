using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBubble : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform ui;
    public float Randomtime;
    public bool canCreate;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < 15 ; i++)
        {
            GameObject newbubble = Instantiate(bubblePrefab,transform.position,transform.rotation);
            int Size = Random.Range(1,6);
            newbubble.transform.localScale = new Vector3(Size,Size,transform.localScale.z);
            newbubble.transform.SetParent(ui);
            newbubble.transform.position = new Vector3(Random.Range(100,1800),transform.position.y,transform.position.z);
            Destroy(newbubble,5f);
        }

        StartCoroutine(Openning());
    }

    // Update is called once per frame
    void Update()
    {
        if(!canCreate)
        {
            return;
        }
        if(Randomtime > 0)
        {
            Randomtime -= Time.deltaTime;
        }
        else if(Randomtime <= 0)
        {
            Randomtime = Random.Range(1,2f);
            GameObject newbubble = Instantiate(bubblePrefab,transform.position,transform.rotation);
            int Size = Random.Range(1,6);
            newbubble.transform.localScale = new Vector3(Size,Size,transform.localScale.z);
            newbubble.transform.SetParent(ui);
            newbubble.transform.position = new Vector3(Random.Range(300,1800),transform.position.y,transform.position.z);
            Destroy(newbubble,5f);
        }
    }

    IEnumerator Openning()
    {
        yield return new WaitForSeconds(2f);

        canCreate = true;
    }
}
