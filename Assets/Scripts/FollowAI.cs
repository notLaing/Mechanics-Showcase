﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform target;
    public Material iceMat;
    public Material poisonMat;
    public Material enemyMat;
    private Renderer thisMat;
    public GameObject jelly;
    public int enemyLevel;
    public bool isDead = false;
    private float speed;
    private Vector3 size = new Vector3();
    public int enemyType = 0;
    public int damage;
    //private Renderer mat;
    public bool hasChained = false;

    // Start is called before the first frame update
    void Start()
    {
        thisMat = GetComponentInChildren<Renderer>();
        if (name == "IceJelly1")
        {
            enemyLevel = 1;
            enemyType = 1;
        }
        else if (name == "IceJelly2")
        {
            enemyLevel = 2;
            enemyType = 1;
        }
        else if (name == "IceJelly3")
        {
            enemyLevel = 3;
            enemyType = 1;
        }
        else if (name == "PoisonJelly1")
        {
            enemyLevel = 1;
            enemyType = 2;
        }
        else if (name == "PoisonJelly2")
        {
            enemyLevel = 2;
            enemyType = 2;
        }
        else if (name == "PoisonJelly3")
        {
            enemyLevel = 3;
            enemyType = 2;
        }
        else
        {
            float levelRandom = Random.Range(0, 1f);
            if (levelRandom <= .6f) enemyLevel = 1;
            if (levelRandom > .6f && levelRandom <= .9f) enemyLevel = 2;
            if (levelRandom > .9f) enemyLevel = 3;

            float typeRandom = Random.Range(0, 1f);
            if (typeRandom <= .24f) enemyType = 1;//<=.49
            else if (typeRandom <= .49f) enemyType = 2;//>.49
            else if (typeRandom <= .74f) enemyType = 3;
            else if (typeRandom > .74f) enemyType = 4;
        }
        

        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        //thisMat = GetComponent<Renderer>();
        switch(enemyLevel)
        {
            case 1:
                speed = 10;
                damage = 10;
                size = new Vector3(1f, 1f, 1f);
                transform.localScale = size;
                break;

            case 2:
                speed = 7;
                damage = 15;
                size = new Vector3(2f, 2f, 2f);
                transform.localScale = size;
                break;

            case 3:
                speed = 3;
                damage = 25;
                size = new Vector3(3f, 3f, 3f);
                transform.localScale = size;
                break;

            default:
                speed = 10;
                damage = 10;
                size = new Vector3(1f, 1f, 1f);
                transform.localScale = size;
                break;
        }
        switch(enemyType)
        {
            case 1:
                thisMat.material = iceMat;
                break;
            case 2:
                thisMat.material = poisonMat;
                break;
            case 3:
                thisMat.material = enemyMat;
                gameObject.AddComponent<EnemyShoot>();
                break;
            default:
                break;

        }
        agent.speed = speed;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDead) Destroy(gameObject);
       
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance >= 10f) agent.SetDestination(target.position);
        FacePlayer();
        if (GetComponent<Hit>()) hasChained = true;
        if (Input.GetKeyDown(KeyCode.P)) print(hasChained);

    }
    private void FacePlayer()
    {
        if (target != null)
        {
            Vector3 lookPoint = new Vector3(target.position.x, 0f, target.position.z);
            transform.LookAt(lookPoint);
        }
    }
}
