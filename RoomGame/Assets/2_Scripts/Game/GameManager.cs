using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    PlayerMovement player;
    WaypointPatrol[] waypointPatrols;
    public Vector3 PlayerPos { get { return player.transform.position; } }

    [Header("ItemData")]
    [SerializeField] private ItemData[] itemDatas;
    public Dictionary<int, ItemData> ItemDates = new Dictionary<int, ItemData>();



    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < itemDatas.Length; i++)
        {
            if (ItemDates.ContainsKey(itemDatas[i].item_Id))
                Debug.Log("�ߺ��Ǵ� ���̵� �ֽ��ϴ� : " + itemDatas[i].item_Name);

            ItemDates.Add(itemDatas[i].item_Id, itemDatas[i]);
        } 

        player = GameObject.FindObjectOfType<PlayerMovement>();
        waypointPatrols = GameObject.FindObjectsOfType<WaypointPatrol>();
    }

  

    public void EnemyMove(bool IsMove)//�����̴� ��� ���� ����/�̵�
    {
        for (int i = 0; i < waypointPatrols.Length; i++)
            waypointPatrols[i].MoveOnOff(IsMove);
    }

    public void PlayerMove(bool IsMove)
    {
        player.IsMove = IsMove;
    }
}
