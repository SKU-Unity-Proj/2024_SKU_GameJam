using UnityEngine;
using UnityEngine.AI;

public class BabyAgent : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;
    public float wanderRadius = 10f; // 아기가 이동할 범위 반경
    public float wanderTimer = 5f; // 새로운 목적지를 설정하는 시간 간격

    private float timer;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            this._agent.SetDestination(newPos);
            SetAnimationState("Crawl");
            timer = 0;
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
}
