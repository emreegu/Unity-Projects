using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] monsterReference;

    private GameObject spawnMonster;
    
    [SerializeField] private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            spawnMonster = Instantiate(monsterReference[randomIndex]);
        
        
            if (randomIndex == 0) 
            {
                //left
                spawnMonster.transform.position = leftPos.position;
                spawnMonster.GetComponent<Monster>().speed = Random.Range(4,10);
            }
            else
            {
                //rigth side
                spawnMonster.transform.position = rightPos.position;
                spawnMonster.GetComponent<Monster>().speed = -Random.Range(4,10);
                spawnMonster.transform.localScale = new Vector3(-1f, 1f, 1f);

            }
        } // while loop
    }
    
} //class
