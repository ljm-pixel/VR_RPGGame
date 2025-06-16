﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float speed = 10;
    protected float damage = 1; //伤害
    public GameObject explosionPrefab;
    new Rigidbody rigidbody;

    private float time = 5f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetSpeed(Vector3 direction)
    {
        rigidbody.velocity = direction * speed;
    }

    protected virtual void OnEnable()
    {
        time = 5f;
        speed = 10f + GameData.Instance.player.AttackSpeed;
        damage = GameData.Instance.player.Attack * 0.3f;
    }

    void Update()
    {
        if(time > 0)
            time -= Time.deltaTime;
        else
            ObjectPool.Instance.PushObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);// 爆炸
        exp.transform.position = transform.position;
        exp.transform.parent = other.transform;

        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                SoundManager.Instance.Play3DSound(2, monster.transform.position, 1);
            }
        }
        // Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
