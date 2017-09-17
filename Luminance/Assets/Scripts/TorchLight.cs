using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("TorchFlickerOffset", Random.Range(1f, 20f));
        anim.speed = Random.Range(.8f, 1f);
    }
}
