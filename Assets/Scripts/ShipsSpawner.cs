using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _gunTransform;
    private List<Transform> _correctPos;

    public void SpawnShip()
    {
        var ship = Factory.instance.GetShip();
        ship.transform.position = FindAvailableSpawnPoint().position + new Vector3(
            Random.insideUnitCircle.x * 100.0f, 
            0.0f, 
            Random.insideUnitCircle.y * 100.0f);
        Model.ships.Add(ship);
        ship.Init();
        _correctPos.Clear();
    }

    private Transform FindAvailableSpawnPoint()
    {
        foreach (var point in _spawnPoints)
        {
            var toSpawnPoint = point.transform.position - _gunTransform.position;
            var angel = Vector3.Dot(toSpawnPoint.normalized, _gunTransform.forward);
            if (angel < 0) _correctPos.Add(point);
        }
        return _correctPos[Random.Range(0, _correctPos.Count)];
    }

    private void Start()
    {
        _correctPos = new List<Transform>();
    }
}