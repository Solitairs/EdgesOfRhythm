using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCmdsInformation : MonoBehaviour
{
    private int type;
    // Start is called before the first frame update
    void Start()
    {
        type = GameObject.FindGameObjectWithTag("ChooseCmdType").GetComponent<TMP_Dropdown>().value+1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
