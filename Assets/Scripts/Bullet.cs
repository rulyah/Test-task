using Configs;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _firstPos;
    private bool _canDamage = true;
    
    public void Fire(Vector3 direction)
    {
        _rigidbody.AddForce(direction * GameConfig.instance.bulletSpeed, ForceMode.Impulse);
    }

    private void Start()
    {
        _firstPos = transform.position;
    }

    private void HitEnemy(IALive obj)
    {
        obj.TakeDamage(Random.Range(GameConfig.instance.minBulletDamage, GameConfig.instance.maxBulletDamage + 1));
    }

    private void HideBullet()
    {
        transform.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        Factory.instance.HideBullet(this);
    }

    private void Update()
    {
        if(!_canDamage) return;
        var secondPos = transform.position;
        if (Physics.Raycast(transform.position, -transform.forward, out var hit,
                Vector3.Distance(secondPos, _firstPos)))
        {
            _firstPos = secondPos;
            if (hit.transform.GetComponent<IALive>() is Ship ship)
            {
                HitEnemy(ship);
                _canDamage = false;
                HideBullet();
            }
        }
        else
        {
            if(transform.position.y <= GameConfig.instance.despawnDepth) HideBullet();
        }
    }
}