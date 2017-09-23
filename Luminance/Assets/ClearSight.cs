using UnityEngine;
using System.Collections;


public class ClearSight : MonoBehaviour
{
    public float distanceToPlayer = 5.0f;
    public LayerMask environmentLayer;
    public Transform target;

    private Ray ray;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(target.position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, distanceToPlayer, environmentLayer);
        foreach (RaycastHit hit in hits)
        {
            print("Found 1");
            MeshRenderer rend = hit.collider.GetComponent<MeshRenderer>();
            if (rend == null)
                continue; // no renderer attached? go to next hit
                          // TODO: maybe implement here a check for GOs that should not be affected like the player


            if (rend)
            {
                // Change the material of all hit colliders
                // to use a transparent shader.
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray);
    }

}
