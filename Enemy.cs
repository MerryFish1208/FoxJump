using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    protected Animator Anim;
    protected AudioSource deathAudio;

    private void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        deathAudio = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        
        deathAudio.Play();
        Anim.SetTrigger("death");
    }

}
