using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLineRender : MonoBehaviour
{
     RaycastHit raycastHit;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward),
            out raycastHit, Mathf.Infinity, 0, QueryTriggerInteraction.Ignore);

        Vector3 vHitPosition;

        if (raycastHit.collider == null)
            vHitPosition = Camera.main.transform.forward * 10000000;
        else
            vHitPosition = raycastHit.point;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, vHitPosition);
    }
}
