using System;
using Configs;
using UI;
using UnityEngine;

public class Ship : MonoBehaviour, IALive
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Rigidbody _rigidbody;
    
    private Vector3 _moveDirection;
    private Quaternion _rotation;
    private Transform _movePoint => GameConfig.instance.wayPoints[model.pointIndex];
    public ShipModel model { get; private set; }
    public static Action onShipDie;

    public void TakeDamage(int value)
    {
        model.health -= value;
        _healthBar.ChangeHealthBar(model.health, GameConfig.instance.maxShipHealth);
        if (model.health <= 0) OnShipDie();
    }

    private void OnShipDie()
    {
        model.isLive = false;
        model.currentSpeed = 2.5f;
        _rigidbody.useGravity = true;
    }

    private void HideShip()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        Factory.instance.HideShip(this);
        onShipDie?.Invoke();
    }

    public void Init()
    {
        model = new ShipModel
        {
            currentSpeed = GameConfig.instance.maxShipSpeed,
            health = GameConfig.instance.maxShipHealth,
            isLive = true
        };
        _healthBar.ChangeHealthBar(model.health, GameConfig.instance.maxShipHealth);
        model.pointIndex = FindClosestPointIndex();
        _moveDirection = Vector3.Normalize(_movePoint.position - transform.position);
        transform.forward = _moveDirection;
    }

    private int FindClosestPointIndex()
    {
        var distanceToPoint = float.MaxValue;
        var closestPointIndex = -1;
        for (var i = 0; i < GameConfig.instance.wayPoints.Count; i++)
        {
            var currentDistance = Vector3.Distance(transform.position, GameConfig.instance.wayPoints[i].transform.position);
            if (currentDistance < distanceToPoint)
            {
                distanceToPoint = currentDistance;
                closestPointIndex = i;
            }
        }
        return closestPointIndex;
    }

    private void GoNextMovePoint()
    {
        model.pointIndex++;
        if (model.pointIndex >= GameConfig.instance.wayPoints.Count)
        {
            model.pointIndex = 0;
        }
    }

    private void OnReachPoint()
    {
        GoNextMovePoint();
        _moveDirection = Vector3.Normalize(_movePoint.position - transform.position);
        _rotation = Quaternion.LookRotation(_moveDirection);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject.GetComponent<Ship>();
        if (target != null)
        {
            TakeDamage(GameConfig.instance.maxShipHealth);
        }
    }

    private void Update()
    {
        if (model.isLive)
        {
            var distance = Vector3.Distance(transform.position, _movePoint.position);
            if (distance <= 0.5f) OnReachPoint();
            _rigidbody.velocity = _moveDirection * model.currentSpeed;
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 
                GameConfig.instance.shipRotationSpeed * Time.deltaTime);
        }
        else
        {
            if(!model.isLive && transform.position.y <= GameConfig.instance.despawnDepth) HideShip();
        }
    }
}