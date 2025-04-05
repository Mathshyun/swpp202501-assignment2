using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
{
    public int triggerValue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayManager.Instance.ActivateTrigger(triggerValue);
        }
    }
}
