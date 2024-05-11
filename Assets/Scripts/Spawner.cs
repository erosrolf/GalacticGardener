using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Architecture;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject planetPrefab;
    public float _z_distanceToPlayer;
    public GameObject pointPrefab;
    public float minSpeed;
    public float maxSpeed;

    public GameObject SpawnParent;

    private Vector3[] _innerSpawnZone;
    private Vector3[] _middleSpawnZone;

    void OnEnable()
    {
        GameManager.StartGameEvent += StartSpawn;
        GameManager.PlanetEvent += SpawnPlanet;
        JetpackController.NewZonePositionEvent += ZonesOnPlayerPosition;
    }

    void OnDisable()
    {
        GameManager.StartGameEvent -= StartSpawn;
        GameManager.PlanetEvent -= SpawnPlanet;
        JetpackController.NewZonePositionEvent -= ZonesOnPlayerPosition;
    }

    public void SpawnPlanet()
    {
        var position = transform.position;
        position.z = _z_distanceToPlayer;
        GameObject newObj = Instantiate(planetPrefab, position, Quaternion.identity);
        newObj.GetComponent<Rigidbody>().AddForce(Vector3.back * 3, ForceMode.Impulse);
    }

    public void ZonesOnPlayerPosition(Vector3 target)
    {
        for (int i = 0; i < _innerSpawnZone.Length; ++i)
        {
            _innerSpawnZone[i].x += target.x;
            _innerSpawnZone[i].y += target.y;
        }
        for (int i = 0; i < _middleSpawnZone.Length; ++i)
        {
            _middleSpawnZone[i].x += target.x;
            _middleSpawnZone[i].y += target.y;
        }
    }

    public void VisualizePoints(Vector3[] points)
    {
        foreach (Vector3 point in points)
        {
            Instantiate(pointPrefab, new Vector3(point.x, point.y, _z_distanceToPlayer), Quaternion.identity);
        }
    }

    private void Awake()
    {
        _innerSpawnZone = HexagonPoints.CalculatePoints(GameSettings.Instance.DistanceBetweenObjects);
        _innerSpawnZone.Append(Vector3.zero);
        var tmp = HexagonPoints.CalculatePoints(GameSettings.Instance.DistanceBetweenObjects * 2);
        _middleSpawnZone = InsertMiddlePoints(tmp);
    }

    public void StartSpawn()
    {
        Debug.Log("START SPAWN");
        StartCoroutine(SpawnOnInnerZoneRepeating(2f, 4f));
        StartCoroutine(SpawnOnMiddleZoneRepeating(4f, 4f));
    }

    IEnumerator SpawnOnInnerZoneRepeating(float timeToStart, float timeToRepeat)
    {
        yield return new WaitForSeconds(timeToStart);
        while (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            StartCoroutine(SpawnOnInnerZone(timeToRepeat));
            yield return new WaitForSeconds(timeToRepeat);
        }
    }

    IEnumerator SpawnOnMiddleZoneRepeating(float timeToStart, float timeToRepeat)
    {
        yield return new WaitForSeconds(timeToStart);
        while (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            StartCoroutine(SpawnOnMiddleZone(timeToRepeat));
            yield return new WaitForSeconds(timeToRepeat);
        }
    }

    public IEnumerator SpawnOnInnerZone(float timeToRepeat)
    {
        int count = GameSettings.Instance.SpawnCountOnInnerZone;
        var indexes = GetUniqueRandomNumbers(_innerSpawnZone.Length, count);
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Vector3 spawnPosition = _innerSpawnZone[indexes[i]];
            spawnPosition.z = _z_distanceToPlayer;
            GameObject newObj = Instantiate(prefab, spawnPosition, Quaternion.identity);
            newObj.transform.parent = SpawnParent.transform;
            float forceFactor = Random.Range(minSpeed, maxSpeed);
            newObj.GetComponent<Rigidbody>().AddForce(Vector3.back * forceFactor, ForceMode.Impulse);
            yield return new WaitForSeconds(timeToRepeat / count);
        }
    }

    public IEnumerator SpawnOnMiddleZone(float timeToRepeat)
    {
        int count = GameSettings.Instance.SpawnCountOnMiddleZone;
        var indexes = GetUniqueRandomNumbers(_middleSpawnZone.Length, count);
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Vector3 spawnPosition = _middleSpawnZone[indexes[i]];
            spawnPosition.z = _z_distanceToPlayer;
            GameObject newObj = Instantiate(prefab, spawnPosition, Quaternion.identity);
            newObj.transform.parent = SpawnParent.transform;
            float forceFactor = Random.Range(minSpeed, maxSpeed);
            newObj.GetComponent<Rigidbody>().AddForce(Vector3.back * forceFactor, ForceMode.Impulse);
            yield return new WaitForSeconds(timeToRepeat / count);
        }
    }

    private Vector3 GetMiddlePoint(Vector3 pointA, Vector3 pointB)
    {
        return Vector3.Lerp(pointA, pointB, 0.5f);
    }

    private Vector3[] InsertMiddlePoints(Vector3[] points)
    {
        List<Vector3> newPoints = new List<Vector3>();

        for (int i = 0; i < points.Length - 1; i++)
        {
            newPoints.Add(points[i]);
            newPoints.Add(GetMiddlePoint(points[i], points[i + 1]));
        }
        newPoints.Add(GetMiddlePoint(points[0], points[points.Length - 1]));
        newPoints.Add(points[points.Length - 1]);

        return newPoints.ToArray();
    }

    private int[] GetUniqueRandomNumbers(int max, int count)
    {
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < count)
        {
            numbers.Add(Random.Range(0, max));
        }
        return new List<int>(numbers).ToArray();
    }
}