using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class ERSCommand
{
    [Serializable]
    public struct CommandIndex
    {
        public int type;
        public float time;
        public int number;
        public CommandIndex(int type, float time, int number)
        {
            this.type =type;
            this.time =time;
            this.number =number;
        }
    };
    [Serializable]
    public struct c01 //激活音符
    {
        public int id;
        public float startSpeed;
        public c01(int id, float startSpeed)
        {
            this.id = id;
            this.startSpeed = id;
        }
    };
    [Serializable]
    public struct c02 //更改音符速度
    {
        public int id;
        public float toSpeed;
        public c02(int id,float toSpeed)
        {
            this.id = id;
            this.toSpeed = toSpeed;
        }
    };
    [Serializable]
    public struct c03 //随时间更改背景颜色
    {
        public float[] color;
        public float duringTime;
        public c03(Color color, float duringTime)
        {
            this.color = new float[4];
            this.color[0] = color.r; color[1] = color.g; color[2] = color.b; color[3] = color.a;
            this.duringTime = duringTime;
        }
    };
    [Serializable]
    public struct c04 //随时间更改框架和轨道颜色
    {
        public float[] color;
        public float duringTime;
        public c04(Color color, float duringTime)
        {
            this.color = new float[4];
            this.color[0] = color.r; color[1] = color.g; color[2] = color.b; color[3] = color.a;
            this.duringTime = duringTime;
        }
    };
    [Serializable]
    public struct c05 //开启或关闭轨道响应
    {
        public bool oc;
        public c05(bool oc)
        {
            this.oc = oc;
        }
    };
    [Serializable]
    public struct c06 //闪烁轨道
    {
        public float duringTime;
        public c06(float duringTime)
        {
            this.duringTime = duringTime;
        }
    };
    [Serializable]
    public struct c07 //闪烁框架
    {
        public float duringTime;
        public c07(float duringTime)
        {
            this.duringTime = duringTime;
        }
    };
    ///////////////////////////////////////////////////////////////   Register and Function   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public CommandIndex[] cindex = new CommandIndex[10001]; public int cmdNum = 0;
    public c01[] x01 = new c01[4001]; public int n1 = 0;
    public c02[] x02 = new c02[4001]; public int n2 = 0;
    public c03[] x03 = new c03[4001]; public int n3 = 0;
    public c04[] x04 = new c04[4001]; public int n4 = 0;
    public c05[] x05 = new c05[4001]; public int n5 = 0;
    public c06[] x06 = new c06[4001]; public int n6 = 0;
    public c07[] x07 = new c07[4001]; public int n7 = 0;
    public void commandOut()
    {
        //throw error
    }
    public void throwE(string msg)
    {
        //throw error
    }
    public void delCmd(int id)
    {
        if (id >= cmdNum || id<0)
        {
            return;
        }
        id++;
        if (id == cmdNum)
        {
            cmdNum--;
            return;
        }
        for(;id< cmdNum; id++)
        {
            cindex[id - 1] = cindex[id];
        }//把之后的命令向前移一位以覆盖
        cmdNum--;
    }
    public void addx01(float time,c01 a01)
    {
        if(cmdNum > 10000 || n1 > 2000) commandOut();
        cindex[cmdNum]=new CommandIndex(1,time,n1);
        x01[n1]=a01;
        cmdNum++;
        n1++;
    }
    public void addx02(float time, c02 a02)
    {
        if (cmdNum > 10000 || n2 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(2, time, n2);
        x02[n2] = a02;
        cmdNum++;
        n2++;
    }
    public void addx03(float time, c03 a03)
    {
        if (cmdNum > 10000 || n3 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(3, time, n3);
        x03[n3] = a03;
        cmdNum++;
        n3++;
    }
    public void addx04(float time, c04 a04)
    {
        if (cmdNum > 10000 || n4 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(4, time, n4);
        x04[n4] = a04;
        cmdNum++;
        n4++;
    }
    public void addx05(float time, c05 a05)
    {
        if (cmdNum > 15000 || n5 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(4, time, n4);
        x05[n5] = a05;
        cmdNum++;
        n5++;
    }
    public void addx06(float time, c06 a06)
    {
        if (cmdNum > 10000 || n6 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(4, time, n4);
        x06[n6] = a06;
        cmdNum++;
        n6++;
    }
    public void addx07(float time, c07 a07)
    {
        if (cmdNum > 10000 || n7 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(4, time, n4);
        x07[n7] = a07;
        cmdNum++;
        n7++;
    }
}
