using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPosStart, xPosFinish, zPosStart, zPosFinish, howManyEnemiesMin, howManyEnemiesMax;
    int xPos;
    int zPos;
    int enemyCount;
    int howManyEnemies;

    void Start()
    {
        howManyEnemies = Random.Range(howManyEnemiesMin, howManyEnemiesMax);
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < howManyEnemies)
        {
            xPos = Random.Range(xPosStart, xPosFinish);
            zPos = Random.Range(zPosStart, zPosFinish);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.Euler(0, Random.Range(0, 360), 0));           
            yield return new WaitForSeconds(0.001f);
            enemyCount += 1;
        }
    }

}
