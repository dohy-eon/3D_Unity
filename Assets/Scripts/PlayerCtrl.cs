using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody rigid;
    float jumpForce = 250.0f;
    float walkForce = 5.0f;
    GameObject item;
    int score = 0;

    public AudioClip pickupSound; // 아이템 획득 사운드
    public ParticleSystem pickupEffect; // 파티클 이펙트 프리팹

    private AudioSource audioSource;

    void Start()
    {
        this.rigid = GetComponent<Rigidbody>();
        this.item = GameObject.Find("Item");
        this.audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid.velocity.y == 0.0f)
        {
            this.rigid.AddForce(transform.up * this.jumpForce);
        }

        // 이동
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 2;
            this.rigid.AddForce(transform.right * key * this.walkForce);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -2;
            this.rigid.AddForce(transform.right * key * this.walkForce);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            key = 2;
            this.rigid.AddForce(transform.forward * key * this.walkForce);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            key = -2;
            this.rigid.AddForce(transform.forward * key * this.walkForce);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("아이템 획득!");

        score += 10;

        if (score >= 0)
        {
            this.item.GetComponent<Text>().text = "Score: " + score.ToString();
        }

        // 사운드 재생
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

        // 파티클 생성
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
        }

        Destroy(other.gameObject);
    }
}
