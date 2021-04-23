
    using DefaultNamespace;
    using UnityEngine;
    [RequireComponent(typeof(EnemyAttackHandler))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class Enemy_StateMachine : MonoBehaviour, IEnemy
    {
        public EnemyMovement movementController;
        public BaseAttackHandler attackHandler;
        private EnemyHealth healthSystem;
        public Animator entityAnimator;
        
        public int areaRaycasts = 30;
        public AreaCheck areaInformation;
        public float attackRange;

        public Enemy_Group enemyGroup;
        public bool checkingForPlayer;
        public LayerMask obstacleLayer;
        public GameObject playerTarget;
        public LayerMask hitTestLayer;
        
        public bool stunned;

        public bool alive = true;
        private bool alerted;
        
        public WeightedDirection[] pD;
        public Vector3 currentTargetPos;
        public Vector3 currentTargetDirection;

        public enum EnemyType
        {
            Melee,
            Ranged,
            Caster
        }

        public EnemyType enemyType;
        
        public enum baseStates
        {
            PATROL,
            MOVE,
            ATTACK,
            DEATH
        }

        [SerializeField]
        public baseStates BaseStates;

        public virtual void Awake()
        {
            areaInformation = gameObject.AddComponent<AreaCheck>();
            areaInformation.AmountOfRaycasts = areaRaycasts;
            attackHandler = GetComponent<BaseAttackHandler>();

            attackRange = attackHandler.baseAttack.range;
            entityAnimator = GetComponentInChildren<Animator>();
            movementController = GetComponent<EnemyMovement>();
            movementController._characterController = this;
            movementController._characterAnimator = entityAnimator;
        }
        public virtual void Start()
        {
            transform.parent = null;
            SetState(new PatrolState(this));
            
        }

        public string state;
        public virtual void Update()
        {
           Debug.DrawRay(transform.position, currentTargetDirection, Color.black);
           _currentState.Tick();
           state = _currentState.Name;
           if (pD != null)
           {
               foreach (WeightedDirection wd in pD)
               {
                   if (wd.weight > 0f)
                   {
                       Debug.DrawRay(transform.position, wd.direction * wd.weight * 5f, Color.green);
                   }
                   else if (wd.weight < 0f)
                   {
                       Debug.DrawRay(transform.position, wd.direction * Mathf.Abs(wd.weight) * 5f, Color.red);
                   }
               }
           }
        }
        public State _currentState { get; set; }
        public  virtual void SetState(State state)
        {
            _currentState?.OnStateExit();
            
            _currentState = state;

            _currentState?.OnStateEnter();
        }

        public virtual string GetStateText()
        {
            string res = "";
            res = _currentState.Name;
            return res;
        }

        public virtual bool CanSeePlayer()
        {
            RaycastHit hit = new RaycastHit();
            int nRays = 100;
            for (int i = 0; i < nRays; i++)
            {
                float theta = (float)i / nRays * 2 * Mathf.PI;
                if (Physics.Raycast(transform.position, new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta)), out hit, 50.0f, hitTestLayer) && hit.collider.tag == "Player")
                {
                    SetTargetPos(hit.point);
                    return true;
                }
            }
            return false;
        }

        public abstract void AttackAction();

        public virtual bool HasReachedAttackRange()
        {
            float dist = Vector3.Distance(transform.position, playerTarget.transform.position);
            if (dist <= attackRange)
            {
                return true;
            }
            return false;
        }

        public virtual void SetTargetPos(Vector3 target)
        {
            currentTargetPos = target;
        }

        public virtual void Alert()
        {
            playerTarget = GameObject.FindWithTag("Player");
        }

        public virtual void Die()
        {
            enemyGroup.GetComponent<Spawner>().EntityDeath();
            alive = false;
            Destroy(gameObject, 0.5f);
        }

        public void AlertAllEnemies()
        {
            enemyGroup.AlertManager();
        }
        
        public bool CheckForPlayer()
        {
            RaycastHit[] hitTargets = areaInformation.RayCastAroundArea(hitTestLayer).hitInfo;
            foreach (RaycastHit hit in hitTargets)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        playerTarget = hit.collider.gameObject;
                        return true;
                    }
                }
            }
            return false;
        }
        public virtual Vector3 DecidePatrolDirection()
        {
            var finalDecision = new Vector3();
            var p = enemyGroup.patrolArea;
            var rp = RandomPointInPatrolArea();
            rp.y = 0;
            RaycastHit hit;
            Vector3 dir = (rp - transform.position);
            Ray ray = new Ray(transform.position, dir);
            if (Physics.SphereCast(ray, 0.5f, out hit, obstacleLayer))
            {

            }
            finalDecision = rp;
            finalDecision.y = 0;
            currentTargetDirection = dir;
            currentTargetDirection.y = 0;
            return finalDecision;
        }

        public struct WeightedDirection
        {
            public Vector3 direction;
            public float weight;
        }

        private Vector3 RandomPointInPatrolArea()
        {
            var p = enemyGroup.patrolArea;
            var rp = new Vector3(
                Random.Range(p.min.x, p.max.x),
                0,
                Random.Range(p.min.z, p.max.z)
            );
            rp = transform.TransformDirection(rp);
            return rp;
        }
        private float sense = 0.08f;
        public virtual Vector3 DecidePathToPlayer()
        {
            var targetDir = currentTargetPos - transform.position;
            targetDir.y = 0;
            targetDir = targetDir.normalized;
            var distance = Vector3.Distance(transform.position, currentTargetPos);
            pD = new WeightedDirection[areaInformation.AmountOfRaycasts];
            AreaCheck.RayCastValues RCV = areaInformation.RayCastAroundArea(obstacleLayer);
            var hitTargets = RCV.hitInfo;
            var hitPoints = RCV.points;
            for (int i = 0; i < hitPoints.Length; i++)
            {
                pD[i].weight = 0f;
                hitPoints[i].y = 0;
                pD[i].direction = (hitPoints[i] - transform.position).normalized;
                pD[i].weight = (Vector3.Dot(targetDir, pD[i].direction));
            }
            for (int i = 0; i < hitTargets.Length; i++)
            {
                var h = hitTargets[i];
                if (h.collider != null)
                {
                    if (h.collider.tag == "Enemy")
                    {
                        pD[i].weight -= 0.1f;
                    }

                    if (h.collider.tag == "Ground")
                    {
                        pD[i].weight -= (1 / Vector3.Distance(transform.position, hitTargets[i].point));
                    }

                    if (h.collider.tag == "Player")
                    {
                        pD[i].weight += 1f;
                    }
                }
                else
                {
                    pD[i].weight += 0.3f;
                }
                pD[i].weight += 0.05f * Vector3.Dot(transform.forward, pD[i].direction);
            }

            var weightedMax = WeightedMax(pD);
            currentTargetDirection = weightedMax * 10f;
            weightedMax.y = 0f;
            return weightedMax;
        }

        protected Vector3 WeightedMax(WeightedDirection[] wD)
        {
            var currentDirection = new Vector3();
            var currentWeight = -10f;
            foreach (WeightedDirection w in wD)
            {
                if (w.weight > currentWeight+0.1f)
                {
                        currentDirection = w.direction;
                        currentWeight = w.weight;
                }
            }
            return currentDirection;
        }
    }
    
        
