using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Factory instance { get; private set; }


    [SerializeField] private Ship _shipPrefab;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private GameObject _shootFXPrefab;
    
    private List<Ship> _hiddenShips;
    private List<Bullet> _hiddenBullets;
    private List<GameObject> _hiddenShootFx;

    private void Awake()
    {
        instance = this;
        _hiddenBullets = new List<Bullet>();
        _hiddenShips = new List<Ship>();
        _hiddenShootFx = new List<GameObject>();
    }
    
    public Ship GetShip()
    {
        if (_hiddenShips.Count <= 0)
        {
            var ship = Instantiate(_shipPrefab);
            return ship;
        }
        else
        {
            var ship = _hiddenShips[0];
            _hiddenShips.Remove(ship);
            Model.ships.Add(ship);
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

            var bullet = Instantiate(_bulletPrefab);
            return bullet;
        }
    }


    public GameObject GetShootFX()
    {
        if (_hiddenShootFx.Count > 0)
        {
            var fx = _hiddenShootFx[0];
            _hiddenShootFx.Remove(fx);
            fx.SetActive(true);
            return fx;
        }
        else
        {
            return Instantiate(_shootFXPrefab);
        }
    }

    public void HideShip(Ship ship)
    {
        Model.ships.Remove(ship);
        ship.gameObject.SetActive(false);
        _hiddenShips.Add(ship);
    }

    public void HideBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _hiddenBullets.Add(bullet);
    }

    public void HideShootFX(GameObject fx)
    {
        _hiddenShootFx.Add(fx);
        fx.SetActive(false);
    }
}