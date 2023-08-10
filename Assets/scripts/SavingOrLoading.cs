using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class SavingOrLoading : MonoBehaviour
{
    public TMP_InputField savePath;
    // Start is called before the first frame update
    public void SaveDataByJSON() //保存
    {
        //优化信息
        Dictionary<int,int> ChangeID = new Dictionary<int,int>();
        for(int i=0;i< GameController.register.noteNum; i++)
        {
            int tid = GameController.register.notes[i].ID;
            GameController.register.notes[i].ID = i;
            ChangeID.Add(tid, i);
        }
        for (int i = 0; i < GameController.cmd.cmdNum; i++)
        {
            if (GameController.cmd.cindex[i].type == 1)
            {
                if (ChangeID.ContainsKey(GameController.cmd.Getx01(i).id)) GameController.cmd.Getx01(i).id = ChangeID[GameController.cmd.Getx01(i).id];
                else
                {
                    Debug.Log("Notes have no id:" + GameController.cmd.Getx01(i).id.ToString() + "  From Cmds-1.");
                    GameController.cmd.delCmd(i);
                }
            }
            if(GameController.cmd.cindex[i].type == 2)
            {
                if (ChangeID.ContainsKey(GameController.cmd.Getx02(i).id)) GameController.cmd.Getx02(i).id = ChangeID[GameController.cmd.Getx02(i).id];
                else
                {
                    Debug.Log("Notes have no id:" + GameController.cmd.Getx01(i).id.ToString() + "  From Cmds-2.");
                    GameController.cmd.delCmd(i);
                }
            }
        }
        GameController.register.SaveSort();
        GameController.cmd.SaveSort();
        //开始序列化
        string directory = savePath.text + "\\";
        if (Directory.Exists(directory) == false)
        {
            Directory.CreateDirectory(directory);
        }
        string json = JsonUtility.ToJson(GameController.register);
        StreamWriter sw = new StreamWriter(directory + GameController.register.meta.Name + ".eorsr");
        sw.Write(json);
        sw.Close();
        json = JsonUtility.ToJson(GameController.cmd);
        sw = new StreamWriter(directory + GameController.register.meta.Name + ".eorsc");
        sw.Write(json);
        sw.Close();
        Destroy(gameObject);
    }
    public void LoadDataByJSON()
    {
        string directory = savePath.text;
        string json = File.ReadAllText(directory + ".eorsr");
        GameController.register = JsonUtility.FromJson<ERSRegister>(json);
        json = File.ReadAllText(directory + ".eorsc");
        GameController.cmd = JsonUtility.FromJson<ERSCommand>(json);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Destroy(gameObject);
        }
    }
}
