using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 鼠标控制
{
    public partial class Form1 : Form
    {
        Queue<Tuple<string, string>> queue = new Queue<Tuple<string, string>>();
        KeyboardHook k_hook;
        bool flag = true;
        int f = 0;
        int j = 0;
        Point p;
       // long start = DateTime.Now.m
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        [DllImport("user32.dll")]
        private static extern int mouse_event(int duFlags, int dx, int dy, int cButtons, int duExtraInfo);

        const int MOUSEEVENTF_MOVE = 0x0001;  //移动鼠标

        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTUP = 0x0004;// 模拟鼠标左键抬起
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;// 模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;// 模拟鼠标中键按下
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标 


        public Form1()
        {
           InitializeComponent();
            k_hook = new KeyboardHook();
            //k_hook.KeyDownEvent += new System.Windows.Forms.KeyEventHandler(hook_KeyDown);//钩住键按下 
            k_hook.KeyPressEvent += K_hook_KeyPressEvent;
            k_hook.Start();//安装键盘钩子
        }


        private void GrabTicket() {
            f = 0;
            Thread.Sleep(2000);
            while (f == 0)
            {
                SetCursorPos(Convert.ToInt32(this.textBox1.Text), Convert.ToInt32(this.textBox2.Text));
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Thread.Sleep(20);
                SetCursorPos(Convert.ToInt32(this.textBox3.Text), Convert.ToInt32(this.textBox4.Text));
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Thread.Sleep(20);
            }
        }

        /// <summary>
        /// 获取鼠标坐标
        /// </summary>
        /// <returns></returns>
        private Tuple<string, string> GetXY() {
            Point point = new Point();
            string x = point.X.ToString();
            string y = point.Y.ToString();
            Tuple<string, string> xy = new Tuple<string, string>(x,y);
            return xy;

        }

        private void MoveMouse(int x,int y) {
            SetCursorPos(x,y);

        }
        private void Mouse_Click() {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关闭该窗口后3秒开始抢。开始后按任意数字键结束");
            //Thread.Sleep(5000);
            flag = true;
           // this.button1.Visible = false;
            Thread thread = new Thread(GrabTicket);
           // threadList.Add(thread);//将当前线程添加到线程列表

            thread.IsBackground = true; //置为后台线程
            thread.Start();
            
            
        }

        private void Form1_Load(object sender, MouseEventArgs e)
        {
            Point point = new Point();
            string x = point.X.ToString();
            string y = point.Y.ToString();
            this.textBox1.Text = x;
            this.textBox2.Text = y;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Thread thread = new Thread(GetXY);
            MessageBox.Show("1秒钟后采集第一个坐标");
            Thread.Sleep(2000);
            GetCursorPos(out p);
            int x = p.X;
            int y = p.Y;
            this.textBox1.Text = x.ToString();
            this.textBox2.Text = y.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1秒钟后采集第二个坐标");
            Thread.Sleep(3000);
            GetCursorPos(out p);
            int x = p.X;
            int y = p.Y;
            this.textBox3.Text = x.ToString();
            this.textBox4.Text = y.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关闭该窗口后结束");
            flag = false;
            this.button1.Visible = true;
        }

        private void K_hook_KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            //tb1.Text += e.KeyChar;
            f = (int)e.KeyChar;
           // System.Windows.Forms.MessageBox.Show(f.ToString());
        }

        private void 次每秒_TextChanged(object sender, EventArgs e)
        {

        }


        int count = 0;
        int b = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = Convert.ToString( ++count);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox7.Text = Convert.ToString(++b);
        }
    }
}
