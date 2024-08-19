using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAnimationController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = SpectralController.Deciders[0].transform.position;
    }
}
