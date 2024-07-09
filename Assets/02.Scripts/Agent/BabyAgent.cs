using UnityEngine;
using UnityEngine.AI;

public class BabyAgent : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;
    public float wanderRadius = 10f; // �ƱⰡ �̵��� ���� �ݰ�
    public float wanderTimer = 5f; // ���ο� �������� �����ϴ� �ð� ����

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

    // �ִϸ��̼� ���¸� �����ϴ� ���� �޼ҵ�
    private void SetAnimationState(string stateName, float transitionDuration = 0.1f, int StateLayer = 0)
    {
        if (_anim.HasState(StateLayer, Animator.StringToHash(stateName)))
        {
            _anim.CrossFadeInFixedTime(stateName, transitionDuration, StateLayer);

            if (StateLayer == 1)
                SetLayerPriority(1, 1);
        }

    }

    private void SetLayerPriority(int StateLayer = 1, int Priority = 1) // �ִϸ������� ���̾� �켱���� ��(����) ����
    {
        _anim.SetLayerWeight(StateLayer, Priority);
    }
}
