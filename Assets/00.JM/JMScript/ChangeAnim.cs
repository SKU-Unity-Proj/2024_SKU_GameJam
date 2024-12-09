using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        {
            anim = GetComponent<Animator>();
        }
    }

    public void TChangeAnim()
    {
        anim.SetTrigger("isTurn");
    }
}
