﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace asyncAwait
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int CountCharcters()
        {

            int count = 0;
            using(StreamReader reader=new StreamReader("C:\\Data.txt"))
            {
                string content = reader.ReadToEnd();
                count=content.Length;
                Thread.Sleep(5000);

            }
            return count;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Task<int> task = new Task<int>(CountCharcters);
            task.Start();
            lblCount.Text = "processing File. please wait....";
            int count = await task;
            lblCount.Text = count.ToString() + " characters in file";

        }
    }
}
