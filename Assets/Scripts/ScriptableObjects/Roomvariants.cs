
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "RoomVariants", menuName = "Game/Roomvariants")]
    public class Roomvariants :  ScriptableObject
    {
        public List<GameObject> possibleRooms;
    }
}