using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab2_inf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Class1.listViewFont = listView1.Font;
            toolStripStatusLabel1.Text = $"Total bytes: 0 \t";
            toolStripStatusLabel2.Text = $"0 of 0 items selected";
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo DirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item;

            foreach (FileInfo file in DirInfo.GetFiles())
            {
                item = new ListViewItem(new string[] { file.Name, file.Length.ToString(), file.Extension }, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                { 
                new ListViewItem.ListViewSubItem(item, "File"),
                new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                };
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach(ListViewItem listitem in listView1.Items)
            {
                listitem.Checked = true;
                var graphicFiles = new string[] { ".png", ".jpg", ".bmp", ".gif"};
                var oficeFiles = new string[] { ".docx", ".xlsx", ".pdf", ".txt"};
                var archiveFiles = new string[] {".zip", ".rar", ".7z"};
                var exeDllFiles = new string[] { ".dll", ".exe" };

                if (graphicFiles.Contains(listitem.SubItems[2].Text))
                    listitem.BackColor = Class1.graphicFilesColor;
                else if (oficeFiles.Contains(listitem.SubItems[2].Text))
                    listitem.BackColor = Class1.oficeFilesColor;
                else if (archiveFiles.Contains(listitem.SubItems[2].Text))
                    listitem.BackColor = Class1.archiveFilesColor;
                else if (exeDllFiles.Contains(listitem.SubItems[2].Text))
                    listitem.BackColor = Class1.exeDllFilesColor;
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            var filesSize = 0L;
            foreach(var file in DirInfo.GetFiles())
            {
                filesSize += file.Length;
            }
            toolStripStatusLabel1.Text = $"Total bytes: {filesSize.ToString()} \t";
            Update();
            Chart();
        }
        private void Update()
        {
            toolStripStatusLabel2.Text = $"{listView1.CheckedItems.Count} of {listView1.Items.Count} items selected";
        }
        private void PopulateTreeView()
        {
            TreeNode root;
            treeView1.Nodes.Clear();
            DirectoryInfo info = Class1.directory;
            if (info.Exists)
            {
                root = new TreeNode(info.Name);
                root.Tag = info;
                GetDirectories(info.GetDirectories(), root);
                treeView1.Nodes.Add(root);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode node;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                node = new TreeNode(subDir.Name, 0, 0);
                node.Tag = subDir;
                node.ImageKey = "folder";
                try
                {
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, node);
                    }
                }
                catch { }
                nodeToAddTo.Nodes.Add(node);
            }

        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form4 = new Form4();
            form4.ShowDialog();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (Class1.update)
            {
                PopulateTreeView();
                listView1.Font = Class1.listViewFont;
                Class1.update = false;
            }
        }

        private void listView1_ItemCheck(object sender, ItemCheckedEventArgs e)
        {
            Update();
            Chart();
        }
        public void Chart()
        {
            chart1.Series[0] = new Series();
            chart1.Series[0].LegendText = "Max size \n of files";
            var dictionary = new Dictionary<string, long>();
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                if (!dictionary.Keys.Contains(item.SubItems[2].Text))
                {
                    dictionary.Add(item.SubItems[2].Text, 0);
                }
            }

            foreach (ListViewItem item in listView1.CheckedItems)
            {
                if(long.Parse(item.SubItems[1].Text)> dictionary[item.SubItems[2].Text])
                dictionary[item.SubItems[2].Text] = long.Parse(item.SubItems[1].Text);
            }

            foreach (var item in dictionary)
            {
                chart1.Series[0].Points.AddXY(item.Key, item.Value);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                listView1.Font = fd.Font;
                treeView1.Font = fd.Font;
                chart1.Font = fd.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                TreeNode TN = treeView1.SelectedNode;
                string DPath = TN.FullPath;
                string path = fbd.SelectedPath + @"\saved.txt";
                FileStream FS = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter file = new StreamWriter(FS);

                DirectoryInfo DI = new DirectoryInfo(DPath);
                DirectoryInfo[] dri = DI.GetDirectories();
                foreach (DirectoryInfo s in dri)
                {
                    file.Write(s.Name);
                }
                file.Close();
                FS.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
