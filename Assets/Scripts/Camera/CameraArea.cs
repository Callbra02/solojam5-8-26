using UnityEngine;
using UnityEngine.Events;

public class CameraArea : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Transform> OnEntry;
    
    void Start()
    {
        OnEntry ??= new UnityEvent<Transform>();
    }

    // When player enters a camera area, send signal 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnEntry?.Invoke(this.transform);
        }
    }
}