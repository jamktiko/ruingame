using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class RandomArtifactEffect : MonoBehaviour
{
    public string ArtifactName;
    public Artifact artifact;
    public List<ArtifactInfo> Commons;
    public List<ArtifactInfo> Rares;
    public List<ArtifactInfo> Mythics;

    void Awake()
    {
        artifact = GetComponent<Artifact>();
        RemoveEquippedArtifactsFromList(Commons);
        RemoveEquippedArtifactsFromList(Rares);
        RemoveEquippedArtifactsFromList(Mythics);
        AddRandomEffect();
        try { ArtifactName = artifact.ArtifactInfo.Name; }
        catch { ArtifactName = "no artifact info"; }
    }

    void RemoveEquippedArtifactsFromList(List<ArtifactInfo> artifacts)
    {
        try
        {
            List<Artifact> equippedArtifacts = PlayerManager.Instance._playerData.artifactList;
            int i = 0;
            while (i < artifacts.Count)
            {
                bool artifactIsEquipped = equippedArtifacts.Exists(x => x.ArtifactInfo.Name == artifacts[i].Name);
                if (artifactIsEquipped)
                    artifacts.RemoveAt(i);
                else
                    i++;
            }
        }
        catch (System.Exception e)
        { Debug.Log("error in removing artifact info from list: " + e.Message); }
    }

    private void AddRandomEffect()
    {
        float commonMax = 0.45f;
        float rareMax = 0.35f + commonMax;
        float mythicMax = 1f;
        bool artifactAdded = false;
        bool errorInAddingEffect = false;
        if (Commons.Count + Rares.Count + Mythics.Count > 0)
        {
            do
            {
                float rnd = Random.Range(0f, 1f);

                if (rnd <= commonMax && Commons.Count > 0)
                {
                    AddEffect(Commons, ref artifactAdded, ref errorInAddingEffect);
                }
                else if (rnd <= rareMax && rnd > commonMax && Rares.Count > 0)
                {
                    AddEffect(Rares, ref artifactAdded, ref errorInAddingEffect);
                }
                else if (rnd <= mythicMax && rnd > rareMax && Mythics.Count > 0)
                {
                    AddEffect(Mythics, ref artifactAdded, ref errorInAddingEffect);
                }
            } while (!artifactAdded && !errorInAddingEffect);
        }
    }

    private void AddEffect(List<ArtifactInfo> goList, ref bool artifactAdded, ref bool error)
    {

        int index = Mathf.FloorToInt(Random.Range(0f, goList.Count));
        ArtifactInfo info = goList[index];
        artifact.ArtifactInfo = info;
        artifactAdded = false;
        error = false;
        try
        {
            ArtifactEffect effect = GetEffect(info.Name);
            if (effect != null)
            {
                artifact.ArtifactEffect = effect;
                artifactAdded = true;
            }
        }
        catch (System.Exception e)
        {
            artifactAdded = false;
            error = true;
            Debug.Log("error in random artifact effect: " + e.Message);
        }
    }

    private ArtifactEffect GetEffect(string effectName)
    {
        switch (effectName)
        {
            case "Atlantean Trident":
                return gameObject.AddComponent<AtlanteanTrident>();
            case "Book of the wise ones":
                return gameObject.AddComponent<BookOfTheWiseOnes>();
            case "Cape of Agility":
                return gameObject.GetComponent<CapeOfAgility>();
            case "Essence of Kraken":
                return gameObject.GetComponent<EssenceOfKraken>();
            case "Feather of the Phoenix":
                return gameObject.AddComponent<FeatherOfThePhoenix>();
            case "Kappa’s plate":
                return gameObject.AddComponent<KappasPlate>();
            case "Leviathan’s hide":
                return gameObject.AddComponent<LeviathansHide>();
            case "Rabbits foot":
                return gameObject.AddComponent<RabbitsFoot>();
            case "Scythe of the Grim Reaper":
                return gameObject.AddComponent<ScytheOfTheGrimReaper>();
            case "The Ruined ring":
                return gameObject.AddComponent<TheRuinedRing>();
            default:
                return null;
        }
    }
}
