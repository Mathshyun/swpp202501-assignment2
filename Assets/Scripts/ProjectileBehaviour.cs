using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float lifeTime;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
    }

    private void Update()
    {
        if (lifeTime < 0f) Destroy(gameObject);
        
        lifeTime -= Time.deltaTime;
    }
}
