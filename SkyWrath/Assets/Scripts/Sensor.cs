using UnityEngine;
using System.Collections.Generic;

public class Sensor : MonoBehaviour
{
    [Header("Detection Settings")]
    public float scanRadius = 10f;
    public string[] detectableTags;

    [Header("Detected Objects")]
    public List<GameObject> detectedObjects = new List<GameObject>();
    public GameObject closestDetectedObject;
    
    public GameObject ScanArea()
    {
        detectedObjects.Clear();
        closestDetectedObject = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanRadius);
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hitColliders)
        {
            GameObject obj = hit.gameObject;

            // Skip self
            if (obj == gameObject)
                continue;

            foreach (string tag in detectableTags)
            {
                if (obj.CompareTag(tag))
                {
                    detectedObjects.Add(obj);

                    float dist = Vector3.Distance(transform.position, obj.transform.position);
                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        closestDetectedObject = obj;
                    }
                    break;
                }
            }
        }

        return closestDetectedObject;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}