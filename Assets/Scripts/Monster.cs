using System.Collections;
//using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//public enum MonsterState
//{
//    Idle,       // 待机
//    Move,       // 移动
//    Attack,     // 攻击
//    TakeDamage, // 受伤
//    Dead        // 死亡
//}

public class Monster : MonoBehaviour
{
    public RectTransform minHealthPos;
    public RectTransform maxHealthPos;
    public RectTransform healthPos;
    private bool isAtk;

    protected float speed = 1f;
    protected float damage = 2f;
    protected float maxHealth;
    private float currentHealth;
    private Transform playerPos;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private float time = 0;
    private float timer = 0;
    //protected virtual float Damage
    //{
    //    get => GameData.Instance.MonsterAttack;
    //    set => GameData.Instance.MonsterAttack = value;
    //}

    protected virtual void Initialize()
    {
        maxHealth = GameData.Instance.MonsterHealth * 5f;
        damage = GameData.Instance.MonsterAttack * 2f;
        speed = GameData.Instance.MonsterSpeed * 1.5f;
    }
    private void Awake()
    {
        // 通过标签查找玩家（确保玩家对象有 "Player" 标签）
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerPos = playerObj.transform;
        }
        else
        {
            Debug.LogError("未找到玩家对象！");
        }
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Initialize();
        currentHealth = maxHealth;
        healthPos.position = maxHealthPos.position;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerPos.transform.position);
        //if (agent.remainingDistance < agent.stoppingDistance)
        //{
        //    animator.SetTrigger("Attack");
        //}
        //else
        //{
        //    //PlayAnim("move");
        //    //IsStopped = false;
        //}
        if ((transform.position - playerPos.position).magnitude < 2f)
        {
            animator.SetTrigger("Attack");
            if (time == 0)
                time = animator.GetCurrentAnimatorStateInfo(0).length;
            //if(animator.GetCurrentAnimatorStateInfo(0).length)//  获取当前动画状态的长度
        }

        if (time != 0)
        {
            timer += Time.deltaTime;
            if(timer >= time*0.5 && timer <= time)
                isAtk = true;
            if (timer >= time)
            {
                time = 0;
                timer = 0;
                isAtk = false;
            }
        }
    }

    public void PlayAkt()
    {
        isAtk = true;
    }
    public void StopAkt()
    {
        isAtk = false;
    }

    //private void StopMone(float time)
    //{
    //    agent.isStopped = true;
    //    time -= Time.deltaTime;
    //    if (time <= 0)
    //        agent.isStopped = false;
    //}
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthPos.position -= (maxHealthPos.position - minHealthPos.position) * (damage / maxHealth);
        animator.SetTrigger("TakeDamage");
        if (currentHealth <= 0)
        {
            if(Random.Range(0, 100) < 25)
                PropSpawner.Instance.SpawnerPropPrefabs(transform);
            gameObject.GetComponent<Collider>().enabled = false;
            animator.SetTrigger("Die");
            //Die();
            Invoke("Die", 1.3f);
        }
        //StopMone(animator.GetCurrentAnimatorStateInfo(0).length);
    }
    public void Die()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAtk)
            return;
        if (other.CompareTag("Player"))
        {
            print("Player");
            GameData.Instance.player.TakeDamage(damage);
            //Player player = other.GetComponent<Player>();
            //if (player != null)
            //    player.TakeDamage(damage);
        }
    }
}
