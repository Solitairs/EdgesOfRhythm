using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
public class SpectralController : MonoBehaviour
{
    public Transform canvas;
    public static int size = 2;
    public static float DefaultSpeed = 50;
    public UnityEngine.Object Settings;
    public GameObject deciderPool;
    public static SpectralData spectralData=new SpectralData();
    public KeyCode[] KeyCodes;
    public AudioSource audioSource;
    public float gameTime { get { return audioSource.time; } private set { audioSource.time = value; } }
    public DeciderController FocusDecider;
    public Transform[] transforms;
    public static float targetX=1;
    public static NoteController[] Notes;
    public static DeciderController[] Deciders;
    private bool isAltDown;
    public void size2()
    {
        size = 2;
        targetX = transforms[0].position.x;
    }
    public void size4()
    {
        size = 4;
        targetX = transforms[1].position.x;
    }
    public void size8()
    {
        size = 8;
        targetX = transforms[2].position.x;
    }
    public void size16()
    {
        size = 16;
        targetX = transforms[3].position.x;
    }
    public void size32()
    {
        size = 32;
        targetX = transforms[4].position.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        targetX = transforms[0].position.x;
        gameTime = 0;
        isAltDown = false;
        spectralData.setDeciders(9,deciderPool);
        audioSource=GameObject.FindGameObjectWithTag("audio").GetComponent<AudioSource>();
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2)).y);
    }
    public void showSettings()
    {
        Instantiate(Settings, canvas);
    }
    public static void reloadSpectral()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isAltDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            isAltDown = false;
        }
        if (isAltDown && Input.GetKeyDown(KeyCode.A))
        {
            //Ä¬ÈÏ´¹Ö±ÏÂÂä
            if(audioSource.clip!=null)
                spectralData.addNote(
                    gameTime - 8, Mathf.Round(gameTime / (60 / spectralData.BPM/size)) * (60 / spectralData.BPM/size), FocusDecider.id, 
                    new SpectralData.Cpos(FocusDecider.transform.position.x), 
                    new SpectralData.Cpos(0, -DefaultSpeed, FocusDecider.transform.position.x + 8 * DefaultSpeed, 0, 0, 0, 0, 0, 0), 0
                    );
            reloadSpectral();
        }
    }
}
