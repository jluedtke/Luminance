using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHandler : MonoBehaviour {

    public LayerMask environmentLayer;
    public Material opaqueMat;
    // Update is called once per frame
    void Update ()
    {

        MeshRenderer objectMesh;

        RaycastHit hit;


        if (Physics.Linecast(transform.position, transform.position + (Vector3.forward * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }
        if (Physics.Linecast(transform.position, transform.position + ((Vector3.forward + Vector3.right) * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }
        if (Physics.Linecast(transform.position, transform.position + ((Vector3.forward + Vector3.left) * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }
        if (Physics.Linecast(transform.position, transform.position + (new Vector3(.5f, 0, 1) * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }
        if (Physics.Linecast(transform.position, transform.position + (new Vector3(-.5f, 0, 1) * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }
        if (Physics.Linecast(transform.position, transform.position + (new Vector3(.3f, 0, 1) * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }
        if (Physics.Linecast(transform.position, transform.position + (new Vector3(-.3f, 0, 1) * 8), out hit, environmentLayer))
        {
            objectMesh = hit.collider.GetComponent<MeshRenderer>();
            objectMesh.material = opaqueMat;
        }


    }
}

//Physics.Linecast(transform.position, transform.position + (Vector3.forward* 5), out hit, environmentLayer)