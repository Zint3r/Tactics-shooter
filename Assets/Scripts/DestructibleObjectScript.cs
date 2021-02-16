using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestructibleObjectScript : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
    public void ObjectActivation()
    {
        anim.SetTrigger("Activ");
    }
}