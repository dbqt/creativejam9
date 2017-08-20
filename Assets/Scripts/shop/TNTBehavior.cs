using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTBehavior : MonoBehaviour {

    public Animator Anim;
    public GameObject TNTModel;

    public void SetTNT(float delay) {
        Invoke("Explode", delay);
    }

    void Explode() {
        //play explosion animation
        Anim.SetTrigger("Explode");
        TNTModel.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }
}
