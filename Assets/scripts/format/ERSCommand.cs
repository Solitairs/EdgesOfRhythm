using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ERSCommand : MonoBehaviour
{
    public struct CommandIndex
    {
        public int type;
        public float time;
        public int number;
        public CommandIndex(int ntype, float ntime, int nnumber)
        {
            type=ntype;
            time=ntime;
            number=nnumber;
        }
    };
    public struct c01 //激活音符
    {
        public int id;
        public float startSpeed;
        public c01(int vid,float vstartSpeed)
        {
            id = vid;
            startSpeed = vid;
        }
    }
    public struct c02 //更改音符速度
    {
        public int id;
        public float toSpeed;
        public c02(int vid,float vtoSpeed)
        {
            id = vid;
            toSpeed = vtoSpeed;
        }
    }
    public struct c03 //随时间更改背景颜色
    {
        public Color color;
        public float duringTime;
        public c03(Color vcolor, float vduringTime)
        {
            color = vcolor;
            duringTime = vduringTime;
        }
    }
    public struct c04 //更改背景颜色
    {
        public Color color;
        public c04(Color vcolor)
        {
            color = vcolor;
        }
    }
    public struct c05 //载入雨水特效
    {
        public Color color;
        public float gravity;
        public c05(Color vcolor,float vgravity)
        {
            color=vcolor;
            gravity = vgravity;
        }
    }
    //x06 关闭雨水特效
    public struct c07 //旋转谱面
    {
        public int id;
        public float toAngle;
        public float duringTime;
        public c07(int nid,float ntoAngle,float nduringTime)
        {
            id = nid;
            toAngle = ntoAngle;
            duringTime = nduringTime;
        }
    }
    public struct c08 //旋转镜头
    {
        public float toAngle;
        public float duringTime;
        public c08(float ntoAngle,float nduringTime)
        {
            toAngle = ntoAngle;
            duringTime = nduringTime;
        }
    }
    //x09 增加一个eyes特效
    //x10 眨
    //x11 删除eyes特效
    //剩下的下次再做
    ///////////////////////////////////////////////////////////////   Register and Function   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    CommandIndex[] cindex = new CommandIndex[10001]; int cmdNum = 0;
    c01[] x01 = new c01[2001]; int n1 = 0;
    c02[] x02 = new c02[2001]; int n2 = 0;
    c03[] x03 = new c03[2001]; int n3 = 0;
    c04[] x04 = new c04[2001]; int n4 = 0;
    c05[] x05 = new c05[2001]; int n5 = 0;
    c07[] x07 = new c07[2001]; int n7 = 0;
    public void commandOut()
    {

    }
    public void throwE(string msg)
    {

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
        if (cmdNum > 10000 || n5 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(5, time, n5);
        x05[n5] = a05;
        cmdNum++;
        n5++;
    }
    public void addx07(float time, c07 a07)
    {
        if (cmdNum > 10000 || n7 > 2000) commandOut();
        cindex[cmdNum] = new CommandIndex(7, time, n7);
        x07[n7] = a07;
        cmdNum++;
        n7++;
    }
}
