using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyNum;
    [SerializeField] Color keyColor;
    [SerializeField] MeshRenderer keyMesh;

    private void Start()
    {
        keyMesh.material.color = keyColor;
    }

}
