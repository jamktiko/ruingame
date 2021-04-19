
using UnityEngine;

public class PatrolArea : AreaCheck
{
    [SerializeField] [Range(-5, 5)] public float sizeCorrection;
    [SerializeField] [Range(0, 100)] protected float heightRange;
    [SerializeField] [Range(0, 100)] public float heightCorrection;
    public override Bounds CreateArea()
    {
        Vector3 patrolAreaPosition = new Vector3(transform.position.x, FindGroundHeight(), transform.position.z);
        float generatedMaxSize = CheckOverLap(patrolAreaPosition);
        Bounds initialBounds = new Bounds(patrolAreaPosition, new Vector3(generatedMaxSize, 0.2f, generatedMaxSize));
        return initialBounds;
    }

    protected virtual float FindGroundHeight()
    {
        float height = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, heightRange, obstacleLayers))
        {
            height = (transform.position.y - hit.distance) + heightCorrection;
        }

        return height;
    }

    private float CheckOverLap(Vector3 patrolAreaPosition)
    {
        float maximumInterruptedSize = maximumSize;
        float angle = 0;
        for (int i = 0; i < AmountOfRaycasts + 1; i++)
        {
            Vector3 dir = transform.TransformDirection(Vector3.forward) * maximumSize;
            dir = Quaternion.Euler(0, angle, 0) * dir;
            angle += 360 / AmountOfRaycasts;
            Ray ray = new Ray(patrolAreaPosition, dir);
            RaycastHit hit;
            if (Physics.SphereCast(ray, detectionSensitivity, out hit, maximumInterruptedSize, obstacleLayers))
            {
                maximumInterruptedSize = hit.distance;
            }
        }

        return maximumInterruptedSize * 2 + sizeCorrection;
    }

    public void SetPatrolAreaSettings(PatrolAreaSettings settings)
    {
        var s = settings;
        obstacleLayers = s.obstacleLayers;

        maximumSize = s.maximumSize;

        sizeCorrection = s.sizeCorrection;

        heightRange = s.heightRange;

        heightCorrection = s.heightCorrection;

        AmountOfRaycasts = s.AmountOfRaycasts;
        detectionSensitivity = s.detectionSensitivity;
    }
}
