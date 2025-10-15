using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace FormElements
{
    public partial class Form1 : Form
    {
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pic;
        CheckBox c_btn1, c_btn2, c_btn3, c_btn4, c_btn5, c_btn6;
        RadioButton r_btn1, r_btn2, r_btn3, r_btn4;
        TabControl tabC;
        TabPage tabP1, tabP2, tabP3;
        ListBox lb;
        PictureBox pbCat = new PictureBox();
        PictureBox pbDog = new PictureBox();
        bool t = true;
        public Form1()
        {
            
            Height = 600;
            Width = 800;
            Text = "Vorm elementidega";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid");
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("Märkeruut"));
            tn.Nodes.Add(new TreeNode("Radionupp"));
            tn.Nodes.Add(new TreeNode("MessageBox"));
            tn.Nodes.Add(new TreeNode("ListBox"));
            tn.Nodes.Add(new TreeNode("MainMenu"));


            //nupp
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Location = new Point(150, 30);
            btn.Height = 30;
            btn.Width = 100;

            //pealkiri
            lbl = new Label();
            lbl.Text = "Elementide loomine c# abil";
            lbl.Font = new Font("Arial", 24);
            lbl.Size = new Size(400, 30);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            pic = new PictureBox();
            pic.Size = new Size(50, 50);
            pic.Location = new Point(150, 60);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = Image.FromFile(@"..\..\Images\close_box_red.png");           
            pic.DoubleClick += Pic_DoubleClick;

            

            tree.Nodes.Add(tn);
            Controls.Add(tree);
        }
        int click = 0;
        private void Pic_DoubleClick(object sender, EventArgs e)
        {   //Double_Click -> carusel (3-4 images) 1-2-3-4-1-2-3-4-... 
            string[] images = { "cat.png", "dog.jpg" };
            string fail = images[click];
            pic.Image = Image.FromFile(@"..\..\Images\" + fail);
            click++;
            if (click==2) {click = 0;}
        }

        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            lbl.BackColor = Color.Transparent;

            var lbl_answer = MessageBox.Show("Kas sa hindasid teksti?", "Lahe aken", MessageBoxButtons.YesNo);

            if (lbl_answer == DialogResult.Yes)
            {
                MessageBox.Show("See oli lahe !!!", "Lahe aken");
            }
            else
            {
                MessageBox.Show("🖕", "🖕");
            }

        }

        private void Lbl_MouseHover(object sender, EventArgs e)
        {
            lbl.BackColor = Color.FromArgb(200, 10, 20);
        }
        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text=="Nupp")
            {
                Controls.Add(btn);
            }
            else if (e.Node.Text== "Silt")
            {
                Controls.Add(lbl);
            }
            else if (e.Node.Text=="Pilt")
            {
                Controls.Add(pic);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                c_btn1 = new CheckBox();
                c_btn1.Text = "Vali mind";
                c_btn1.Size = new Size(c_btn1.Text.Length * 9, 20);
                c_btn1.Location = new Point(310, 450);
                c_btn1.CheckedChanged += C_btn1_CheckedChanged;

                c_btn2 = new CheckBox();
                c_btn2.Size = new Size(100, 100);
                c_btn2.Image = Image.FromFile(@"..\..\Images\about.png");
                c_btn2.Location = new Point(310, 200);

                c_btn3 = new CheckBox();
                c_btn3.Size = new Size(100, 30);
                c_btn3.Text = "1";
                c_btn3.Location = new Point(310, 320);

                c_btn4 = new CheckBox();
                c_btn4.Size = new Size(100, 30);
                c_btn4.Text = "2";
                c_btn4.Location = new Point(310, 350);

                c_btn5 = new CheckBox();
                c_btn5.Size = new Size(100, 30);
                c_btn5.Text = "3";
                c_btn5.Location = new Point(310, 380);

                c_btn6 = new CheckBox();
                c_btn6.Size = new Size(100, 30);
                c_btn6.Text = "4";
                c_btn6.Location = new Point(310, 410);

                Controls.Add(c_btn1);
                Controls.Add(c_btn2);
                Controls.Add(c_btn3);
                Controls.Add(c_btn4);
                Controls.Add(c_btn5);
                Controls.Add(c_btn6);

                c_btn3.Click += new EventHandler(C_btn1_Click);
                c_btn4.Click += new EventHandler(C_btn1_Click);
                c_btn5.Click += new EventHandler(C_btn1_Click);
                c_btn6.Click += new EventHandler(C_btn1_Click);
            }
            else if (e.Node.Text == "Radionupp")
            {
                r_btn1 = new RadioButton();
                r_btn1.Text = "Must teema";
                r_btn1.Location = new Point(200, 420);

                r_btn2 = new RadioButton();
                r_btn2.Text = "Valge teema";
                r_btn2.Location = new Point(200, 440);

                r_btn3 = new RadioButton();
                r_btn3.Text = "Punane teema";
                r_btn3.Location = new Point(200, 460);

                r_btn4 = new RadioButton();
                r_btn4.Text = "Kollane teema";
                r_btn4.Location = new Point(200, 480);

                Controls.Add(r_btn1);
                Controls.Add(r_btn2);
                Controls.Add(r_btn3);
                Controls.Add(r_btn4);
                r_btn1.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn2.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn3.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn4.CheckedChanged += new EventHandler(r_btn_Checked);
            }
            else if (e.Node.Text== "MessageBox")
            {
                MessageBox.Show("Meesike", "Lahe aken");
                var answer = MessageBox.Show("Kirjuta lahe tekst", "Lahe aken", MessageBoxButtons.OK);
                if (answer == DialogResult.OK)
                {
                    string text = Interaction.InputBox("Siia kirjuta lahe tekst", "Lahe aken", "Siia kirjuta");
                    if (MessageBox.Show("Kas sa tõesti tahad selle saata?", "Lahe salvestusaken", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        lbl.Text = text;
                        Controls.Add(lbl);
                    }
                    else
                    {
                        lbl.Text = "...";
                        Controls.Add(lbl);
                    }
                }
                else
                {
                    MessageBox.Show("See oli lahe", "Lahe aken");
                }
            }
            else if (e.Node.Text == "ListBox")
            {
                lb = new ListBox();
                lb.Items.Add("Dog");
                lb.Items.Add("Cat");
                lb.Items.Add("Dog and cat");
                lb.Location = new Point(150,120);
                lb.SelectedIndexChanged += new EventHandler(ls_SelectedIndexChanged);
                Controls.Add(lb);
            }
            else if (e.Node.Text == "MainMenu")
            {
                MainMenu menu = new MainMenu();

                MenuItem menuFile = new MenuItem("File");

                MenuItem menuCat = new MenuItem("Cat");
                MenuItem menuDog = new MenuItem("Dog");

                menuFile.MenuItems.Add("Exit", new EventHandler(menuFile_Exit_Select));
                menuCat.MenuItems.Add("Cat", new EventHandler(menuCatImage));
                menuDog.MenuItems.Add("Dog", new EventHandler(menuDogImage));

                menu.MenuItems.Add(menuFile);
                menu.MenuItems.Add(menuCat);
                menu.MenuItems.Add(menuDog);
                Menu = menu;
                //
            }
        }
        private void menuFile_Exit_Select(object sender, EventArgs e)
        {
            Close();
        }
        private void menuCatImage(object sender, EventArgs e)
        {
            string catimg = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\cat.png"));
            pbCat.Image = Image.FromFile(catimg);
            pbCat.Location = new Point(300, 200);
            pbCat.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCat.Size = new Size(100, 100);

            if (!Controls.Contains(pbCat))
                Controls.Add(pbCat);
        }
        private void menuDogImage(object sender, EventArgs e)
        {
            string dogimg = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\dog.jpg"));
            pbDog.Image = Image.FromFile(dogimg);
            pbDog.Location = new Point(200, 200);
            pbDog.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDog.Size = new Size(100, 100);

            if (!Controls.Contains(pbDog))
                Controls.Add(pbDog);
        }
        private void ClearImages()
        {
            Controls.Remove(pbCat);
            Controls.Remove(pbDog);
        }
        private void ls_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearImages();

            switch (lb.SelectedItem.ToString())
            {   
                case "Cat":
                    menuCatImage(sender, e);
                    break;
                case "Dog":
                    menuDogImage(sender, e);
                    break;
                case "Dog and cat":
                    menuCatImage(sender, e);
                    menuDogImage(sender, e);
                    break;
            }
        }
        private void TabC_Selected(object sender, TabControlEventArgs e)
        {
            //this.tabC.TabPages.Clear();
            tabC.TabPages.Remove(tabC.SelectedTab);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void TabC_DoubleClick(object sender, EventArgs e)
        {
            string title = "tabP" + (tabC.TabCount - 1).ToString();
            TabPage tb = new TabPage(title);
            
            tabC.TabPages.Add(tb);
        }
        private void TabP3_DoubleClick(object sender, EventArgs e)
        {
            string title = "tabP" + (tabC.TabCount + 1).ToString();
            TabPage tb = new TabPage(title);
            tabC.TabPages.Add(tb);
        }
        private void r_btn_Checked(object sender, EventArgs e)
        {
            if (r_btn1.Checked)
            {
                BackColor = Color.Black;
                r_btn2.ForeColor = Color.White;
                r_btn1.ForeColor = Color.White;
                r_btn3.ForeColor = Color.White;
                r_btn4.ForeColor = Color.White;
            }
            else if (r_btn2.Checked)
            {
                BackColor = Color.White;
                r_btn2.ForeColor = Color.Black;
                r_btn1.ForeColor = Color.Black;
                r_btn3.ForeColor = Color.Black;
                r_btn4.ForeColor = Color.Black;
            }
            else if (r_btn3.Checked)
            {
                BackColor = Color.Red;
                r_btn2.ForeColor = Color.Black;
                r_btn1.ForeColor = Color.Black;
                r_btn3.ForeColor = Color.Black;
                r_btn4.ForeColor = Color.Black;
            }
            else if (r_btn4.Checked)
            {
                BackColor = Color.Yellow;
                r_btn2.ForeColor = Color.Black;
                r_btn1.ForeColor = Color.Black;
                r_btn3.ForeColor = Color.Black;
                r_btn4.ForeColor = Color.Black;
            }
        }
        private void C_btn1_Click(object sender, EventArgs e)
        {
            c_btn3.Checked = !c_btn3.Checked;
            c_btn4.Checked = !c_btn4.Checked;
            c_btn5.Checked = !c_btn5.Checked;
            c_btn6.Checked = !c_btn6.Checked;
        }
        private void C_btn1_CheckedChanged(object sender, EventArgs e)
        {           
            if (t)
            {
                Size = new Size(1000, 1000);
                pic.BorderStyle = BorderStyle.Fixed3D;
                c_btn1.Text = "Teeme väiksem";
                c_btn1.Font = new Font("Arial", 36, FontStyle.Bold);
                t = false;
            }
            else
            {
                Size = new Size(700, 500);
                c_btn1.Text = "Suurendame";
                c_btn1.Font = new Font("Arial", 36, FontStyle.Regular);
                t = true;
            }            
        }
    }
}
