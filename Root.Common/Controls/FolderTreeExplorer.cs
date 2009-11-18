using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace Root.Controls
{
    public partial class FolderTreeExplorer : UserControl
    {
        Hashtable list_folders = new Hashtable(20);
        public delegate void OnAddFolder(object FolderName);
        public event OnAddFolder AddFolder;

        public delegate void OnRemoveFolder(object FolderName);
        public event OnRemoveFolder RemoveFolder;

        public FolderTreeExplorer()
        {
            InitializeComponent();
            tV.CheckBoxes = true;

            tV.BeforeExpand += new TreeViewCancelEventHandler(tV_BeforeExpand);
            tV.AfterCheck += new TreeViewEventHandler(tV_AfterCheck);

            tV.Nodes.AddRange(PrintDrives());
            tV.ShowNodeToolTips = true;
            tV.HideSelection = false;
        }

        public Hashtable SelectedFolders
        {
            get { return list_folders; }
        }

        void tV_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SelectDirectory(e.Node);
        }

        /// <summary>
        /// Добавление/удаление директории
        /// </summary>
        /// <param name="node">Нода дерева. В Tag - полный путь до директории. В Checked - выбрана до нажатия/нет</param>
        private void SelectDirectory(TreeNode node)
        {
            PrecompChildNodes(node);
            if (node.Checked)
            {
                if (!list_folders.ContainsKey(node.Tag))
                {
                    list_folders.Add(node.Tag, node.Tag);

                    if (AddFolder != null)
                    {
                        AddFolder.Invoke(node.Tag);
                    }
                }

                Hashtable to_remove = new Hashtable(20);

                foreach (object path in list_folders.Keys)
                {
                    string input = string.Empty;
                    string pattern = string.Empty;

                    if (path.ToString().Length > node.Tag.ToString().Length)
                    {
                        input = path.ToString();
                        pattern = node.Tag.ToString();
                    }
                    else
                    {
                        input = node.Tag.ToString();
                        pattern = path.ToString();
                    }

                    bool cmp = input.Contains(pattern);//если выбранная папка ближе к корню, чем ранее выбранная
                    if ((cmp) && //так вот если она есть
                        (path.ToString() != node.Tag.ToString()) &&//и не равна самой себе
                        (path.ToString().Split(Convert.ToChar("\\")).Length != node.Tag.ToString().Split(Convert.ToChar("\\")).Length) //и их уровни вложенности разные
                        )
                    {
                        if (!to_remove.ContainsKey((object)input))
                        {
                            to_remove.Add((object)input, (object)input);
                        }
                        if (RemoveFolder != null)
                        {
                            RemoveFolder.Invoke(path);
                        }
                    }
                }

                foreach (object path in to_remove.Keys)
                {
                    list_folders.Remove((object)path);
                }

                node.Nodes.Clear();
                //MessageBox.Show(list_folders.Count.ToString() + "/" + to_remove.Count.ToString());
            }
            else
            {
                list_folders.Remove(node.Tag);
                if (RemoveFolder != null)
                {
                    RemoveFolder.Invoke(node.Tag);
                }
            }
        }

        void tV_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            PrecompChildNodes(e.Node);
        }

        private string print_file_size(long size)
        {
            string ret = string.Empty;
            if (size < 10240)
            {
                return size.ToString() + " Байт";
            }
            if (size < 1048576)
            {
                return Math.Floor((double)size / 1024) + " кБайт";
            }

            if (size < 737418240)
            {
                return string.Format("{0:0.00}", (double)size / 1048576) + " МБайт";
            }
            else
            {
                return string.Format("{0:0.00}", ((double)size / 1073741824)) + " ГБайт";
            }
        }

        private TreeNode[] PrintDrives()
        {
            DriveInfo[] dids = DriveInfo.GetDrives();
            TreeNode[] tns = new TreeNode[dids.Length];

            for (int i = 0; i < dids.Length; i++)
            {
                try
                {
                    string vol_label = string.Empty;
                    string disk_litera = string.Empty;
                    string disk_avial_free_space = string.Empty;
                    string disk_total_size = string.Empty;

                    if (i == 0 && dids[i].Name == "A:\\")
                    {
                        disk_litera = "A:\\";
                        vol_label = " (Дисковод гибких дисков)";
                    }
                    else
                    {
                        disk_litera = dids[i].Name;
                        if (dids[i].IsReady)
                        {
                            vol_label = "  (" + dids[i].VolumeLabel + ")";
                            disk_total_size =       "Размер:    " + print_file_size(dids[i].TotalSize) + Environment.NewLine;
                            disk_total_size +=      "Занято:     " + print_file_size(dids[i].TotalSize - dids[i].AvailableFreeSpace) + Environment.NewLine;
                            disk_avial_free_space = "Доступно: " + print_file_size(dids[i].AvailableFreeSpace);
                        }
                    }

                    TreeNode tn = new TreeNode(disk_litera + vol_label, 0, 0);
                    tn.ToolTipText = disk_total_size + disk_avial_free_space;
                    tn.Name = dids[i].Name;
                    tn.Tag = dids[i].Name.Replace("/", @"\");
                    tn.Nodes.Add("empty_", "empty_", 1, 1);
                    tns[i] = tn;
                }
                catch (Exception)
                {
                    TreeNode tn = new TreeNode("Ошибка определения", 0, 0);
                    tn.Name = dids[i].Name;
                    tn.Tag = dids[i].Name.Replace("/", @"\"); 
                    tn.Nodes.Add("empty_", "empty_", 1, 1);
                    tns[i] = tn;
                }
            }
            return tns;
        }
        private void PrecompChildNodes(TreeNode e)
        {
            try
            {
                string last_path = string.Empty;
                bool status_checked = false;

                e.Nodes.Clear();
                last_path = e.Tag.ToString();
                status_checked = e.Checked;
                DirectoryInfo di = new DirectoryInfo(e.Tag.ToString());

                DirectoryInfo[] dis = di.GetDirectories("*", SearchOption.TopDirectoryOnly);
                TreeNode[] tns = new TreeNode[dis.Length];
                for (int i = 0; i < dis.Length; i++)
                {
                    string path = Path.Combine(last_path, dis[i].Name);
                    TreeNode tn = new TreeNode(dis[i].Name, 1, 1);
                    tn.Name = dis[i].Name;
                    tn.Tag = path.Replace("/", @"\"); ;
                    tn.Checked = status_checked;
                    if (!status_checked)
                    {
                        if(list_folders.ContainsKey(tn.Tag))
                        {
                            tn.Checked = true;
                        }
                        tn.Nodes.Add("empty_", "empty_", 1, 1);
                    }
                    tns[i] = tn;
                }
                e.Nodes.AddRange(tns);
            }
            catch (IOException)
            {
            }
            catch (SecurityException)
            {
            }
            catch (ArgumentNullException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

    }
}
