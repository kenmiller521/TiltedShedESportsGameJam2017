using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private Vector2 _shootVector;
    [SerializeField] private float _shootForce;
    private void Start()
    {
        if(!_spawnTransform)
        {
            _spawnTransform = this.transform;
        }
    }
    public void ShootProjectile()
    {
        if (!_projectilePrefab) return;
        var go = GameObject.Instantiate(_projectilePrefab);
        go.transform.position = _spawnTransform.position;
        var rb = go.GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.AddForce(_shootVector.normalized * _shootForce, ForceMode2D.Impulse);
        }
    }
}
