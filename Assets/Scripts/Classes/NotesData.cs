using System.Collections;
using System.Collections.Generic;
using System;
public class NotesData
{
    //initial value
    public float deterTime, activeTime, k, b, length;
    public int cmdNum = 0, deterRoad, type;//0-8 type:0tap 1stick 2hold
    public SpectralData.Cpos x, y;
    public Cmd[] cmds;
    public class cmp : IComparer //sort notes
    {
        public int Compare(object x, object y)
        {
            Cmd a = (Cmd)(x);
            Cmd b = (Cmd)(y);
            if (a.time < b.time) return -1;
            else if (a.time > b.time) return 1;
            else return 0;
        }
    }
    public void sortCmds()
    {
        if (cmdNum < 2) return;
        IComparer Cmp = new cmp();
        Array.Sort(cmds, 0, cmdNum - 1, Cmp);
    }
    public NotesData(float activeTime,float deterTime,int deterRoad, SpectralData.Cpos x, SpectralData.Cpos y, int type, float length)
    {
        cmds = new Cmd[0];
        cmdNum = 0;
        this.type = type;
        this.activeTime = activeTime;
        this.deterTime = deterTime;
        this.deterRoad = deterRoad;
        this.length = length;
        k = 0;
        b = 0;
        this.x = x;
        this.y = y;
    }
    public struct Cmd
    {
        public float time; //call time
        public SpectralData.Cpos x, y; //translate value
        public float k, b, r, g, l; //rotation value:kx+h
        public Cmd(float time, SpectralData.Cpos x, SpectralData.Cpos y, float k, float b, float r, float g, float l)
        {
            this.time = time; this.x = x; this.y = y;
            this.k = k; this.b = b; this.r = r; this.g = g; this.l = l;
        }
    }
    public void addCmd(float time, SpectralData.Cpos x, SpectralData.Cpos y, float k, float b, float r, float g, float l)
    {
        cmdNum++;
        Cmd[] temp = cmds;
        cmds = new Cmd[cmdNum];
        for(int i = 0; i < cmdNum-1; i++)
        {
            cmds[i] = temp[i];
        }
        cmds[cmdNum-1]=new Cmd(time, x, y, k, b, r, g, l);
    }
    public void delCmd(int ID) //true index
    {
        cmdNum--;
        Cmd[] temp = cmds;
        for (int i = ID; i < cmdNum; i++)
        {
            temp[i] = cmds[i + 1];
        }
        cmds = new Cmd[cmdNum];
        for (int i = 0; i < cmdNum-1; i++)
        {
            cmds[i] = temp[i];
        }
    }
}
