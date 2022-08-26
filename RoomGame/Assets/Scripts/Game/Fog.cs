using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] GameObject[] fogs;

    public void OffFogs()
    {
        for (int i = 0; i < fogs.Length; i++)
            fogs[i].SetActive(false);
    }
    
}
