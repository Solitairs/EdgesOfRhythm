using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicController : MonoBehaviour
{
    public Slider musicProgress;
    public AudioSource AudioSource;
    public TextMeshProUGUI Text;

    public void ProgressChanged()//调整进度
    {
        if(AudioSource.clip != null)
        {
            AudioSource.time=musicProgress.value* AudioSource.clip.length;
        }
    }
    public void ControlPlay()//暂停
    {
        if (AudioSource.isPlaying)
        {
            AudioSource.Pause();
            AudioSource.time = Mathf.Round(AudioSource.time / (60F / SpectralController.spectralData.BPM / SpectralController.size)) * (60F / SpectralController.spectralData.BPM / SpectralController.size);
            GameObject[] Nobjects= GameObject.FindGameObjectsWithTag("Notes");
            for (int i = 0; i < Nobjects.Length; i++)  //遍历数组，输出物件的名称
            {
                Nobjects[i].GetComponent<Button>().enabled=true;
            }
        }
        else
        {
            AudioSource.Play();
            GameObject[] Nobjects = GameObject.FindGameObjectsWithTag("Notes");
            for (int i = 0; i < Nobjects.Length; i++)  //遍历数组，输出物件的名称
            {
                Nobjects[i].GetComponent<Button>().enabled = false;
            }
        }
    }
    public void ChooseMusicFile()//加载音乐文件
    {
        StartCoroutine(Load(OpenFile.ChooseMusicFile()));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//暂停
        {
            ControlPlay();
        }
        if (AudioSource.clip != null)
        {
            Text.text=AudioSource.time.ToString("#0.000") +"//"+ AudioSource.clip.length.ToString("#0.000");
            musicProgress.value = AudioSource.time / AudioSource.clip.length;
        }
    }
    IEnumerator Load(string path)
    {
        WWW ww = new WWW(path);
        yield return ww;
        if (ww.error == null && ww.isDone)
        {
            AudioSource.clip = ww.GetAudioClip();
            AudioSource.Play();
            GameObject[] Nobjects = GameObject.FindGameObjectsWithTag("Notes");
            for (int i = 0; i < Nobjects.Length; i++)  //遍历数组，输出物件的名称
            {
                Nobjects[i].GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            print(ww.error);
        }
    }
}
