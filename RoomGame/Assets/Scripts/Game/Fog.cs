using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] GameObject[] fogs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            for (int i = 0; i < fogs.Length; i++)
                fogs[i].SetActive(false);
        
    }
}
