using UnityEngine;
using UnityEngine.AI;
using FC;

public class BabyAgent : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private CatchObject _catchObject;

    public LayerMask groundMask = TagAndLayer.LayerMasking.Ground;

    public float wanderRadius = 10f; // 아기가 이동할 범위 반경
    public float wanderTimer = 5f; // 새로운 목적지를 설정하는 시간 간격
    public Transform holdPos;

    private float timer;
    private bool isInCrib = false; // 아기가 crib 안에 있는지 여부

    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _catchObject = FindObjectOfType<CatchObject>(); // CatchObject 스크립트 참조

        timer = wanderTimer;
    }

    void Update()
    {
        if (_catchObject != null && !_catchObject.isGround)
        {
            //_agent.ResetPath();
            _anim.SetBool("isFall", true);
            return;
        }

        if (!isInCrib)
        {
            timer += Time.deltaTime;

            if (!_agent.isOnNavMesh || !IsOnGround())
            {
                //_agent.ResetPath();
                SetAnimationState("Fall");
                return;
            }

            if (timer >= wanderTimer)
            {
                _anim.SetBool("isFall", false);
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _agent.SetDestination(newPos);
                SetAnimationState("Crawl");
                timer = 0;
            }
        }
        else
        {
            _anim.SetBool("isFall", false);
            // 아기가 crib 안에 있는 동안 목적지 취소 및 "Sleep" 애니메이션 유지
            _agent.SetDestination(holdPos.position);
            SetAnimationState("Sleep");
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    // 애니메이션 상태를 설정하는 보조 메소드
    private void SetAnimationState(string stateName, float transitionDuration = 0.1f, int StateLayer = 0)
    {
        if (_anim.HasState(StateLayer, Animator.StringToHash(stateName)))
        {
            _anim.CrossFadeInFixedTime(stateName, transitionDuration, StateLayer);

            if (StateLayer == 1)
                SetLayerPriority(1, 1);
        }
    }

    private void SetLayerPriority(int StateLayer = 1, int Priority = 1) // 애니메이터의 레이어 우선순위 값(무게) 설정
    {
        _anim.SetLayerWeight(StateLayer, Priority);
    }

    // 지면에 있는지 확인하는 메소드
    private bool IsOnGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, 1f, groundMask);
    }

    // 콜라이더에 들어왔을 때 처리하는 메소드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("babyCrib"))
        {
            isInCrib = true;
        }
    }

    // 콜라이더에 있는 동안 처리하는 메소드
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("babyCrib"))
        {
            isInCrib = true;
        }
    }

    // 콜라이더에서 나갔을 때 처리하는 메소드
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("babyCrib"))
        {
            isInCrib = false;
        }
    }

    // NavMeshAgent 활성화 메소드
    public void EnableAgent()
    {
        _agent.enabled = true;
    }

    // NavMeshAgent 비활성화 메소드
    public void DisableAgent()
    {
        _agent.enabled = false;
    }
}
