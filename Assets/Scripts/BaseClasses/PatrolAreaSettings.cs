using UnityEngine;

[CreateAssetMenu(fileName = "PatrolAreaSettings", menuName = "Enemies/PatrolArea", order = 0)]
public class PatrolAreaSettings : ScriptableObject
{
    [SerializeField]
    public LayerMask obstacleLayers;
    [SerializeField]
    [Range(0, 100)]
    public float maximumSize;
    [SerializeField]
    [Range(-5, 5)]
    public float sizeCorrection;
    [SerializeField]
    [Range(0, 100)]
    public float heightRange;
    [SerializeField]
    [Range(0, 100)]
    public float heightCorrection;
    [SerializeField]
    [Range(10, 60)]
    public int AmountOfRaycasts = 10;
    [SerializeField] [Range(0, 10)] 
    public float detectionSensitivity = 0;
}
