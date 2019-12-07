using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
namespace Sly2_HACK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Mem m = new Mem();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int procID = m.getProcIDFromName("pcsx2");
                bool openProc = false;

                if (procID > 0)
                {
                    openProc = m.OpenProcess(procID);
                    string GameID = m.readString("0x202ADB13","",10);
                    if (GameID != "SCES-52529")
                    {
                        openProc = false;
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "GAME NOT FOUND";
                        });
                    }
                }
                else
                {
                    label4.Invoke((MethodInvoker)delegate
                    {
                        label4.Text = "GAME NOT FOUND";
                    });

                }
                if (openProc){
                    trackBar3.Invoke((MethodInvoker)delegate
                    {
                        float RenderDistance = trackBar3.Value;
                        RenderDistance = RenderDistance / 100;
                        label6.Text = RenderDistance.ToString();
                        m.writeMemory("0x202E535C", "float", RenderDistance.ToString());
                    });
                    trackBar1.Invoke((MethodInvoker)delegate
                    {
                        float clock = trackBar1.Value;
                        clock = clock / 100;
                        label13.Text = clock.ToString();
                        m.writeMemory("0x202E52F4", "float", clock.ToString());
                    });
                    trackBar2.Invoke((MethodInvoker)delegate
                    {
                        float jumpHeight = trackBar2.Value;
                        label14.Text = jumpHeight.ToString();
                        if(jumpHeight != 215)
                        {
                            m.writeMemory("0x202C69B8", "float", jumpHeight.ToString());
                        }
                        else
                        {
                            m.writeMemory("0x202C69B8", "float", "215");
                            m.writeMemory("0x202C69BC", "float", "315");
                        }

                    });
                    int CharID = m.readByte("0x203DC26C");
                    if (CharID == 7)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label9.Text = "Sly";
                        });
                    }
                    else if(CharID == 8)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label9.Text = "Bentley";
                        });
                    }
                    else if (CharID == 9)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label9.Text = "Murray";
                        });
                    }

                    if (checkBox1.Checked)
                    {
                        int bottles = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
                        m.writeMemory("0x203E93F4", "int", bottles.ToString());

                    }
                    if (checkBox2.Checked)
                    {
                        int money = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
                        m.writeMemory("0x203DC300", "int", money.ToString());

                    }
                    if (checkBox3.Checked)
                    {
                        m.writeMemory("0x203DC2B0", "int", "40");
                        m.writeMemory("0x203DC2E0", "int", "60");
                        m.writeMemory("0x203DC2C8", "int", "40");
                    }
                    if (checkBox4.Checked)
                    {
                        m.writeMemory("0x203DC2B4", "int", "100");
                        m.writeMemory("0x203DC2E4", "int", "100");
                        m.writeMemory("0x203DC2CC", "int", "100");
                    }
                    //4294967295 MegaJump
                    //203DC260 Episode ID
                    //203DC26C Active Char

                    int LevelID = m.readByte("0x203DC260");
                    if (LevelID == 0)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Menu";
                        });

                    }
                    else if (LevelID == 1)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Paris";
                        });

                    }
                    else if (LevelID == 2)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Indian Palace";
                        });

                    }
                    else if (LevelID == 3)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Indian Jungle";
                        });

                    }
                    else if (LevelID == 4)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Prison";
                        });

                    }
                    else if (LevelID == 5)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Neyla vs The Contessa";
                        });

                    }
                    else if (LevelID == 6)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Canada Hub";
                        });

                    }
                    else if (LevelID == 7)
                    {
                        label4.Invoke((MethodInvoker)delegate
                        {
                            label4.Text = "Canada Serration";
                        });

                    }
                    if (checkBox5.Checked)
                    {
                        if (LevelID == 1)
                        {
                            // 20FBAF28 Sly Inf Jump
                            // 211EE3F8 Murray Inf Jump
                            // 20D8A7A8
                            m.writeMemory("0x20FBAF28", "int", "1");//Sly
                            m.writeMemory("0x211EE3F8", "int", "1");//Murray
                            m.writeMemory("0x20D8A7A8", "int", "1");//Bentley

                        }
                        else if (LevelID == 2) // Rajan Part 1
                        {
                            m.writeMemory("0x20D08878", "int", "1");//Sly
                            m.writeMemory("0x20EC4118", "int", "1");//Murray
                            m.writeMemory("0x20BB80F8", "int", "1");//Bentley
                        }
                        else if (LevelID == 3)// Rajan Part 2
                        {
                            m.writeMemory("0x20D303C8", "int", "1");//Sly
                            m.writeMemory("0x20F831C8", "int", "1");//Murray
                            m.writeMemory("0x20C34C98", "int", "1");//Bentley
                        }
                        else if (LevelID == 4) // Prison
                        {
                            m.writeMemory("0x2117A6D8", "int", "1");//Sly
                            //m.writeMemory("0x20F831C8", "int", "1");//Murray Is in Jail
                            m.writeMemory("0x2103B528", "int", "1");//Bentley
                        }
                        else if (LevelID == 5) // Neyla War
                        {
                            m.writeMemory("0x208BDD88", "int", "1");//Sly
                            m.writeMemory("0x20B72408", "int", "1");//Murray
                            m.writeMemory("0x207A74F8", "int", "1");//Bentley
                        }
                        else if (LevelID == 6)//CANADA
                        {
                            m.writeMemory("0x20AE44B8", "int", "1");//Sly
                            m.writeMemory("0x20D653A8", "int", "1");//Murray
                            m.writeMemory("0x20825018", "int", "1");//Bentley
                        }
                        else if (LevelID == 7)//LumberMill
                        {
                            m.writeMemory("0x210EDD18", "int", "1");//Sly
                            m.writeMemory("0x212B51F8", "int", "1");//Murray
                            m.writeMemory("0x20FFE918", "int", "1");//Bentley
                        }
                        else if (LevelID == 8)//Blimp
                        {
                            
                            m.writeMemory("0x20B45B98", "int", "1");//Sly
                            m.writeMemory("0x20D0F978", "int", "1");//Murray
                            m.writeMemory("0x2086D268", "int", "1");//Bentley
                        }
                    }
                    if (checkBox6.Checked)
                    {
                        m.writeMemory("0x203DC2F8", "int", "-1");
                        m.writeMemory("0x203DC2FC", "int", "-1");
                    }
                    if (checkBox11.Checked)
                    {
                        m.writeMemory("0x203E8A14", "int", "1");
                    }
                    else{
                        m.writeMemory("0x203E8A14", "int", "0");
                    }

                }
            }
        }
    }
}
