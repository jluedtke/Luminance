﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Vector3 distance;

	// Update is called once per frame
	void Update () {
        transform.position = player.position + distance;
        transform.LookAt(player.transform);

    }
}
