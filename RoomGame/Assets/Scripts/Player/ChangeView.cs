using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{ 
    Camera mainCamera;
    public GameObject thirdViewCamera;
    public GameObject firstViewCamera;

    [SerializeField] bool cameraView = false;
    LayerMask playerLayer;

    PlayerMovement player;


    private void Awake()
    {
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        mainCamera = Camera.main;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            ChangeCamera();
    }


    void ChangeCamera()
    {
        player.ChangeMoveMode(); 

        if (!cameraView)
            mainCamera.cullingMask = mainCamera.cullingMask & ~playerLayer;
        else
            mainCamera.cullingMask = -1;


        thirdViewCamera.SetActive(cameraView);
        firstViewCamera.SetActive(!cameraView);

        cameraView = !cameraView;
    }

}



