using UnityEngine;
using UnityEngine.AI;

public class Bee : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _carryCapacity = 5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _fatigueRate = 5f;
    [SerializeField] private float _recoveryAmount = 20f;
    [SerializeField] private float _lifeDuration = 60f;

    [Header("Flight Behavior")]
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _hoverAmplitude = 0.15f;
    [SerializeField] private float _hoverFrequency = 3f;
    [SerializeField] private float _wobbleAmplitude = 0.1f;
    [SerializeField] private float _wobbleFrequency = 2f;

    [Header("Hive Interaction")]
    [SerializeField] private float _disappearDistance = 2f;

    private IPollenReceiver _receiver;
    private int _carriedPollen;
    private float _fatigue;
    private bool _isOnExpedition;

    private BeeStateMachine _stateMachine;
    private NavMeshAgent _agent;

    private Vector3 _initialLocalPos;
    private float _hoverTimer;
    private float _wobbleTimer;

    private GameObject _body;

    private float _lifeTimer;

    public void Init(IPollenReceiver receiver)
    {
        _receiver = receiver;
    }

    private void Start()
    {
        _body = this.transform.GetChild(0).gameObject;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _agent.angularSpeed = 0;

        if (!_agent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                _agent.Warp(hit.position);
        }

        _initialLocalPos = transform.position;

        _stateMachine = new BeeStateMachine();
        _stateMachine.Initialize(new BeeIdleState(this, _stateMachine));

        _lifeTimer = 0f; // ⏳ Initialise le temps de vie
    }

    private void Update()
    {
        _stateMachine.Update();

        if (_isOnExpedition)
        {
            _fatigue += _fatigueRate * Time.deltaTime;
            _fatigue = Mathf.Clamp(_fatigue, 0, 100);
        }

        UpdateRotation();
        SimulateHover();
        CheckHiveProximity();

        UpdateLifeTimer();
    }

    private void UpdateLifeTimer()
    {
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer >= _lifeDuration)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void CheckHiveProximity()
    {
        if (_receiver == null) return;

        if (_receiver is MonoBehaviour receiverObj)
        {
            float distance = Vector3.Distance(transform.position, receiverObj.transform.position);
            if (distance <= _disappearDistance)
            {
                _body.SetActive(false);
            }
            else
            {
                _body.SetActive(true);
            }
        }
    }

    public void MoveTowards(Vector3 target)
    {
        if (_agent != null && _agent.isActiveAndEnabled)
        {
            _agent.SetDestination(target);
        }
    }

    public bool ReachedDestination()
    {
        if (_agent == null || !_agent.isActiveAndEnabled)
            return false;

        if (!_agent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                _agent.Warp(hit.position);
            else
                return false;
        }

        if (_agent.pathPending)
            return false;

        return _agent.remainingDistance <= _agent.stoppingDistance;
    }

    private void UpdateRotation()
    {
        if (_agent == null) return;

        Vector3 velocity = _agent.velocity;
        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * _rotationSpeed);
        }
    }

    private void SimulateHover()
    {
        _hoverTimer += Time.deltaTime * _hoverFrequency;
        _wobbleTimer += Time.deltaTime * _wobbleFrequency;

        Vector3 offset = Vector3.zero;
        offset.y = Mathf.Sin(_hoverTimer) * _hoverAmplitude;
        offset.x = Mathf.Sin(_wobbleTimer) * _wobbleAmplitude;

        transform.position += offset * Time.deltaTime;
    }

    public void StartExpedition() => _isOnExpedition = true;
    public void EndExpedition() => _isOnExpedition = false;

    public void CollectPollen(Flower flower)
    {
        if (flower.ContainsPollen())
        {
            int pollen = flower.GetPollen();
            int capacity = Mathf.RoundToInt(_carryCapacity);
            int amountTaken = Mathf.Min(capacity, pollen);
            _carriedPollen = amountTaken;
        }
    }

    public void DepositPollen()
    {
        if (_receiver != null && _carriedPollen > 0)
        {
            _receiver.AddPollen(_carriedPollen);
            _carriedPollen = 0;
            Recover();
        }
    }

    private void Recover()
    {
        _fatigue -= _recoveryAmount;
        _fatigue = Mathf.Clamp(_fatigue, 0, 100);
    }
    
    private void OnDestroy()
    {
        if (_receiver is Beehive hive)
        {
            hive.UnregisterBee(this);
        }
    }


    public bool IsCarryingPollen() => _carriedPollen > 0;
    public bool IsTired() => _fatigue >= 100;
    public float GetFatigue() => _fatigue;
}