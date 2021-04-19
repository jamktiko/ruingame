﻿
using UnityEngine;

public class AreaCheck : MonoBehaviour
{
    [SerializeField]
    protected LayerMask obstacleLayers;

    [SerializeField] [Range(0, 100)] protected float maximumSize = 10f;
    [SerializeField]
    [Range(10, 60)]
    public int AmountOfRaycasts = 10;
    [SerializeField] [Range(0, 10)] protected float detectionSensitivity = 0.5f;
    protected Vector3[] areaVertices;
    public virtual Bounds CreateArea()
    {
        return new Bounds(Vector3.zero, Vector3.zero);
    }
    public struct RayCastValues
    {
        public Vector3[] points;
        public RaycastHit[] hitInfo;
    }

    public RayCastValues RayCastAroundArea(LayerMask layerToCheck)
    {
        RayCastValues RCV;
        areaVertices = new Vector3[AmountOfRaycasts];
        RaycastHit[] hitInfos = new RaycastHit[AmountOfRaycasts];
        float angle = 0;
        for (int i = 0; i < AmountOfRaycasts; i++)
        {
            Vector3 dir = transform.TransformDirection(Vector3.forward) * maximumSize;
            dir = Quaternion.Euler(0, angle, 0) * dir;
            angle += 360 / AmountOfRaycasts;
            //Debug.DrawRay(transform.position, dir, Color.red);
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.SphereCast(ray,detectionSensitivity, out hit, maximumSize, layerToCheck))
            {
                areaVertices[i] = hit.point;
                hitInfos[i] = hit;
              // Debug.DrawLine(hit.point, transform.position, Color.blue);
            }
            else
            {
               areaVertices[i] = transform.position + (dir);
            }
        }
        RCV.points = areaVertices;
        RCV.hitInfo = hitInfos;
        return RCV;
    }
    protected virtual void Update()
    {
        //RayCastAroundArea(obstacleLayers);
    }
    
    void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maximumSize);
        
        Gizmos.color = Color.green;
        if (areaVertices != null) 
            for (int i = 0; i < areaVertices.Length; i++)
            {
                Gizmos.DrawWireSphere(areaVertices[i], 0.1f + detectionSensitivity);
            }
            */
    }
    
}