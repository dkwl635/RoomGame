using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject subCamera;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            ChangeCamera();
    }


    void ChangeCamera()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        subCamera.SetActive(!subCamera.activeSelf);
    }



}
