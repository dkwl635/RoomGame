using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    PlayerMovement player;
    WaypointPatrol[] waypointPatrols;

    [Header("ItemData")]
    [SerializeField] private ItemData[] itemDatas;
    public Dictionary<int, ItemData> ItemDates = new Dictionary<int, ItemData>();

    [Header("Material")]
    [SerializeField] Material[] outlineMaterials;
    public Dictionary<string, Material> OutlineMaterials = new Dictionary<string, Material>();


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

        for (int i = 0; i < outlineMaterials.Length; i++)
        {
            if (OutlineMaterials.ContainsKey(outlineMaterials[i].name))
                Debug.Log("�ߺ��Ǵ� ���̵� �ֽ��ϴ� : " + outlineMaterials[i].name);

            OutlineMaterials.Add(outlineMaterials[i].name, outlineMaterials[i]);
        }

        player = GameObject.FindObjectOfType<PlayerMovement>();
        waypointPatrols = GameObject.FindObjectsOfType<WaypointPatrol>();
    }

    private void Start()
    {
      
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
