using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FController : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, SpectralController.targetX, 7.5F*Time.deltaTime), transform.position.y, transform.position.z);
    }
}
