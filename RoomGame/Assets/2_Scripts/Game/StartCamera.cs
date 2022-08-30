using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{ 
    float timer = 0.0f;
    CinemachineVirtualCamera Cinemachine;
    private void Awake()
    {
        Cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Cinemachine.m_Lens.FieldOfView = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime * 0.9f;
        Cinemachine.m_Lens.FieldOfView = Mathf.Lerp(0, 60, timer);
        if(timer > 1.0f)
        {
            Cinemachine.m_Lens.FieldOfView = 60.0f;
            enabled = false;
        }

    }
}
