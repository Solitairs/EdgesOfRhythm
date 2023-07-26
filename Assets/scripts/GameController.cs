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
S���浱ǰָ�������
N��������
���Ҽ�ͷ��ʱ��1.5s
�ո���ͣ�򲥷�
 */
public class GameController : MonoBehaviour
{
    public TMP_InputField field;
    public UnityEngine.Object noteCreater;
    public Transform canvas;
    public UnityEngine.Object NotesList,CmdsList;
    public ERSRegister register;
    public ERSCommand cmd;
    private AudioSource Audio;
    void Start()
    {
        register = new ERSRegister();
        cmd = new ERSCommand();
        for(int i = 0; i < 2; i++)
        {
            Debug.Log(i);
        }
        Audio = GetComponent<AudioSource>();
        //��������
        string path;
        string local;
        //��ȡ������Assets·��
        path = Application.dataPath;
        //��ȡ��Assetsͬ���ļ���
        int n = path.LastIndexOf("/");
        path = path.Substring(0, n);
        path += "/Music";
        //����Music��һ����Assetsͬ�����ļ��У�����������֣�,local:�������ƣ�.wav�����ֺ�׺
        bool finded = false;
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*");
            for (int i = 0; i < files.Length; i++)
            {
                //���Թ����ļ�
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
            //ʹ��www����ز���
            StartCoroutine(Load(path));
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
    private bool SaveAgain = false;
    public void showNotesList()
    {
        Instantiate(NotesList, canvas);
    }
    public void addNoteNow()
    {
        CreateNote tempObject = GameObject.FindGameObjectWithTag("NoteCreater").GetComponent<CreateNote>();
        if (SaveAgain) register.delNote(register.noteNum - 1);
        register.addNote(new ERSRegister.Notes(tempObject.Type.value, tempObject.rotateType.value, tempObject.hurtType.value, tempObject.deterRoad.value, Convert.ToSingle(tempObject.StartSpeed.text), Convert.ToSingle(tempObject.deterTime.text)));
        SaveAgain = true;
    }
    private void delNoteCreater()
    {
        SaveAgain = false;
        Destroy(GameObject.FindGameObjectWithTag("NoteCreater"));
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
        {
            if(GameObject.FindGameObjectWithTag("List")==null)showNotesList();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            if (GameObject.FindGameObjectWithTag("NoteCreater") != null) addNoteNow();
        }
        if(Input.GetKeyDown(KeyCode.Z))
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
                //��Ҫɾ�����(������)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Audio.pitch = GetComponent<AudioSource>().pitch*-1+1;//Stop or play
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
