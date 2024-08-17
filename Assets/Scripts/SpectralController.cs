using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralController : MonoBehaviour
{
    public Transform canvas;
    public Object Settings;
    public SpectralData spectralData=new SpectralData();
    // Start is called before the first frame update
    void Start()
    {
        spectralData.setDeciders(9);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2)).y);
    }
    public void showSettings()
    {
        Instantiate(Settings, canvas);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
