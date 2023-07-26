using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void addNoteNow()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().addNoteNow();
    }
}
