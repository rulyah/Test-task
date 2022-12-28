using System;
using System.Collections;
using Configs;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Camera _camera;

    public bool isCanShoot = true;

    public void Shoot(IProjectile projectile)
    {
        isCanShoot = false; 
        StartCoroutine(Delay(GameConfig.instance.prepareForShooting, () =>
        {
            projectile.transform.position = _shootPoint.position;
            projectile.Fire(_shootPoint.forward);
            PlayShootFX();
            Reload();
        }));
    }

    private void Reload()
    {
        StartCoroutine(Delay(GameConfig.instance.reloadTime, () => isCanShoot = true));
    }

    private void PlayShootFX()
    {
        var fx = Factory.instance.GetShootFX();
        fx.transform.SetParent(_shootPoint);
        fx.transform.localPosition = Vector3.zero;
        StartCoroutine(Delay(1.0f, () => Factory.instance.HideShootFX(fx)));
    }
    
    private void Update()
    {
        if (Quaternion.Angle(transform.rotation, _camera.transform.rotation) > 1.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _camera.transform.rotation, 
                GameConfig.instance.gunRotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator Delay(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
    }
}