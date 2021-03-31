using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {	   
        public AreaCheck areaInformation;
        public float attackRange;
        public bool alerted;
       // public Enemy_Group enemyGroup;
        public bool checkingForPlayer;
        public enum NPC_EnemyAction { NONE = 0, IDLE, PATROL, INSPECT, ATTACK, APPROACH}
        public Animator npcAnimator;
        public GameObject playerTarget;
        public LayerMask hitTestLayer;
        public LayerMask obstacleLayer;
        Vector3 targetPos, startingPos;

        private Vector3 movementInput;
        private Rigidbody _entityRigidbody;
        public float movementSpeed = 10f;
        public NPC_PatrolNode patrolNode;
        
    }
}