using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool open;
    public Animator animator;
    private void Start()
    {
        open = false;
    }
    private void Update()
    {
        if (open) animator.SetBool("Open", true);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (open) 
            SceneManager.LoadScene("End");
    }
}
