using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ERSRegister : MonoBehaviour
{
    public int noteNum = 0;
    public struct Notes
    {
        public int angleType, hurtType, deterRoad, Type;
        public float deterTime,startSpeed;
        public Notes(int ntype, int nangleType,int nhurtType,int ndeterRoad,float nspeed,float ndeterTime)
        {
            Type = ntype;
            angleType = nangleType;
            hurtType = nhurtType;
            deterRoad = ndeterRoad;
            startSpeed = nspeed;
            deterTime = ndeterTime;
        }
    }
    public Notes[] notes=new Notes[2001];
    public void addNote(Notes note)
    {
        if (noteNum > 2000) return;//超限
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
