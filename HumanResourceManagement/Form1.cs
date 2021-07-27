using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    public partial class Form1 : Form
    {
        List<Employee> list = new List<Employee>();

        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)   //tree'den seçilen node düzenleme amacıyla; çalışan ise 3'e, değilse 1'e yazılır
        {
            TreeView view = sender as TreeView;
            if (view.SelectedNode.Level != 2)
            {
                textBox1.Text = view.SelectedNode.FullPath;
            }
            else
            {
                textBox3.Text = view.SelectedNode.FullPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)   //textbox2 de yazan isim ile departman ekleme
        {
            if (treeView1.SelectedNode.Level == 0)
            {
                TreeNode parent = treeView1.SelectedNode;
                if (parent != null && textBox2.Text.Length != 0)
                {
                    TreeNode node = new TreeNode(textBox2.Text);
                    parent.Nodes.Add(node);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)   //düzenle butonu. seçili node'un adının textbox 2 de görünmesini sağlar
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                textBox2.Text = treeView1.SelectedNode.Text;
            }
            else
            {
                textBox2.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)    //departman güncelleme (save) butonu. Textbox2'nin son hali tree'ye kaydolur.
        {
             treeView1.SelectedNode.Text = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)  //departman silme butonu. Secili departman silinir (şirket adı silinemez)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }

        //alt bölüm
        private void button7_Click(object sender, EventArgs e)   //yeni kişiyi tree'ye ve class'a ekleme butonu
        {
            try
            {
                if (string.IsNullOrEmpty(textBox4.Text.Trim()))
                { throw new Exception("Please write a name firstly"); }
                if (string.IsNullOrEmpty(comboBox1.Text.Trim()))
                { throw new Exception("Please write a favourite language "); }
                
                Employee emp = new Employee(textBox4.Text.Trim(), comboBox1.Text.Trim());  

                 TreeNode parent = treeView1.SelectedNode;
                if (parent != null && treeView1.SelectedNode.Level == 1)   //node seçimi sadece kisi eklenebilecek sekildeyse (0-2 olamaz)
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        emp.certificate.Add(new List<String> { listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[2].Text });
                    }

                    TreeNode node = new TreeNode(emp.fullname);
                    parent.Nodes.Add(node);         //tree'ye eklemek.
                    list.Add(emp);                  //listeye eklemek
                    MessageBox.Show("Saved");
                }
                else
                {
                    MessageBox.Show("Ekleme basarisiz.Uygun Node secin!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)  //güncellemek icin save buton
        {
            try
            {
                if (string.IsNullOrEmpty(textBox4.Text.Trim()))
                { throw new Exception("Please write a name firstly"); }
                if (string.IsNullOrEmpty(comboBox1.Text.Trim()))
                { throw new Exception("Please write a favourite language "); }

                Employee emp = list.First(item => item.fullname == treeView1.SelectedNode.Text);  //tıkladıgım kisiyi classtan bul
               
                emp.fullname = textBox4.Text;    //bulunan kisinin özelliklerini değistirmek
                emp.favlang = comboBox1.Text;
                emp.certificate = new List<List<String>>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    emp.certificate.Add(new List<String> { listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[2].Text });
                }

                treeView1.SelectedNode.Text = textBox4.Text;    //görünüm olarakta degistirmek
                MessageBox.Show("Saved");
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button8_Click(object sender, EventArgs e)  //ismi ve tarihi bos olmadıgı sürece sertifikayı listviewa atar
        {
            try
            {
                if (string.IsNullOrEmpty(textBox5.Text))
                { throw new Exception("Please write Certificate Name!"); }
                if (string.IsNullOrEmpty(numericUpDown1.Text))
                { throw new Exception("Please write a Certificate's year "); }

                var item = listView1.Items.Add("Delete/Sil");
                item.SubItems.Add(numericUpDown1.Text);
                item.SubItems.Add(textBox5.Text);

            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);  
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 2000;
            numericUpDown1.Maximum = 2050;
  
        }

        private void listView1_Click(object sender, EventArgs e)  //ekrandan sertifika silme
        {
            listView1.SelectedItems[0].Remove();
        }

        private void button5_Click(object sender, EventArgs e)   //kişi silmek
        {
            if (treeView1.SelectedNode.Level == 2)
            {
                treeView1.Nodes.Remove(treeView1.SelectedNode);  //görünümden silmek
                Employee emp = list.First(item => item.fullname == treeView1.SelectedNode.Text);
                list.Remove(emp);            // classtan silmek
            }
        }

        private void button6_Click(object sender, EventArgs e)  //düzenle butonu; secilen kisinin bilgilerini classtan getirir
        {
            if (treeView1.SelectedNode.Level == 2)
            {
                textBox5.Text = "";

                listView1.Items.Clear();            //sertifikasız kisiye gecildiğinde eski kisinin sertifikalarının ekranda kalmaması icin

                textBox4.Text = treeView1.SelectedNode.Text;

                Employee emp = list.First(item => item.fullname == textBox4.Text);

                comboBox1.Text = emp.favlang;

                for (int i = 0; i < emp.certificate.Count; i++)
                {
                    var item = new ListViewItem(new[] { "Delete/Sil", emp.certificate[i][0], emp.certificate[i][1] });
                    listView1.Items.Add(item);
                }

            }
            else
            {
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
                listView1.Items.Clear();
            }

        }

    }
}


