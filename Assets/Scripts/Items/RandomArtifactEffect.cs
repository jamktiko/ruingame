using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class RandomArtifactEffect : MonoBehaviour
{
    private Artifact artifact;
    public List<ArtifactInfo> Commons;
    public List<ArtifactInfo> Rares;
    public List<ArtifactInfo> Mythics;
    public string ArtifactName;
    // Start is called before the first frame update
    private void Awake()
    {
        artifact = GetComponent<Artifact>();
        AddRandomEffect();
        ArtifactName = artifact.ArtifactInfo.Name;
    }

    private void AddRandomEffect()
    {
        float common = 0.45f;
        float rare = 0.35f + common;
        float mythic = 1f;
        bool artifactAdded = false;

        if (Commons.Count + Rares.Count + Mythics.Count > 0)
        {
            do
            {
                float rnd = Random.Range(0f, 1f);

                if (rnd <= common && Commons.Count > 0)
                {
                    artifactAdded = AddEffectAndRemoveFromList(Commons);
                }
                else if (rnd <= rare && rnd > common && Rares.Count > 0)
                {
                    artifactAdded = AddEffectAndRemoveFromList(Rares);
                }
                else if (rnd <= mythic && rnd > rare && Mythics.Count > 0)
                {
                    artifactAdded = AddEffectAndRemoveFromList(Mythics);
                }
            } while (!artifactAdded);
        }
    }

    private bool AddEffectAndRemoveFromList(List<ArtifactInfo> goList)
    {

        int index = Mathf.FloorToInt(Random.Range(0f, goList.Count));
        ArtifactInfo info = goList[index];
        switch (info.Name)
        {
            case "Atlantean Trident":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<AtlanteanTrident>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;
                }
                return true;
            case "Book of the wise ones":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<BookOfTheWiseOnes>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;
            case "Cape of Agility":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<CapeOfAgility>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;

            case "Essence of Kraken":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<EssenceOfKraken>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;

            case "Feather of the Phoenix":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<FeatherOfThePhoenix>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;
            case "Kappa’s plate":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<KappasPlate>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;

            case "Leviathan’s hide":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<LeviathansHide>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;

            case "Rabbits foot":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<RabbitsFoot>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;

            case "Scythe of the Grim Reaper":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<ScytheOfTheGrimReaper>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;

            case "The Ruined ring":
                try
                {
                    ArtifactEffect effect = gameObject.AddComponent<TheRuinedRing>();
                    artifact.ArtifactEffect = effect;
                    artifact.ArtifactInfo = goList[index];
                    goList.RemoveAt(index);

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;

                }
                return true;


            default:
                return false;

        }

    }
}