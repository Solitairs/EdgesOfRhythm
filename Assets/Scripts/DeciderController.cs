using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderController : MonoBehaviour
{
    public int id = -1;
    private SpectralController SpectralController;
    private void Awake()
    {
        id = -1;
    }
    private void Start()
    {
        SpectralController=GameObject.FindGameObjectWithTag("SpectralController").GetComponent<SpectralController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (id == -1) return;
        transform.position = new Vector3(SpectralController.spectralData.deciders[id].x.TPos(SpectralController.gameTime), SpectralController.spectralData.deciders[id].y.TPos(SpectralController.gameTime));
        Debug.Log(SpectralController.spectralData.deciders[id].x.TPos(SpectralController.gameTime));//TODO: 矫正坐标(世界坐标与ui坐标)
    }
}
