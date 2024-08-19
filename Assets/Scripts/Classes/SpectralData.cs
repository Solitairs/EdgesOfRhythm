using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpectralData
{
    public void setDeciders(int deciderNum,GameObject DeciderPool)
    {
        this.deciderNum = deciderNum;
        deciders=new DeciderData[deciderNum];
        SpectralController.Deciders=new DeciderController[deciderNum];
        for (int i = 0; i < deciderNum; i++)
        {
            deciders[i] = new DeciderData(-1920F + 3840 / (deciderNum + 1) * (i + 1), -840);
            SpectralController.Deciders[i]=DeciderPool.transform.GetChild(i).GetComponent<DeciderController>();
            SpectralController.Deciders[i].id = i;
        }

    }
    public class cmpNote : IComparer //sort notes
    {
        public int Compare(object x, object y) 
        {
            NotesData a = (NotesData)(x);
            NotesData b = (NotesData)(y);
            if (a.activeTime < b.activeTime) return -1; 
            else if (a.activeTime > b.activeTime) return 1; 
            else return 0; 
        }
    }
    public struct Cpos //ax^2+bx+c+dcos(ex+f)+gsin(hx+i)  translate value
    { 
        public float a, b, c, d, e, f, g, h, i;
        public Cpos(float a, float b, float c, float d, float e, float f, float g, float h, float i)
        {
            this.a = a; this.b = b; this.c = c; this.d = d; this.e = e; this.f = f; this.g = g; this.h = h; this.i = i;
        }
        public Cpos(float c)
        {
            a = 0; b = 0;
            this.c = c;
            b = 0;d = 0; e = 0;f = 0;g = 0;h = 0;i = 0;
        }
        public float TPos(float time)
        {
            return a*time*time+b*time+c+ d * (float)Math.Cos(time / 180 * 3.1415926 * e + f) + g * (float)Math.Sin(time / 180 * 3.1415926 * h + i);
        }
    }
    public int noteNum = 0,BPM=60;
    public int deciderNum=9;
    public NotesData[] notes=new NotesData[0];
    public DeciderData[] deciders=new DeciderData[9];
    public void sortNotes()
    {
        if (noteNum < 2) return;
        IComparer Cmp = new cmpNote();
        Array.Sort(notes, 0, noteNum-1, Cmp);
        for (int i = 0; i < noteNum; i++)
        {
            notes[i].sortCmds();
        }
    }
    public void sortDeciders()//对判定线进行排序
    {
        for (int i = 0; i < deciderNum; i++)
        {
            deciders[i].sortCmds();
        }
    }
    public void addNote(float activeTime, float deterTime, int deterRoad, Cpos x, Cpos y, int type, float length=0)//新增音符
    {
        noteNum++;
        NotesData[] temp = notes;
        notes = new NotesData[noteNum];
        for (int i = 0; i < noteNum - 1; i++)
        {
            notes[i] = temp[i];
        }
        notes[noteNum - 1] = new NotesData(activeTime, deterTime, deterRoad, x, y, type, length);
    }
    public void delNote(int ID) //true index
    {
        noteNum--;
        NotesData[] temp = notes;
        for (int i = ID; i < noteNum; i++)
        {
            temp[i] = notes[i + 1];
        }
        notes = new NotesData[noteNum];
        for (int i = 0; i < noteNum - 1; i++)
        {
            notes[i] = temp[i];
        }
    }

    BinaryReader reader;
    BinaryWriter writer;
    private int readint()
    {
        return reader.ReadInt32();
    }
    private float readfloat()
    {
        return reader.ReadSingle();
    }
    private Cpos readCPos()
    {
        Cpos x = new Cpos(readfloat(), readfloat(), readfloat(), readfloat(), readfloat(), readfloat(), readfloat(), readfloat(), readfloat());
        return x;
    }
    private void writeCPos(Cpos x)
    {
        writer.Write(x.a);
        writer.Write(x.b);
        writer.Write(x.c);
        writer.Write(x.d);
        writer.Write(x.e);
        writer.Write(x.f);
        writer.Write(x.g);
        writer.Write(x.h);
        writer.Write(x.i);
    }
    public bool Load(string notePath, string deciderPath, string spectralPath)
    {
        //read basic values
        reader = new BinaryReader(new FileStream(spectralPath,FileMode.Open));
        noteNum=readint();
        deciderNum = readint();
        BPM = readint();
        reader.Close();

        //read notes
        reader = new BinaryReader(new FileStream(notePath, FileMode.Open));
        notes = new NotesData[noteNum];
        for(int i = 0; i < noteNum; i++)
        {
            notes[i].activeTime = readfloat();
            notes[i].deterTime = readfloat();
            notes[i].deterRoad = readint();
            notes[i].x = readCPos();
            notes[i].y = readCPos();
            notes[i].k = readfloat();
            notes[i].b = readfloat();
            notes[i].type = readint();
            if(notes[i].type==2) notes[i].length = readfloat();
            else notes[i].length = 0;
            int cmdNum= readint();
            notes[i].cmds = new NotesData.Cmd[0];
            notes[i].cmdNum = 0;
            for(int j = 0; j < cmdNum; j++)
            {
                notes[i].addCmd(readfloat(), readCPos(), readCPos(), readfloat(), readfloat(), readfloat(), readfloat(), readfloat());
            }
        }
        reader.Close();

        //read deciders
        reader = new BinaryReader(new FileStream(deciderPath, FileMode.Open));
        deciders=new DeciderData[deciderNum];
        for (int i = 0; i < deciderNum; i++)
        {
            deciders[i].x = readCPos();
            deciders[i].y = readCPos();
            deciders[i].k = readfloat();
            deciders[i].b = readfloat();
            deciders[i].Ak = readfloat();
            deciders[i].Ab = readfloat();
            int cmdNum = readint();
            deciders[i].cmds = new DeciderData.Cmd[0];
            deciders[i].cmdNum = 0;
            for (int j = 0; j < cmdNum; j++)
            {
                deciders[i].addCmd(readfloat(), readCPos(), readCPos(), readfloat(), readfloat(), readfloat(), readfloat());
            }
        }
        reader.Close();

        return true;
    }
    public bool Save(string notePath, string deciderPath, string spectralPath)
    {
        sortDeciders();
        sortNotes();
        writer = new BinaryWriter(new FileStream(spectralPath,FileMode.Create));
        writer.Write(noteNum);
        writer.Write(deciderNum);
        writer.Write(BPM);
        writer.Close();

        writer = new BinaryWriter(new FileStream(notePath, FileMode.Create));
        for (int i = 0; i < noteNum; i++)
        {
            writer.Write(notes[i].activeTime);
            writer.Write(notes[i].deterTime);
            writer.Write(notes[i].deterRoad);
            writeCPos(notes[i].x);
            writeCPos(notes[i].y);
            writer.Write(notes[i].k);
            writer.Write(notes[i].b);
            writer.Write(notes[i].type);
            if (notes[i].type == 2) writer.Write(notes[i].length);
            writer.Write(notes[i].cmdNum);
            for (int j = 0; j < notes[i].cmdNum; j++)
            {
                writer.Write(notes[i].cmds[j].time);
                writeCPos(notes[i].cmds[j].x);
                writeCPos(notes[i].cmds[j].y);
                writer.Write(notes[i].cmds[j].k);
                writer.Write(notes[i].cmds[j].b);
                writer.Write(notes[i].cmds[j].r);
                writer.Write(notes[i].cmds[j].g);
                writer.Write(notes[i].cmds[j].l);
            }
        }
        writer.Close();

        writer = new BinaryWriter(new FileStream(deciderPath, FileMode.Create));
        for (int i = 0; i < noteNum; i++)
        {
            writeCPos(deciders[i].x);
            writeCPos(deciders[i].y);
            writer.Write(deciders[i].k);
            writer.Write(deciders[i].b);
            writer.Write(deciders[i].Ak);
            writer.Write(deciders[i].Ab);
            writer.Write(deciders[i].cmdNum);
            for (int j = 0; j < deciders[i].cmdNum; j++)
            {
                writer.Write(deciders[i].cmds[j].time);
                writeCPos(deciders[i].cmds[j].x);
                writeCPos(deciders[i].cmds[j].y);
                writer.Write(deciders[i].cmds[j].k);
                writer.Write(deciders[i].cmds[j].b);
                writer.Write(deciders[i].cmds[j].Ak);
                writer.Write(deciders[i].cmds[j].Ab);
            }
        }
        writer.Close();

        return true;
    }
}
