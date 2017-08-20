using UnityEngine;
using System.Collections;

public class AboriginalSpawner: MonoBehaviour
{
    // Maximal and minimal delay between spawns.
    public float MinimalDelaySpawns = 1.0f;
    public float MaximalDelaySpawns = 2.0f;

    // Choosing the maximum and minimum range of the sides for the cannon to appear.
    public float minimumXSide = 0.0f;
    public float maximumXSide = 0.0f;
    public float minimumZSide = 0.0f;
    public float maximumZSide = 0.0f;
    public Object Aboriginal;

    // Use this for initialization
	void Start () {
	    StartCoroutine(StartSpawning());
        minimumXSide = -Random.Range(0.0f, 1.0f);
        maximumXSide = Random.Range(0.0f, 21.0f);
        minimumZSide = -Random.Range(0.0f, 1.0f);
        maximumZSide = Random.Range(0.0f, 13.0f);
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            // Delay between spawns
            yield return new WaitForSeconds(Random.Range(MinimalDelaySpawns, MaximalDelaySpawns));

            int side = Random.Range(0, 4);

            switch (side)
            {
                // side 0 is 12 o'clock side of map (shoots arrows to x positive - 90 degrees)
                case 0:
                    GameObject result0 = (GameObject)Instantiate(Aboriginal, new Vector3(Random.Range(minimumXSide, maximumXSide), 0.3f, 13), Quaternion.Euler(0, 90, 0));
                    break;
                // side 1 is 3 o'clock side of map (shoots arrows to x positive - 180 degrees)
                case 1:
                    GameObject result1 = (GameObject)Instantiate(Aboriginal, new Vector3(21, 0.3f, Random.Range(minimumZSide, maximumZSide)), Quaternion.Euler(0, 180, 0));
                    break;
                // side 2 is 6 o'clock side of map¸(shoots arrows to x positive - 270 degrees)
                case 2:
                    GameObject result2 = (GameObject)Instantiate(Aboriginal, new Vector3(Random.Range(minimumXSide, maximumXSide), 0.3f, -1), Quaternion.Euler(0, 270, 0));
                    break;
                // side 3 (default) is 9 o'clock side of map (shoots arrows to x positive - 0 degrees)
                default:
                    GameObject result3 = (GameObject)Instantiate(Aboriginal, new Vector3(-1, 0.3f, Random.Range(minimumZSide, maximumZSide)), Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
    }
}
