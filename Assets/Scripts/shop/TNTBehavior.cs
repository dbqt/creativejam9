using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTBehavior : MonoBehaviour {

    public void SetTNT(float delay) {
        Invoke("Explode", delay);
    }

    void Explode() {
        //play explosion animation

        Destroy(this.gameObject, 0.5f);
    }
}
