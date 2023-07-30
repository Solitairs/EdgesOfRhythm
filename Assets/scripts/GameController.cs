using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
/*
^N查看音符列表
Tab打开设置
Alt+S保存当前指令或音符
Alt+O打开
Alt+P保存
N创建音符
左右箭头跳时间1.5s
空格暂停或播放
 */
public class GameController : MonoBehaviour
{
    public static int mathNoteID = 0;
    public UnityEngine.UI.Image progress;
    public static int[] Pl;
    public TMP_InputField field;
    public UnityEngine.Object noteCreater,cmdCreater,NotesList,CmdsList,Settings,Saving,loading;
    public Transform canvas;
    public static ERSRegister register;
    public static ERSCommand cmd;
    public AudioSource Audio;
    private bool loadingDone = false;
    void Start()
    {
        Pl = new int[9];
        Pl[0] = 2;
        Pl[1] = 3;
        Pl[2] = 4;
        Pl[3] = 6;
        Pl[4] = 8;
        Pl[5] = 12;
        Pl[6] = 16;
        Pl[7] = 24;
        Pl[8] = 32;
        register = new ERSRegister();
        register.Intialize();
        cmd = new ERSCommand();
        //加载音乐
        string path;
        string local;
        //获取本工程Assets路径
        path = Application.dataPath;
        path += "/Music";
        //这里Music是一个与Assets同级的文件夹（用来存放音乐）,local:音乐名称，.wav：音乐后缀
        bool finded = false;
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*");
            for (int i = 0; i < files.Length; i++)
            {
                //忽略关联文件
                if (files[i].Name.EndsWith(".wav"))
                {
                    finded = true;
                    local = files[i].Name;
                    path += "/" + local;
                    break;
                }
            }
        }
        if (finded)
        {
            //使用www类加载播放
            StartCoroutine(Load(path));
        }
        else
        {
            Debug.LogError("No Musics");
        }
    }
    IEnumerator Load(string path)
    {
        if (File.Exists(path))
        {
            path = "file:///" + path;
            WWW ww = new WWW(path);
            yield return ww;
            if (ww.error == null && ww.isDone)
            {
                Audio.clip = ww.GetAudioClip();
                Audio.Play();
                Audio.pitch = 0;
                loadingDone = true;
            }
            else
            {
                print(ww.error);
            }
        }
    }
    public void showNotesList()
    {
        Instantiate(NotesList, canvas);
    }
    private void delNoteCreater()
    {
        Destroy(GameObject.FindGameObjectWithTag("NoteCreater"));
    }
    public void showCmdsList()
    {
        Instantiate(CmdsList, canvas);
    }
    private void delCmdCreater()
    {
        Destroy(GameObject.FindGameObjectWithTag("CmdCreater"));
    }
    public static float moveTimeByValue(float time)
    {
        time = time / (60 / register.meta.BPM / register.meta.P);
        time = MathF.Round(time);
        time = time * (60 / register.meta.BPM / register.meta.P);
        return time;
    }
    void Update()
    {
        if(loadingDone)progress.fillAmount = Audio.time / Audio.clip.length;
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            Instantiate(Saving, canvas);
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.O) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            Instantiate(loading, canvas);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null) {
            Instantiate(Settings, canvas);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            showNotesList();
        }
        else if(Input.GetKeyDown(KeyCode.N) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            if (GameObject.FindGameObjectWithTag("NoteCreater")!=null)
            {
                CreateNote tempObject= GameObject.FindGameObjectWithTag("NoteCreater").GetComponent<CreateNote>();
                tempObject.addNoteNow();
                tempObject.Start();
            }
            else
            {
                delCmdCreater();
                Instantiate(noteCreater,canvas);
            }
        }
        ///////////////
        if (false && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            showCmdsList();
        }
        else if (false&&Input.GetKeyDown(KeyCode.V) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            if (GameObject.FindGameObjectWithTag("CmdCreater") != null)
            {
                CreateCmd tempObject = GameObject.FindGameObjectWithTag("CmdCreater").GetComponent<CreateCmd>();
                tempObject.addCmdNow();
                tempObject.Start();
            }
            else
            {
                delNoteCreater();
                Instantiate(cmdCreater, canvas);
            }
        }
        //////////////
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Audio.time -= 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Audio.time += 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameObject.FindGameObjectWithTag("Settings") == null && GameObject.FindGameObjectWithTag("List") == null)
        {
            Audio.pitch = GetComponent<AudioSource>().pitch*-1+1;//Stop or Start
        }
        if (Audio.pitch == 0)
        {
            if (!field.isFocused)
            {
                Audio.time = Convert.ToSingle(field.text);
            }
        }
        else
        {
            field.text=Audio.time.ToString();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Application.Quit();
        }
    }
}
