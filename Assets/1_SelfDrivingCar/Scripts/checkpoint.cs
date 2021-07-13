using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour 
{
    public GameObject checkpointList;
    public GameObject Tasks;
    public GameObject car;

    public Rigidbody carRigid;

    public List<GameObject> Listy;
    public List<GameObject> task;

    private int taskInd = 0;
    private int checkpointInd = 0;
    public int straightCheckpoints = 0;
    public int nonStraightCheckpoints = 0;

    Vector3 resetVector;
    Vector3 randomVector;

    public CommandServer c;
    public List<Vector3> positionList;
    // Use this for initialization
    void Awake()
    {
        Listy = checkpointList.GetAllChilds();
        task = Tasks.GetAllChilds();
        resetVector = new Vector3(166.5f, -79.6200027f, -43.9099998f);
        randomVector = new Vector3(0, 0, 0);
        Debug.Log(Listy.Count);
    }

    // Update is called once per frame
    public void checkpointComplete () {
        float distance = Vector3.Distance(Listy[checkpointInd].transform.position, car.transform.position);
        if (distance <= 5.0f)
        {
            Debug.Log("Crossed a new checkpoint");
            if (Listy[checkpointInd].name.Contains("road_straight"))
            {
                straightCheckpoints++;
                c.rewardStraight();
                setCheckpointInd();
            }

            else {
                c.rewardCheckpoint();
                nonStraightCheckpoints++;
                setCheckpointInd();
            }
        }
	}

    public void setCheckpointInd()
    {
        if (checkpointInd >= Listy.Count)
        {
            checkpointInd = 0;
        }
        else
        {
            checkpointInd = checkpointInd + 1;
        }
    }
    public bool stuckCheck() {
        if (positionList.Count > 300) {
            float stuckDist = Vector3.Distance(positionList[0], car.transform.position);
            
            if (stuckDist < 5)
            {
                positionList.Clear();
                return true;
            }

            else {
                positionList.RemoveAt(0);
                positionList.Add(car.transform.position);
                return false;
            }
        }
        positionList.Add(car.transform.position);
        return false;
    }

    public void reset()
    {
        car.transform.rotation = Quaternion.Euler(0, 90, 0);
        randomVector = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        car.transform.position = resetVector + randomVector;
        carRigid.velocity = Vector3.zero;
        carRigid.angularVelocity = Vector3.zero;
        positionList.Clear();
        straightCheckpoints = 0;
        nonStraightCheckpoints = 0;
        checkpointInd = 0;
    }

    public void increaseTask() 
    {
        if (taskInd >= task.Count)
        {
            taskInd = 0;
        }
        else 
        {
            taskInd = taskInd + 1;
        }
    }

    public bool taskComplete()
    {
        float taskDist = Vector3.Distance(task[taskInd].transform.position, car.transform.position);
        if (taskDist <= 5.0f)
        {
            return true;
        }
        return false;
    }
}

public static class ClassExtension
{
    public static List<GameObject> GetAllChilds(this GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}
