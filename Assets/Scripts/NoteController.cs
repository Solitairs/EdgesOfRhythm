using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public int id=-1;


    // Update is called once per frame
    void Update()
    {
        if (id == -1)
        {
            transform.position = new Vector3(-8000, 0);
            return;
        }

    }
}
