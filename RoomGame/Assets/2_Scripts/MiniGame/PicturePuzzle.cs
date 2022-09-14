using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicturePuzzle : MonoBehaviour
{
    [SerializeField]Fog fog;

    //0 6Spades , 1 6Heart , 2 9Diamond , 3 3Club
    public Picture[] pictures;

    public void CheckPuzzle()
    {
        if (pictures[0].num == 6 && pictures[1].num == 6 && pictures[2].num == 9 && pictures[3].num == 3)
        {
            fog.OffFogs();
            gameObject.SetActive(false);
        }
    }



}
