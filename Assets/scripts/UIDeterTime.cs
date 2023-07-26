using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDeterTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<TMP_InputField>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().time.ToString();
    }
}
