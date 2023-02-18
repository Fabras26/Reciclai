using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizarCor : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Image>().color = Random.ColorHSV();
    }
    void Update()
    {
        
    }
}
