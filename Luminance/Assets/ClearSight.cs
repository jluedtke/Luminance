using UnityEngine;
using System.Collections.Generic;


public class ClearSight : MonoBehaviour
{
    public float distanceToPlayer = 5.0f;
    public LayerMask environmentLayer;
    public Transform target;

    [Header("Materials")]
    public Material transparentMat;
    public Material opaqueMat;

    private float fireDelta = 2F; //fire rate

    private float nextFire = 2F; //after first shot
    private float myTime = 0.0F;

    private List<GameObject> transparentWalls = new List<GameObject>();
    private GameObject currentWall;


    void Update()
    {
        myTime = myTime + Time.deltaTime;
        RaycastHit hit;
        MeshRenderer objectMesh;
        if (Physics.Linecast(transform.position, target.position, out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();

            if (!transparentWalls.Contains(hit.collider.gameObject))
                transparentWalls.Add(hit.collider.gameObject);

            currentWall = hit.collider.gameObject; 
            objectMesh.material = transparentMat;
            Color objectColor = objectMesh.material.color;
            objectColor.a = 0.8f;
            objectMesh.material.color = objectColor;
        }

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;

            ChangeToOpaque();

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }

    }

    private void ChangeToOpaque()
    {
        if (transparentWalls.Count <= 0)
            return;



        for (int i = 0; i < transparentWalls.Count; i++)
        {
            if (transparentWalls[i] == currentWall)
                continue;

            transparentWalls[i].GetComponent<MeshRenderer>().material = opaqueMat;
        }
        transparentWalls = new List<GameObject>();


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position);
    }

}
