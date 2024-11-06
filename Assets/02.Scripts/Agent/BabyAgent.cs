using UnityEngine;
using UnityEngine.AI;
using FC;

public class BabyAgent : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private CatchObject _catchObject;

    public LayerMask groundMask = TagAndLayer.LayerMasking.Ground;

    public float wanderRadius = 10f; // �ƱⰡ �̵��� ���� �ݰ�
    public float wanderTimer = 5f; // ���ο� �������� �����ϴ� �ð� ����
    public Transform holdPos;

    private float timer;
    private bool isInCrib = false; // �ƱⰡ crib �ȿ� �ִ��� ����

    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _catchObject = FindObjectOfType<CatchObject>(); // CatchObject ��ũ��Ʈ ����

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
            // �ƱⰡ crib �ȿ� �ִ� ���� ������ ��� �� "Sleep" �ִϸ��̼� ����
            _agent.SetDestination(holdPos.position);
            SetAnimationState("Sleep");
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        // �־��� �ݰ�(dist) ������ ������ ������ ������.
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin; // ������ ���⿡ ���� ��ġ(origin)�� ���� ���� �������� ������.

        NavMeshHit navHit;
        // ��ȿ�� NavMesh ���� ��ġ�� ã�� ��ȯ (randDirection�� NavMesh ���� �ִ��� Ȯ��)
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;  // ��ȿ�� ��ġ�� ��ȯ.
    }

    // �ִϸ��̼� ���¸� �����ϴ� ���� �޼ҵ�
    private void SetAnimationState(string stateName, float transitionDuration = 0.1f, int StateLayer = 0)
    {
        // ������ ���̾�(StateLayer)�� �ش� �ִϸ��̼� ���°� �����ϴ��� Ȯ��
        if (_anim.HasState(StateLayer, Animator.StringToHash(stateName)))
        {
            // ������ ���·� �ε巯�� ��ȯ ȿ���� �Բ� �ִϸ��̼��� ������
            _anim.CrossFadeInFixedTime(stateName, transitionDuration, StateLayer);

            //�켱������ ����
            if (StateLayer == 1)
                SetLayerPriority(1, 1);
        }
    }

    private void SetLayerPriority(int StateLayer = 1, int Priority = 1) // �ִϸ������� ���̾� �켱���� ��(����) ����
    {
        _anim.SetLayerWeight(StateLayer, Priority);
    }

    // ���鿡 �ִ��� Ȯ���ϴ� �޼ҵ�
    private bool IsOnGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, 1f, groundMask);
    }

    // �ݶ��̴��� ������ �� ó���ϴ� �޼ҵ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("babyCrib"))
        {
            isInCrib = true;
        }
    }

    // �ݶ��̴��� �ִ� ���� ó���ϴ� �޼ҵ�
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("babyCrib"))
        {
            isInCrib = true;
        }
    }

    // �ݶ��̴����� ������ �� ó���ϴ� �޼ҵ�
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("babyCrib"))
        {
            isInCrib = false;
        }
    }

    // NavMeshAgent Ȱ��ȭ �޼ҵ�
    public void EnableAgent()
    {
        _agent.enabled = true;
    }

    // NavMeshAgent ��Ȱ��ȭ �޼ҵ�
    public void DisableAgent()
    {
        _agent.enabled = false;
    }
}
