using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float _z_distanceToPlayer;
    private float distanceBetweenSpawnObjects = 3.48f;
    private Vector3[] _spawnPoints;

    private Vector3[] InitSpawnPoints()
    {
        var smallHex = HexagonPoints.CalculatePoints(distanceBetweenSpawnObjects);
        var bigHex = HexagonPoints.CalculatePoints(distanceBetweenSpawnObjects * 2);
        return smallHex.Concat(bigHex).ToArray();
    }

    void Start()
    {
        _spawnPoints = InitSpawnPoints();
        InvokeRepeating("Spawn", 3f, 3f);
    }

    public void Spawn()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject newObj = Instantiate(prefab, new Vector3(spawnPoint.x, spawnPoint.y, _z_distanceToPlayer), Quaternion.identity);
        newObj.GetComponent<Rigidbody>().AddForce(Vector3.back * 5, ForceMode.Impulse);
    }
}