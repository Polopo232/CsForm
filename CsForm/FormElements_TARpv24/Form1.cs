using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private const int StartX = 200;
        private const int StartY = 10;
        private const int Spacing = 10;

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
            tn.Nodes.Add(new TreeNode("Uus aken"));

            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Size = new Size(100, 30);

            lbl = new Label();
            lbl.Text = "Elementide loomine c# abil";
            lbl.Font = new Font("Arial", 24);
            lbl.Size = new Size(400, 30);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            pic = new PictureBox();
            pic.Size = new Size(250, 250);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = Image.FromFile(@"..\..\Images\close_box_red.png");
            pic.DoubleClick += Pic_DoubleClick;

            tree.Nodes.Add(tn);
            Controls.Add(tree);
        }
        private int ArrangeControls(Control[] controls, int startY)
        {
            int currentY = startY;
            foreach (var control in controls)
            {
                if (control != null)
                {
                    control.Location = new Point(StartX, currentY);
                    currentY += control.Height + Spacing;
                    if (!Controls.Contains(control))
                    {
                        Controls.Add(control);
                    }
                }
            }
            return currentY;
        }

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (Control ctrl in Controls.OfType<Control>().ToList())
            {
                if (ctrl != tree)
                    Controls.Remove(ctrl);
            }
            Controls.Add(tree);

            int currentY = StartY;

            if (e.Node.Text == "Nupp")
            {
                currentY = ArrangeControls(new[] { btn }, currentY);
            }
            else if (e.Node.Text == "Silt")
            {
                currentY = ArrangeControls(new[] { lbl }, currentY);
            }
            else if (e.Node.Text == "Pilt")
            {
                currentY = ArrangeControls(new[] { pic }, currentY);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                c_btn3 = new CheckBox { Size = new Size(100, 30), Text = "1" };
                c_btn4 = new CheckBox { Size = new Size(100, 30), Text = "2" };
                c_btn5 = new CheckBox { Size = new Size(100, 30), Text = "3" };
                c_btn6 = new CheckBox { Size = new Size(100, 30), Text = "4" };

                c_btn3.Click += new EventHandler(C_btn1_Click);
                c_btn4.Click += new EventHandler(C_btn1_Click);
                c_btn5.Click += new EventHandler(C_btn1_Click);
                c_btn6.Click += new EventHandler(C_btn1_Click);

                currentY = ArrangeControls(new[] { c_btn3, c_btn4, c_btn5, c_btn6 }, currentY);
            }
            else if (e.Node.Text == "Radionupp")
            {
                r_btn1 = new RadioButton { Text = "Must teema", Size = new Size(120, 20) };
                r_btn2 = new RadioButton { Text = "Valge teema", Size = new Size(120, 20) };
                r_btn3 = new RadioButton { Text = "Punane teema", Size = new Size(120, 20) };
                r_btn4 = new RadioButton { Text = "Kollane teema", Size = new Size(120, 20) };

                r_btn1.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn2.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn3.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn4.CheckedChanged += new EventHandler(r_btn_Checked);

                currentY = ArrangeControls(new[] { r_btn1, r_btn2, r_btn3, r_btn4 }, currentY);
            }
            else if (e.Node.Text == "MessageBox")
            {
                var answer = MessageBox.Show("Kirjuta lahe tekst", "Lahe aken", MessageBoxButtons.OK);
                if (answer == DialogResult.OK)
                {
                    string text = Interaction.InputBox("Siia kirjuta lahe tekst", "Lahe aken", "Siia kirjuta"); 
                    if (MessageBox.Show("Kas sa tõesti tahad selle saata?", "Lahe salvestusaken", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        lbl.Text = text;
                    }
                    else
                    {
                        lbl.Text = "...";
                    }
                    currentY = ArrangeControls(new[] { lbl }, currentY);
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
                lb.Size = new Size(120, 80);
                lb.SelectedIndexChanged += new EventHandler(ls_SelectedIndexChanged);
                currentY = ArrangeControls(new[] { lb }, currentY);
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
            }
            else if (e.Node.Text == "Uus aken")
            {
                NewForm(sender, e);
            }
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
            private int count = 0;
            private void Lbl_MouseLeave(object sender, EventArgs e)
            {if (count != 1) {
                    count ++;
                    lbl.BackColor = Color.Transparent;
                    var lbl_answer = MessageBox.Show("Kas sa hindasid teksti?", "Lahe aken", MessageBoxButtons.YesNo);
                    if (lbl_answer == DialogResult.Yes) {MessageBox.Show("See oli lahe !!!", "Lahe aken");}
                    else {MessageBox.Show("🖕", "🖕");}
                }
                else { return; }
            }

            private void Lbl_MouseHover(object sender, EventArgs e)
            {
                lbl.BackColor = Color.FromArgb(25, 140, 245);
            }
        private void menuFile_Exit_Select(object sender, EventArgs e) {Close();}
        private void menuCatImage(object sender, EventArgs e)
        {
            string catimg = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\cat.png"));
            pbCat.Image = Image.FromFile(catimg);
            pbCat.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCat.Size = new Size(100, 100);
            pbCat.Location = new Point(200, 110);
            if (!Controls.Contains(pbCat))
                Controls.Add(pbCat);
        }
        private void menuDogImage(object sender, EventArgs e)
        {
            string dogimg = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\dog.jpg"));
            pbDog.Image = Image.FromFile(dogimg);
            pbDog.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDog.Size = new Size(100, 100);
            pbDog.Location = new Point(200 + 110, 110);
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
            if (lb.SelectedItem != null)
            {
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
        }
        private void TabC_Selected(object sender, TabControlEventArgs e) {tabC.TabPages.Remove(tabC.SelectedTab);}
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


        private void NewForm(object sender, EventArgs e)
        {
            Form1 Form = new Form1();
            Form.Show();
            this.Hide();
        }
    }
}
