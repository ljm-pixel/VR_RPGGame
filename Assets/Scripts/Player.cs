using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float currentHealth = 10f;
    private float maxHealth = 10f;
    private float attackSpeed = 1f;
    private float attack = 10f;
    private float curativeDose = 1f;
    private int numBullet = 50;
    private int shotgunBulletNum = 3;

    public GameObject[] guns;
    private int gunNum = 0;

    //private float currentHealth;
    private Transform playerPos;
    private float beInjuredIntervalTime = 0.5f;

    public float CurrentHealth
    {
        get => currentHealth;
        set 
        {
            currentHealth = value;
            GameUI.Instance.SetHealthText(currentHealth/maxHealth);
        }
    }
    public float MaxHealth
    {
        get => maxHealth;
        set 
        { 
            //GameUI.Instance.SetHealthText(maxHealth);
            maxHealth = value; 
        }
    }
    public float AttackSpeed
    {
        get => attackSpeed;
        set 
        {
            attackSpeed = value;
            GameUI.Instance.SetAttackSpeedText(attackSpeed);
        }
    }
    public float Attack
    {
        get => attack;
        set 
        {
            attack = value;
            GameUI.Instance.SetAttackText(attack); 
        }
    }

    public float CurativeDose
    {
        get { return curativeDose; }
        set { curativeDose = value; }
    }
    public int NumBullet
    {
        get { return numBullet; }
        set
        {
            numBullet = value;
            GameUI.Instance.SetBulletNumText(numBullet);
        }
    }
    public int ShotgunBulletNum
    {
        get { return shotgunBulletNum; }
        set { shotgunBulletNum = value; }
    }

    public float BeInjuredIntervalTime
    {
        get { return beInjuredIntervalTime; }
        set { beInjuredIntervalTime = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = currentHealth;
        //guns[0].SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        if(beInjuredIntervalTime <= 0)
        {
            CurrentHealth -= damage;
            beInjuredIntervalTime = 0.01f;
        }
        if (CurrentHealth <= 0)
        {
            print("结束了");
            EndGame.Instance.ShowMe();
            GameUI.Instance.StopGame();
            ExplodeProp explodeProp = new ExplodeProp();
            explodeProp.TriggerEffect();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Monster"))
    //    {
    //        TakeDamage(1);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if(beInjuredIntervalTime > 0)
            beInjuredIntervalTime -= Time.deltaTime;
    }

    //void SwitchGun()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Q))
    //    //{
    //    //    guns[gunNum].SetActive(false);
    //    //    if (--gunNum < 0)
    //    //    {
    //    //        gunNum = guns.Length - 1;
    //    //    }
    //    //    guns[gunNum].SetActive(true);
    //    //}
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        guns[gunNum].SetActive(false);
    //        if (++gunNum > guns.Length - 1)
    //        {
    //            gunNum = 0;
    //        }
    //        guns[gunNum].SetActive(true);
    //    }
    //}
}
