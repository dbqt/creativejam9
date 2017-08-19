using UnityEngine;
using System.Collections;

public class AboriginalSpawner: MonoBehaviour
{
    // Maximal and minimal delay between spawns.
    public float MinimalDelay = 2.0f;
    public float MaximalDelay = 5.0f;

    // Choosing the maximum and minimum range of the sides for the cannon to appear.
    public float minimum = -Random.Range(0.0f, 100f);
    public float maximum = Random.Range(0.0f, 100f);
    public Object Aboriginal;

    // Use this for initialization
	void Start () {
	    StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            // Delay between spawns
            yield return new WaitForSeconds(Random.Range(MinimalDelay, MaximalDelay));

            int side = Random.Range(0, 4);

            switch (side)
            {
                // side 0 is 12 o'clock side of map (shoots arrows to x positive)
                case 0:
                    GameObject result0 = (GameObject)Instantiate(Aboriginal, new Vector3(Random.Range(minimum, maximum), 40, 250), Quaternion.Euler(0, 90, 0));
                    break;
                // side 1 is 3 o'clock side of map (shoots arrows to x positive - 180 degrees)
                case 1:
                    GameObject result1 = (GameObject)Instantiate(Aboriginal, new Vector3(250, 40, Random.Range(minimum, maximum)), Quaternion.Euler(0, 180, 0));
                    break;
                // side 2 is 6 o'clock side of map¸(shoots arrows to x positive - 270 degrees)
                case 2:
                    GameObject result2 = (GameObject)Instantiate(Aboriginal, new Vector3(Random.Range(minimum, maximum), 40, -250), Quaternion.Euler(0, 270, 0));
                    break;
                // side 3 (default) is 9 o'clock side of map (shoots arrows to x positive - 0 degrees)
                default:
                    GameObject result3 = (GameObject)Instantiate(Aboriginal, new Vector3(-250, 40, Random.Range(minimum, maximum)), Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
    }
}
