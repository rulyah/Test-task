using UnityEngine;

public interface IProjectile
{ 
    public Transform transform { get; }

    public void Fire(Vector3 direction);
}