using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Values")]
    public float projectileSpeed;
    public float projectileLifeTime;
    public Vector3 relativeSpawnPosition;
    
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject projectilePrefab;

    public void SpawnProjectile()
    {
        var spawnPosition = playerTransform.TransformPoint(relativeSpawnPosition);
        var spawnRotation = playerTransform.rotation;

        var projectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation, transform);
        projectile.GetComponent<Rigidbody>().velocity = projectileSpeed * projectile.transform.forward;
        projectile.GetComponent<ProjectileBehaviour>().lifeTime = projectileLifeTime;
    }
}
