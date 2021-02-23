using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


public class Targeting : MonoBehaviour
{
    //Attack enemies based on targets in area
    public void AttackEnemies(bool attackAll, float radius, float distance, float damage)
    {

        if (attackAll)
        {
            var listOfEnemies = GetListOfEnemiesInRange(radius, distance);
            DealDamageToEnemyHealth(listOfEnemies, damage);
        }
        else
        {
            var closestEnemyInList = GetClosestEnemy(radius, distance);
            DealDamageToEnemyHealth(closestEnemyInList, damage);
        }
    }

    //Deal damage on EnemyHealth
    void DealDamageToEnemyHealth(List<GameObject> enemyList, float damage)
    {
        foreach (var item in enemyList)
        {
            var enemyHealth = item.GetComponent<EnemyHealth>();
            enemyHealth.DealDamage(damage);
            Debug.Log("enemy health: " + enemyHealth.CurrentHealth);
        }
    }

    //Return an array of raycast hits in capsule area 
    public RaycastHit[] GetArrayOfCapsulecastHits(float radius, float distance)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = transform.up * 20f;
        Vector3 direction = transform.forward;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        return Physics.CapsuleCastAll(p1, p2, radius, direction, distance, layerMask, QueryTriggerInteraction.Collide);
    }

    //Return a list of enemies in area
    public List<GameObject> GetListOfEnemiesInRange(float radius, float distance)
    {
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (var item in GetArrayOfCapsulecastHits(radius, distance))
        {
            GameObject go = item.transform.gameObject;
            if (go.CompareTag("Enemy"))
                enemiesInRange.Add(go);
        }

        return enemiesInRange;
    }

    //Return a closest enemy in a list
    public List<GameObject> GetClosestEnemy(float radius, float distance)
    {
        List<GameObject> gameobjectList = GetListOfEnemiesInRange(radius, distance);

        if (gameobjectList.Count < 2)
            return gameobjectList;

        do
        {
            if (CheckIfCloser(gameobjectList[0], gameobjectList[1]))
                gameobjectList.RemoveAt(1);
            else
                gameobjectList.RemoveAt(0);

        } while (gameobjectList.Count > 1);

        return gameobjectList;
    }

    //Return a list of rigidbodies in area
    public List<Rigidbody> ListOfRigidBodiesInRange(float radius, float distance)
    {
        List<Rigidbody> rbList = new List<Rigidbody>();
        var capsuleCastHits = GetArrayOfCapsulecastHits(radius, distance);
        foreach (var item in capsuleCastHits)
        {
            if (item.transform.gameObject.tag != "Player" && item.transform.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rbList.Add(rb);
            }
        }

        return rbList;
    }

    bool CheckIfCloser(GameObject gameObject1, GameObject gameObject2)
    {
        return Vector3.Distance(transform.position, gameObject1.transform.position) < Vector3.Distance(transform.position, gameObject2.transform.position);
    }
}