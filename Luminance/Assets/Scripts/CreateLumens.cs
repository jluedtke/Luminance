using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLumens : MonoBehaviour {

    public GameObject lumen;

    public float range = 2;


	// Use this for initialization
	void Start () {
		
	}

    public void ObjectDied(int min, int max)
    {
        Vector3 center = transform.position;
        int lumenToSpawn = Random.Range(min, max + 1);

        for (int i = 0; i < lumenToSpawn; i++)
        {
            Vector3 pos = RandomCircle(center, range);
            Quaternion rot = Quaternion.identity;
            Instantiate(lumen, pos + Vector3.up, rot);

        }
    }



    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
