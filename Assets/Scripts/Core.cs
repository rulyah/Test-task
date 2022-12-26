using System.Collections.Generic;
using Configs;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ShipsSpawner _shipsSpawner;
    private void Start()
    {
        Model.ships = new List<Ship>();
        InitShipsOnScene();
        Ship.onShipDie += CheckShipsCount;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void InitShipsOnScene()
    {
        var ships = FindObjectsOfType<Ship>();
        for (var i = 0; i < ships.Length; i++)
        {
            Model.ships.Add(ships[i]);
            ships[i].Init();
        }
    }

    private void CheckShipsCount()
    {
        if (Model.ships.Count < GameConfig.instance.maxShipsCount)
        {
            for (var i = Model.ships.Count; i < GameConfig.instance.maxShipsCount; i++)
            {
                _shipsSpawner.SpawnShip();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_gun.isCanShoot)
            {
                _gun.Shoot(Factory.instance.GetBullet());
            }
        }
    }
}
