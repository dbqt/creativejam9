using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTBehavior : MonoBehaviour {

    public Animator Anim;

    public void SetTNT(float delay) {
        Invoke("Explode", delay);
    }

    void Explode() {
        //play explosion animation
        Anim.SetTrigger("Explode");
        Destroy(this.gameObject, 0.5f);
    }
}
