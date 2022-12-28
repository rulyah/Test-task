using Configs;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody _rigidbody;
    
    private Vector3 _firstPos;
    private Vector3 _secondPos;

    private bool _canDamage = true;
    
    public void Fire(Vector3 direction)
    {
        _firstPos = transform.position;
        if (_canDamage == false) _canDamage = true;
        _rigidbody.AddForce(direction * GameConfig.instance.bulletSpeed, ForceMode.Impulse);
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
        _secondPos = transform.position;
            if (Physics.Raycast(_secondPos, Vector3.Normalize(_secondPos - _firstPos), out var hit,
                    Vector3.Distance(_secondPos, _firstPos)))
        {
            _firstPos = _secondPos;
            if (hit.collider.gameObject.GetComponent<IALive>() is Ship ship)
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