using System.Collections;
using System.Collections.Generic;
using System;
public class DeciderData
{
    public SpectralData.Cpos x, y;
    public float k, b, Ak, Ab; //initial value kb:rotation AkAb:Alpha
    public int cmdNum = 0;
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
    public DeciderData(float x,float y)
    {
        cmds = new Cmd[0];
        cmdNum = 0;
        k = 0;
        b = 0;
        Ak = 0;
        Ab = 1;
        this.x = new SpectralData.Cpos(x);
        this.y = new SpectralData.Cpos(y);
    }
    public struct Cmd
    {
        public float time; //call time
        public SpectralData.Cpos x, y; //translate value
        public float k, b, Ab, Ak; //rotation value:kx+h ColorAlpha=AkX+Ab
        public Cmd(float time, SpectralData.Cpos x, SpectralData.Cpos y, float k, float b, float Ak, float Ab)
        {
            this.time = time; this.x = x; this.y = y;
            this.k = k; this.b = b;
            this.Ab = Ab; this.Ak = Ak;
        }
    }
    public void addCmd(float time, SpectralData.Cpos x, SpectralData.Cpos y, float k, float b, float Ak, float Ab)
    {
        cmdNum++;
        Cmd[] temp = cmds;
        cmds = new Cmd[cmdNum];
        for (int i = 0; i < cmdNum - 1; i++)
        {
            cmds[i] = temp[i];
        }
        cmds[cmdNum - 1] = new Cmd(time, x, y, k, b, Ak, Ab);
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
        for (int i = 0; i < cmdNum - 1; i++)
        {
            cmds[i] = temp[i];
        }
    }
}
