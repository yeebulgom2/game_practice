/*
 using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class HpAppear : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    float i = 0.1f;
    [SerializeField] GameObject HpPrefab;
    [SerializeField] GameObject PlayerPrefab;
    PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.AddComponent<PlayerMovement>();
        int MaxHp = playerMovement.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement = gameObject.AddComponent<PlayerMovement>();
        int MaxHp = playerMovement.MaxHp;
        for(int j = 1 ;j <= MaxHp ; j++)
        {
            offset = new Vector3(offset.x, offset.y + i, offset.z);
            Vector3 HpPosition = player.position + offset;
            transform.position = HpPosition;
        }
    }
}
*/