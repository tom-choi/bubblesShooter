using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerIconUI : MonoBehaviour
{
    public Sprite NormalFaceIcon;
    public Sprite ReadyIcon;

    public IEnumerator IamReadyyyyy()
    {
        this.gameObject.GetComponent<Image>().sprite = ReadyIcon;
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<Image>().sprite = NormalFaceIcon;
    }
}