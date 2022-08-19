using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{ 
    Camera mainCamera;
    [SerializeField] GameObject thirdViewCamera;
    [SerializeField] GameObject firstViewCamera;
    [SerializeField] GameObject houesRoot;
    [SerializeField] bool cameraView = false;
    LayerMask playerLayer;

    PlayerMovement player;

    [SerializeField] bool isChange = true; 


    private void Awake()
    {
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        mainCamera = Camera.main;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        isChange = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && isChange)
            ChangeCamera();
    }


    void ChangeCamera()
    {
        isChange = false;
        player.ChangeMoveMode(); 

        if (!cameraView)
            mainCamera.cullingMask = mainCamera.cullingMask & ~playerLayer;
        else
            mainCamera.cullingMask = -1;
        
        thirdViewCamera.SetActive(cameraView);
        firstViewCamera.SetActive(!cameraView);

        StartCoroutine(HouesRootOnOff(!cameraView));

        cameraView = !cameraView;
    }

    IEnumerator HouesRootOnOff(bool On)
    {
        if(On)
        {
            yield return new WaitForSeconds(1.5f);
            houesRoot.SetActive(true);
        }
        else 
        {
            houesRoot.SetActive(false);
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(1.0f);
        isChange = true;
    }

}



