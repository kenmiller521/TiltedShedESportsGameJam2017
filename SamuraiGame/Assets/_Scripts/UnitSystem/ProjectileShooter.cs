using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Vector2 _shootVector;
    [SerializeField] private float _shootForce;
    public Collider2D collider;

    private void Update()
    {
        OnTriggerEnter2D(collider);
        StartCoroutine(ShootDelay());
         
    }

    public void ShootProjectile()
    {
        if (!_projectilePrefab) return;
        var go = GameObject.Instantiate(_projectilePrefab);
        go.transform.position = this.transform.position;
        var rb = go.GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.AddForce(_shootVector.normalized * _shootForce, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShootProjectile();

    }
    IEnumerator ShootDelay()
    {
        _projectilePrefab.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(_projectilePrefab);
        _projectilePrefab.SetActive(true);
    }
}
