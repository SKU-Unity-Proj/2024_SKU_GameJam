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
        // 주어진 반경(dist) 내에서 무작위 방향을 설정함.
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin; // 무작위 방향에 시작 위치(origin)를 더해 최종 목적지를 결정함.

        NavMeshHit navHit;
        // 유효한 NavMesh 위의 위치를 찾아 반환 (randDirection이 NavMesh 위에 있는지 확인)
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;  // 유효한 위치를 반환.
    }

    // 애니메이션 상태를 설정하는 보조 메소드
    private void SetAnimationState(string stateName, float transitionDuration = 0.1f, int StateLayer = 0)
    {
        // 지정된 레이어(StateLayer)에 해당 애니메이션 상태가 존재하는지 확인
        if (_anim.HasState(StateLayer, Animator.StringToHash(stateName)))
        {
            // 지정한 상태로 부드러운 전환 효과와 함께 애니메이션을 변경함
            _anim.CrossFadeInFixedTime(stateName, transitionDuration, StateLayer);

            //우선순위를 설정
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
