using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Vector2 _shootVector;
    [SerializeField] private float _shootForce;
    public Collider2D collider;
    bool timeToShoot;

    private void Update()
    {

    }

    public void ShootProjectile()
    {
        if (!_projectilePrefab) return;
        var go = GameObject.Instantiate(_projectilePrefab);
        go.transform.position = this.transform.position;
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.AddForce(_shootVector.normalized * _shootForce, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.attachedRigidbody.GetComponent<Unit>();
        if (unit)
        {
            timeToShoot = true;
            if (unit.OwnerNumber != 1)
            {
                ShootProjectile();
                StartCoroutine(ShootDelay());
            }
        }
        StartCoroutine(ShootDelay());

    }
    IEnumerator ShootDelay()
    {
        timeToShoot = false;
        yield return new WaitForSeconds(1);
    }
}