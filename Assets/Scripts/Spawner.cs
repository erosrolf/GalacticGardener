using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform[] spawnPoints;
    public float distanceToPlayer;


    void Start()
    {
        InvokeRepeating("Spawn", 3f, 3f);
    }

    public void Spawn()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newObj = Instantiate(prefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, distanceToPlayer), spawnPoint.rotation);

        newObj.GetComponent<Rigidbody>().AddForce(Vector3.back * 5, ForceMode.Impulse);
    }
}