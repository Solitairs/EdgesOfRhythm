using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void addNoteNow()
    {
        GameObject.FindGameObjectWithTag("NoteCreater").GetComponent<CreateNote>().addNoteNow();
    }
    public void addCmdNow()
    {
        GameObject.FindGameObjectWithTag("CmdCreater").GetComponent<CreateCmd>().addCmdNow();
    }
}
