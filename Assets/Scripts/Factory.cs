using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Factory instance { get; private set; }

    public Ship shipPrefab;
    public Bullet bulletPrefab;

    private List<Ship> _hiddenShips;
    private List<Bullet> _hiddenBullets;

    private void Awake()
    {
        instance = this;
        _hiddenBullets = new List<Bullet>();
        _hiddenShips = new List<Ship>();
    }
    
    public Ship GetShip()
    {
        if (_hiddenShips.Count <= 0)
        {
            var ship = Instantiate(shipPrefab);
            return ship;
        }
        else
        {
            var ship = _hiddenShips[0];
            _hiddenShips.Remove(ship);
            ship.gameObject.SetActive(true);
            ship.transform.rotation = Quaternion.identity;
            return ship;
        }
    }

    public Bullet GetBullet()
    {
        if (_hiddenBullets.Count > 0)
        {
            var bullet = _hiddenBullets[0];
            _hiddenBullets.Remove(bullet);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void HideShip(Ship ship)
    {
        ship.gameObject.SetActive(false);
        Model.ships.Remove(ship);
        _hiddenShips.Add(ship);
    }

    public void HideBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _hiddenBullets.Add(bullet);
    }
}