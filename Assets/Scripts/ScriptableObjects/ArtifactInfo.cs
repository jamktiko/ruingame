
using UnityEngine;


[CreateAssetMenu(fileName = "RabbitsFootSO", menuName = "Game/ArtifactInfo")]
public class ArtifactInfo : ScriptableObject
{
    public string Name;
    public string VisualDescription;
    public string Description;
    public string Rarity;
    public string StatChanges;
    public string SpecialEffect;
}
