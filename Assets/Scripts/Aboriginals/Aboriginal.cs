using UnityEngine;
using System.Collections;

public class Aboriginal : MonoBehaviour
{
    public Object arrow;
    public float arrowSpeed = 120.0f;
    public float destroyDelay = 1.0f;


    void Start()
    {
        GameObject shot = (GameObject)Instantiate(arrow, transform.position + transform.right * 50, transform.rotation);
        shot.GetComponent<Rigidbody>().velocity = transform.right.normalized * arrowSpeed;
        Destroy(this.gameObject, destroyDelay);
    }
}