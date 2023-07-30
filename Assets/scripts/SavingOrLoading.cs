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
        GameController.register.SaveSort();
        GameController.cmd.SaveSort();
        Dictionary<int,int> ChangeID = new Dictionary<int,int>();
        for(int i=0;i< GameController.register.noteNum; i++)
        {
            int tid = GameController.register.notes[i].ID;
            GameController.register.notes[i].ID = i;
            ChangeID.Add(tid, i);
        }
        for (int i = 0; i < GameController.register.noteNum; i++)
        {
            if (ChangeID.ContainsKey(GameController.cmd.x01[i].id)) GameController.cmd.x01[i].id = ChangeID[GameController.cmd.x01[i].id];
            else Debug.LogError("Notes have no id:" + GameController.cmd.x01[i].id.ToString() + "  From Cmds.");
        }
        for (int i = 0; i < GameController.register.noteNum; i++)
        {
            if (ChangeID.ContainsKey(GameController.cmd.x02[i].id)) GameController.cmd.x02[i].id = ChangeID[GameController.cmd.x02[i].id];
            else Debug.LogError("Notes have no id:" + GameController.cmd.x02[i].id.ToString() + "  From Cmds.");
        }
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
