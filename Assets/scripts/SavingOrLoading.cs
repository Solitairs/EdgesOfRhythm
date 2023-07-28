using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class SavingOrLoading : MonoBehaviour
{
    public TMP_InputField savePath;
    // Start is called before the first frame update
    public void SaveDataByJSON() //±£´æ
    {
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
        if (File.Exists(directory))
        {
            string json = File.ReadAllText(directory + ".eorsr");
            GameController.register = JsonUtility.FromJson<ERSRegister>(json);
            json = File.ReadAllText(directory + ".eorsc");
            GameController.cmd = JsonUtility.FromJson<ERSCommand>(json);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Destroy(gameObject);
        }
    }
}
