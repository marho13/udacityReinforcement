using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {
    public GameObject checkpointList;
    public GameObject car;
    public List<GameObject> Listy;
    public int checkpointInd = 0;
    public int straightCheckpoints = 0;
    public int nonStraightCheckpoints = 0;
    Vector3 offSet;
    Vector3 resetVector;
    public List<Vector3> positionList;
    // Use this for initialization
    void Start()
    {
        Vector3 offSet = new Vector3(0, 1, 0);
        Listy = checkpointList.GetAllChilds();
        resetVector = Listy[0].transform.position + offSet;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float distance = Vector3.Distance(Listy[checkpointInd].transform.position, car.transform.position);
        if (distance <= 5.0)
        {
            if (Listy[checkpointInd].name.StartsWith("road_straight"))
            {
                straightCheckpoints++;
            }

            else {
                nonStraightCheckpoints++;
            }
            checkpointInd++;
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
        car.transform.rotation = Listy[0].transform.rotation;
        car.transform.position = resetVector;
        straightCheckpoints = 0;
        nonStraightCheckpoints = 0;
        checkpointInd = 0;
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
