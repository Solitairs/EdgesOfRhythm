using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ERSRegister : MonoBehaviour
{
    public int noteNum = 0;
    public static int[] P;
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
    public struct Notes
    {
        public int rotateType, hurtType, deterRoad, Type;
        public float deterTime,startSpeed;
        public Notes(int type, int rotateType, int hurtType,int deterRoad,float startSpeed, float deterTime)
        {
            this.Type = type;
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
        P = new int[9];
        P[0] = 2;
        P[1] = 3;
        P[2] = 4;
        P[3] = 6;
        P[4] = 8;
        P[5] = 12;
        P[6] = 16;
        P[7] = 24;
        P[8] = 32;
        noteNum = 0;
        notes = new Notes[4001];
        meta = new Meta("nobody","?","nobody","nobody","nobody",0,60,8,4);
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
    public class cmp : IComparer
    {
        public int Compare(object x, object y)
        {
            Notes a = (Notes)(x);
            Notes b = (Notes)(y);//然后直接强转（很蠢）
            if (a.deterTime < b.deterTime) return -1;
            else if (a.deterTime > b.deterTime) return 1;//大于0为x大于y
            else return 0;//等于0为x等于y
        }
    }
    public void sort()
    {
        IComparer Cmp = new cmp();
        Array.Sort(notes, 0, noteNum, Cmp);
    }
    public bool Save()
    {
        sort();
        return true;
    }
}
