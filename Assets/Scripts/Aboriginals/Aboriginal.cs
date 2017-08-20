using UnityEngine;
using System.Collections;

public class Aboriginal : MonoBehaviour
{
    public Object arrow;
    public float arrowSpeed = 5.0f;
    public float destroyDelay = 1.0f;
    public float shotDelay = 1.5f;

    GameObject shot;

    void Start()
    {
        Invoke("Shot", shotDelay);
    }

    void Shot()
    {
        shot = (GameObject)Instantiate(arrow, transform.position + transform.right, transform.rotation);
        shot.GetComponent<Rigidbody>().velocity = transform.right.normalized * arrowSpeed;
        Destroy(this.gameObject, destroyDelay);
    }
}