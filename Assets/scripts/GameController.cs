using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
/*
^N查看音符列表
AltS保存当前指令或音符
AltO打开
AltI保存
N创建音符
左右箭头跳时间1.5s
空格暂停或播放
 */
public class GameController : MonoBehaviour
{
    public static int[] Pl;
    public TMP_InputField field;
    public UnityEngine.Object noteCreater,NotesList,CmdsList,Settings,Saving,loading;
    public Transform canvas;
    public static ERSRegister register;
    public static ERSCommand cmd;
    private AudioSource Audio;
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
        Audio = GetComponent<AudioSource>();
        //加载音乐
        string path;
        string local;
        //获取本工程Assets路径
        path = Application.dataPath;
        //获取与Assets同级文件夹
        int n = path.LastIndexOf("/");
        path = path.Substring(0, n);
        path += "/Music";
        //这里Music是一个与Assets同级的文件夹（用来存放音乐）,local:音乐名称，.wav：音乐后缀
        bool finded = false;
        string name = "";
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
                    name = files[i].Name;
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
            }
            else
            {
                print(ww.error);
            }
        }
    }
    public static bool SaveAgain = false;
    public void showNotesList()
    {
        Instantiate(NotesList, canvas);
    }
    private void delNoteCreater()
    {
        SaveAgain = false;
        Destroy(GameObject.FindGameObjectWithTag("NoteCreater"));
    }
    void Update()
    {
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
            SaveAgain = false;
            if (GameObject.FindGameObjectWithTag("NoteCreater")!=null)
            {
                CreateNote tempObject= GameObject.FindGameObjectWithTag("NoteCreater").GetComponent<CreateNote>();
                register.addNote(new ERSRegister.Notes(tempObject.Type.value, tempObject.rotateType.value, tempObject.hurtType.value, tempObject.deterRoad.value, Convert.ToSingle(tempObject.StartSpeed.text), Convert.ToSingle(tempObject.deterTime.text)));
                delNoteCreater();
                Instantiate(noteCreater,canvas);
            }
            else
            {
                //还要删掉别的(先跳过)
                Instantiate(noteCreater,canvas);
            }
        }
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
    }
}
