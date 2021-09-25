using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour
{
    // tutorial base: https://www.youtube.com/watch?v=RXB7wKSoupI 

    //stats that worked: at 100 scale
    /* speed: .9
     * angular drag: 90
     * accell 8
     * stoppin .2
     * radius .0005
     * height 1e-05
     
     */

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    //[SerializeField] float timerForNewPath;
    [SerializeField] float minTimerForNewPath;
    [SerializeField] float maxTimerForNewPath;
    bool inCoRoutine;
    bool validPath;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        //testing to start cows on spawn
        inCoRoutine = true;
        //yield return new WaitForSeconds(timerForNewPath);
        //yield return new WaitForSeconds(Random.Range(minTimerForNewPath, maxTimerForNewPath));
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);

        //if (!validPath) Debug.Log("Found an invalid path");

        /*while (!validPath)
        {
            //yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }*/
        inCoRoutine = false;
    }

    Vector3 getNewRandomPosition()
    {
        //float x = Random.Range((float)-.001, (float).001);
        //float z = Random.Range((float)-.001, (float).001);

        // this worked well before i went from 100 scale to 1000
        //float x = Random.Range((float)-.9, (float).9);
        //float z = Random.Range((float)-.9, (float).9);

        // original
        float x = Random.Range(-100f, 100);
        float z = Random.Range(-100f, 100);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        //yield return new WaitForSeconds(timerForNewPath);
        yield return new WaitForSeconds(Random.Range(minTimerForNewPath, maxTimerForNewPath));
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);

        if (!validPath) Debug.Log("Found an invalid path");

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    public void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
    // Update is called once per frame
    void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(DoSomething());
        }
    }
}
