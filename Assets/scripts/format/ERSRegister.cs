using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;
[Serializable]
public class ERSRegister
{
    public int noteNum = 0;
    [Serializable]
    public struct Meta
    {
        public string Name,Difficulty,SpectrumCreator,MusicCreator,BackgroundCreator;
        public float offset,BPM;
        public int P,PValue;
        public Meta(string Name,string Difficulty,string SpectrumCreator,string MusicCreator,string BackgroundCreator,float offset,float BPM,int P,int PValue)
        {
            this.Name = Name;
            this.Difficulty = Difficulty;
            this.SpectrumCreator = SpectrumCreator;
            this.MusicCreator = MusicCreator;
            this.BackgroundCreator = BackgroundCreator;
            this.offset = offset;
            this.BPM = BPM;
            this.P = P;
            this.PValue = PValue;
        }
    };
    [Serializable]
    public struct Notes
    {
        public int rotateType, hurtType, deterRoad, Type, ID;
        public float deterTime,startSpeed;
        public Notes(int ID, int Type, int rotateType, int hurtType,int deterRoad,float startSpeed, float deterTime)
        {
            this.ID = ID;
            this.Type = Type;
            this.rotateType = rotateType;
            this.hurtType = hurtType;
            this.deterRoad = deterRoad;
            this.startSpeed = startSpeed;
            this.deterTime = deterTime;
        }
    }
    public Meta meta;
    public Notes[] notes=new Notes[4001];
    public void Intialize()
    {
        noteNum = 0;
        notes = new Notes[4001];
        meta = new Meta("null","?","nobody","nobody","nobody",0,60,8,4);
}
    public void addNote(Notes note)
    {
        if (noteNum > 4000) return;//超限
        notes[noteNum] = note;
        noteNum++;
    }
    public void delNote(int id)
    {
        if(noteNum<=0||id>=noteNum) return;
        id++;
        if (id == noteNum)
        {
            noteNum--;
            return;
        }
        for (; id < noteNum; id++)
        {
            notes[id - 1] = notes[id];
        }
        noteNum--;
    }
    public class ECmp : IComparer
    {
        public int Compare(object x, object y)//降序
        {
            Notes a = (Notes)(x);
            Notes b = (Notes)(y);//然后直接强转（很蠢）
            if (a.ID > b.ID) return -1;
            else if (a.ID < b.ID) return 1;//大于0为x大于y
            else return 0;//等于0为x等于y
        }
    }
    public class SCmp : IComparer
    {
        public int Compare(object x, object y)//升序
        {
            Notes a = (Notes)(x);
            Notes b = (Notes)(y);//然后直接强转（很蠢）
            if (a.ID < b.ID) return -1;
            else if (a.ID > b.ID) return 1;//大于0为x大于y
            else return 0;//等于0为x等于y
        }
    }
    public void sort()//ID降序
    {
        IComparer Cmp = new ECmp();
        Array.Sort(notes, 0, noteNum, Cmp);
    }
    public void SaveSort()//ID升序
    {
        IComparer Cmp = new ECmp();
        Array.Sort(notes, 0, noteNum, Cmp);
    }
}
