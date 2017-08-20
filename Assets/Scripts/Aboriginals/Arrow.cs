using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();

        if (pm != null)
            pm.stun(2);

        Destroy(this.gameObject);
    }
}