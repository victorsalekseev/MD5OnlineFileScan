using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using Netcode.Controls;
using Netcode.Common;
using Netcode.Common.Search;
using MySql.Data.MySqlClient;
using Netcode.Preferences;

namespace Netcode.Scan
{
    //Если eht.AddNewHtSign == True, то извлекаем дополнительную инфу

    public partial class FormMain : Form
    {
        enum ScanningType
        {
            AddNewSigns = 1,
            CheckSigns  = 2
        }

        Messages ms = new Messages();
        Search srch = new Search();
        Search addsg = new Search();
        ExHashTable eht = new ExHashTable();
        Netcode.Core.Core md = new Netcode.Core.Core();

        MySql.Data.MySqlClient.MyConnect my = new MyConnect(DB.host, DB.user, DB.password);
        string db_hash = LocalHash.db_hash;
        long count_files = 0;

        public FormMain()
        {
            InitializeComponent();
            this.FormClosed+=new FormClosedEventHandler(FormMain_FormClosed);
            folderTreeExploer.AddFolder += new FolderTreeExplorer.OnAddFolder(folderTreeExploer_AddFolder);
            folderTreeExploer.RemoveFolder += new FolderTreeExplorer.OnRemoveFolder(folderTreeExploer_RemoveFolder);
            toolStripButtonStart.Click += new EventHandler(toolStripButtonStart_Click);
            toolStripButtonPause.Click += new EventHandler(toolStripButtonPause_Click);
            toolStripButtonStop.Click += new EventHandler(toolStripButtonStop_Click);

            srch.FindFile += new Search.OnFindFile(srch_FindFile);
            srch.MakeError += new Search.OnMakeError(srch_MakeError);

            numericUpDownMin.Maximum = 100000000;
            numericUpDownMax.Maximum = 100000000;
            numericUpDownMin.Value = 0;
            numericUpDownMax.Value = 100000000;

            //загрузка сигн
            ms.write_lview_message("Проверка баз...", "Проверяются существующие базы", Color.GhostWhite, 5, listView_trace);
            Application.DoEvents();
            eht.OK += new ExHashTable.OnOK(eht_OK);
            eht.MakeError += new ExHashTable.OnMakeError(eht_MakeError);

            checkedListBox_true.SetItemChecked(0, true);
            checkedListBox_false.SetItemChecked(0, true);

            LoadBaseToExHT();
            buttonToCsuFile.Click += new EventHandler(buttonToCsuFile_Click);
            buttonFromCsuFile.Click += new EventHandler(buttonFromCsuFile_Click);
            button_ch0ose.Click += new EventHandler(button_ch0ose_Click);
            buttonSnewStDb.Click += new EventHandler(buttonSnewStDb_Click);
            button_begin_scan_add_sign.Click += new EventHandler(button_begin_scan_add_sign_Click);
            button_add_sign_stop.Click += new EventHandler(button_add_sign_stop_Click);
            button_add_sign_stop.Enabled = false;

            addsg.FindFile += new Search.OnFindFile(addsg_FindFile);
            addsg.MakeError += new Search.OnMakeError(addsg_MakeError);

            информацияОФайлеToolStripMenuItem.Click += new EventHandler(информацияОФайлеToolStripMenuItem_Click);
            сгенерироватьСигнатуруToolStripMenuItem.Click += new EventHandler(сгенерироватьСигнатуруToolStripMenuItem_Click);
            удалитьФайлToolStripMenuItem.Click += new EventHandler(удалитьФайлToolStripMenuItem_Click);
            очиститьToolStripMenuItem.Click += new EventHandler(очиститьToolStripMenuItem_Click);
            ОчиститьtoolStripMenuItem_trace.Click += new EventHandler(ОчиститьtoolStripMenuItem_trace_Click);
            listView_journal.DoubleClick += new EventHandler(информацияОФайлеToolStripMenuItem_Click);
            принудительноДобавитьВБДToolStripMenuItem.Click += new EventHandler(принудительноДобавитьВБДToolStripMenuItem_Click);
            импортХешзначенийИзФайлаToolStripMenuItem.Click += new EventHandler(импортХешзначенийИзФайлаToolStripMenuItem_Click);
            if (!DB.use_db)
            {
                принудительноДобавитьВБДToolStripMenuItem.Visible = false;
                button_begin_scan_add_sign.Width = 258;
                импортХешзначенийИзФайлаToolStripMenuItem.Enabled = true;
            }
            else
            {
                checkBox_accept_file.Enabled = true;
                checkBox_accept_file.Visible = true;
                импортХешзначенийИзФайлаToolStripMenuItem.Enabled = false;
            }

            contextMenuStrip_journal.Opened += new EventHandler(contextMenuStrip_journal_Opened);
            contextMenuStrip_trace.Opened += new EventHandler(contextMenuStrip_trace_Opened);

            my.MakeError += new MyConnect.OnMakeError(my_MakeError);
        }

        void импортХешзначенийИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog_hl.ShowDialog() == DialogResult.OK)
            {
                this.tabControl.Enabled = false;
                this.folderTreeExploer.Enabled = false;
                this.toolStripaction.Enabled = false;
                this.импортХешзначенийИзФайлаToolStripMenuItem.Enabled = false;
                using (StreamReader sr = new StreamReader(openFileDialog_hl.FileName, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        eht.AddNewHtSign(sr.ReadLine(), true);
                        toolStripStatusLabelScanCnt.Text = eht.CountNewHt.ToString();
                        toolStripStatusLabelScanFile.Text = "Локальное добавление";
                        Application.DoEvents();
                    }
                    toolStripStatusLabelScanFile.Text = "(idle)";
                    ms.write_lview_message("Сообщение", "Добавление сигнатур закончено", Color.GhostWhite, 3, listView_trace);
                }
                this.tabControl.Enabled = true;
                this.folderTreeExploer.Enabled = true;
                this.toolStripaction.Enabled = true;
                this.импортХешзначенийИзФайлаToolStripMenuItem.Enabled = true;
            }
        }

        private void LoadBaseToExHT()
        {
            if (File.Exists(db_hash))
            {
                eht.LoadBases(db_hash);
            }
            else
            {
                MessageBox.Show("Файл базы данных " + Environment.NewLine + db_hash + Environment.NewLine + "не найден" + Environment.NewLine + Environment.NewLine + "Сейчас будет создана новая база данных", "Внимание");
                eht.CreateDBAndLoadHTB(db_hash);
            }
        }

        void buttonSnewStDb_Click(object sender, EventArgs e)
        {
            SaveNewSigns();
        }
        /// <summary>
        /// Сохранение новых сигнатур
        /// </summary>
        private void SaveNewSigns()
        {
            int cnt_ht = eht.ht_base.Count;
            eht.AddNewHtSignsToHtBase(cnt_ht);
            eht.SaveHtSighToDB(db_hash);
            LoadBaseToExHT();
        }

        /// <summary>
        /// Добавление новой сигнатуры
        /// Только эту функцию следует вызывать
        /// из этого класса, чтобы добавить сигнатуру
        /// </summary>
        /// <param name="fname">файл в байтах</param>
        /// <param name="path">путь до файла</param>
        private void AddNewSignFromBytes(byte[] inByte, string path)
        {
            ms.write_lview_message("Добавление новой сигнатуры", "Производится анализ файла и добавление сигнатуры в базу", Color.GhostWhite, 5, listView_trace);
            ////Application.DoEvents();
            string md5sum = md.Analysing(inByte);
            if (eht.AddNewHtSign(md5sum, true))
            {
                AddFileInfoToOnlineDB(path, md5sum);
            }
        }

        private void AddFileInfoToOnlineDB(string path, string md5sum)
        {
            if (DB.use_db)
            {
                //Анализируем файл и добавляем в общую базу
                Hashtable ht_fi = new ExFileInfo().LoadFInfo(md5sum, path, checkBox_accept_file.Checked, DateTime.Now);
                my.Insert(ht_fi, DB.db_name, DB.tbl_fileinfo);
            }
        }

        //Выбор файла для занесения в БД
        void button_ch0ose_Click(object sender, EventArgs e)
        {
            eht.CountNewHt = 0;
            //Здесь есть сохранение во временную таблицу, чтобы делать обновления
            if (openFileDialogAddSign.ShowDialog() == DialogResult.OK)
            {
                AddNewSignFromBytes(srch.ReadFile(openFileDialogAddSign.FileName), openFileDialogAddSign.FileName);
            }
        }

        void buttonFromCsuFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogSign.ShowDialog() == DialogResult.OK)
            {
                ms.write_lview_message("Добавление новых сигнатур", "Производится анализ и добавление новых сигнатур в базу", Color.GhostWhite, 5, listView_trace);
                if (eht.LoadNewHtSighInDB_upd(openFileDialogSign.FileName, progressBarUpd))
                {
                    SaveNewSigns();
                }
                progressBarUpd.Value = progressBarUpd.Minimum;
            }
        }

        void buttonToCsuFile_Click(object sender, EventArgs e)
        {
            if (eht.ht_new.Count > 0)
            {
                MessageBox.Show("Создание файла обновлений." + Environment.NewLine + "Новых сигнатур: "+eht.ht_new.Count.ToString());
                saveFileDialogSign.FileName = "Up." + DateTime.Now.Ticks.ToString();
                if (saveFileDialogSign.ShowDialog() == DialogResult.OK)
                {
                    ms.write_lview_message("Сохранение новых сигнатур", "Производится анализ и сохранение новых сигнатур в файл", Color.GhostWhite, 5, listView_trace);
                    eht.SaveNewHtSighToDB_upd(saveFileDialogSign.FileName, progressBarUpd);
                    progressBarUpd.Value = progressBarUpd.Minimum;
                }
            }
            else
            {
                MessageBox.Show("Новых сигнатур пока не добавлено."+Environment.NewLine+"Сохранять пустой файл обновлений не надо.");
            }
        }

        void eht_MakeError(string Error)
        {
            ms.write_lview_message("Ошибка", Error, Color.Red, 4, listView_trace);
        }

        void eht_OK(string msg, string cmt)
        {
            ms.write_lview_message(msg, cmt, Color.GhostWhite, 6, listView_trace);
        }

        void srch_MakeError(string Error)
        {
            ms.write_lview_message("Ошибка", Error, Color.Red, 4, listView_trace);
        }

        void my_MakeError(string Error)
        {
            ms.write_lview_message("Ошибка БД", Error, Color.Red, 4, listView_trace);
        }

        void addsg_MakeError(string Error)
        {
            ms.write_lview_message("Ошибка", Error, Color.Red, 4, listView_trace);
        }

        /// <summary>
        /// Добавление сигнатур (через одноим. вкладку). Надо отдельно сохранять
        /// </summary>
        /// <param name="FileBytes"></param>
        /// <param name="nm"></param>
        void addsg_FindFile(byte[] FileBytes, FileInfo nm)
        {
            ms.set_tlabel_property(toolStripStatusLabelScanFile, new Messages().EyeFriendlyPath(nm.FullName), "Text");
            AddNewSignFromBytes(FileBytes, nm.FullName);
            ++count_files;
            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");
        }

        /// <summary>
        /// Поиск файлов и сравнение
        /// </summary>
        /// <param name="FileBytes">файл в байтах</param>
        /// <param name="nm">информация о файле</param>
        void srch_FindFile(byte[] FileBytes, FileInfo nm)
        {
            ms.set_tlabel_property(toolStripStatusLabelScanFile, new Messages().EyeFriendlyPath(nm.FullName), "Text");
            string md5sum = md.Analysing(FileBytes);

            ++count_files;
            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");
            //если сигнатура совпала
            bool sign_answ = eht.CheckSign(md5sum);
            if (sign_answ)
            {
                if ((bool)ms.get_ch_box_property(checkedListBox_true, 0, "GetItemChecked"))
                {
                    ms.write_lview_ex_message(nm.Name, nm.FullName, "Найден в локальной базе", md5sum, listView_journal, Color.White, 5);
                }
                if ((bool)ms.get_ch_box_property(checkedListBox_true, 1, "GetItemChecked"))
                {
                    try
                    {
                        File.Delete(nm.FullName);
                        ms.write_lview_ex_message(nm.Name, nm.FullName, "Удален (найден в локальной базе)", md5sum, listView_journal, Color.Red, 4);
                    }
                    catch (Exception ex)
                    {
                        ms.write_lview_ex_message(nm.Name, nm.FullName, "Ошибка удаления (найден в локальной базе) -- " + ex.Message, md5sum, listView_journal, Color.Tomato, 4);
                    }
                }
            }
            //Если сигнатура не совпала
            else
            {
                if ((bool)ms.get_ch_box_property(checkedListBox_false, 0, "GetItemChecked"))
                {
                    ms.write_lview_ex_message(nm.Name, nm.FullName, "Не найден в локальной базе", md5sum, listView_journal, Color.Yellow, 5);
                }
                if ((bool)ms.get_ch_box_property(checkedListBox_false, 1, "GetItemChecked"))
                {
                    try
                    {
                        AddNewSignFromBytes(srch.ReadFile(nm.FullName), nm.FullName);
                        SaveNewSigns();
                        ms.write_lview_ex_message(nm.Name, nm.FullName, "Добавлен в сигнатуры (не найден в локальной базе)", md5sum, listView_journal, Color.YellowGreen, 0);
                    }
                    catch (Exception ex)
                    {
                        ms.write_lview_ex_message(nm.Name, nm.FullName, "Ошибка добавления в сигнатуры (не найден в локальной базе) -- " + ex.Message, md5sum, listView_journal, Color.Tomato, 4);
                    }
                }
                if ((bool)ms.get_ch_box_property(checkedListBox_false, 2, "GetItemChecked") && !sign_answ)
                {
                    try
                    {
                        File.Delete(nm.FullName);
                        ms.write_lview_ex_message(nm.Name, nm.FullName, "Удален (не найден в локальной базе)", md5sum, listView_journal, Color.Red, 4);
                    }
                    catch (Exception ex)
                    {
                        ms.write_lview_ex_message(nm.Name, nm.FullName, "Ошибка удаления (не найден в локальной базе) -- " + ex.Message, md5sum, listView_journal, Color.Tomato, 4);
                    }
                }
            }
            //Application.DoEvents();
        }

        //Начать сканирование. Только здесь, в этой функции, условия учитываются и выполняются
        private void ScanStart(ScanningType st)
        {
            count_files = 0;
            ms.set_tbtn_property(toolStripButtonStart, false, "Enabled");
            ms.set_tbtn_property(toolStripButtonStart, "Идет сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonPause, false, "Checked");
            ms.set_tbtn_property(toolStripButtonPause, "Приостановить сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonStop, false, "Checked");

            ms.set_btn_property(button_begin_scan_add_sign, false, "Enabled");

            ms.set_fte_property(folderTreeExploer, false, "Enabled");

            ms.set_num_ud_property(numericUpDownMax, false, "Enabled");
            ms.set_num_ud_property(numericUpDownMin, false, "Enabled");

            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");

            switch (st)
            {
                case ScanningType.AddNewSigns:
                    {
                        addsg.FileSizeMax = (long)Math.Truncate((decimal)ms.get_num_up_down_property(numericUpDownMax, "Value"));
                        addsg.FileSizeMin = (long)Math.Truncate((decimal)ms.get_num_up_down_property(numericUpDownMin, "Value"));

                        ms.write_lview_message("Сообщение", "Добавление новых сигнатур запущено", Color.GhostWhite, 1, listView_trace);

                        ms.set_tbtn_property(toolStripButtonStart, false, "Checked");
                        ms.set_tbtn_property(toolStripButtonPause, false, "Enabled");
                        ms.set_tbtn_property(toolStripButtonStop, false, "Enabled");
                        ms.set_btn_property(button_add_sign_stop, true, "Enabled");

                        addsg.ScanFiles(folderTreeExploer.SelectedFolders);
                        //tabControl.SelectedTab = tabPageCalSet;
                    }
                    break;
                case ScanningType.CheckSigns:
                    {
                        srch.FileSizeMax = (long)Math.Truncate((decimal)ms.get_num_up_down_property(numericUpDownMax, "Value"));
                        srch.FileSizeMin = (long)Math.Truncate((decimal)ms.get_num_up_down_property(numericUpDownMin, "Value"));

                        ms.write_lview_message("Сообщение", "Сканирование запущено", Color.GhostWhite, 1, listView_trace);

                        ms.set_tbtn_property(toolStripButtonStart, true, "Checked");
                        ms.set_tbtn_property(toolStripButtonPause, true, "Enabled");
                        ms.set_tbtn_property(toolStripButtonStop, true, "Enabled");
                        ms.set_btn_property(button_add_sign_stop, false, "Enabled");

                        srch.ScanFiles(folderTreeExploer.SelectedFolders);
                        //tabControl.SelectedTab = tabPageFirst;
                    }
                    break;
                default:
                    break;
            }

            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");
            ScanStop(st, true);
            ms.write_lview_message("Сканировано", "Файлов: " + count_files.ToString(), Color.DodgerBlue, 5, listView_trace);
        }

        private void ScanPause()
        {
            //Не используется.
            ms.write_lview_message("Сообщение", "Сканирование приостановлено", Color.GhostWhite, 2, listView_trace);
            //toolStripButtonStart.Checked = false;
            //toolStripButtonStart.Enabled = true;
            //toolStripButtonStart.ToolTipText = "Начать сканирование";

            //toolStripButtonPause.Checked = true;
            //toolStripButtonPause.Enabled = false;
            //toolStripButtonPause.ToolTipText = "Сканирование приостановлено";

            //toolStripButtonStop.Checked = false;
        }

        private void ScanStop(ScanningType st, bool self)
        {
            string end = string.Empty;
            if (self)
            {
                end = "завершено";
            }
            else
            {
                end = "остановлено пользователем";
            }

            switch (st)
            {
                case ScanningType.AddNewSigns:
                    {
                        addsg.ScanStop();
                        while (addsg.IsScanning) { }
                        ms.write_lview_message("Сообщение", "Добавление новых сигнатур " + end, Color.GhostWhite, 3, listView_trace);
                    }

                    break;
                case ScanningType.CheckSigns:
                    {
                        srch.ScanStop();
                        while (srch.IsScanning) { }
                        ms.write_lview_message("Сообщение", "Сканирование " + end, Color.GhostWhite, 3, listView_trace);
                    }
                    break;
                default:
                    break;
            }

            ms.set_tlabel_property(toolStripStatusLabelScanCnt, count_files.ToString(), "Text");
            ms.set_btn_property(button_begin_scan_add_sign, true, "Enabled");
            ms.set_tbtn_property(toolStripButtonStart, false, "Checked");
            ms.set_tbtn_property(toolStripButtonStart, true, "Enabled");
            ms.set_tbtn_property(toolStripButtonStart, "Начать сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonPause, false, "Checked");
            ms.set_tbtn_property(toolStripButtonPause, true, "Enabled");
            ms.set_tbtn_property(toolStripButtonPause, "Приостановить сканирование", "ToolTipText");

            ms.set_tbtn_property(toolStripButtonStop, false, "Checked");

            ms.set_fte_property(folderTreeExploer, true, "Enabled");

            ms.set_num_ud_property(numericUpDownMax, true, "Enabled");
            ms.set_num_ud_property(numericUpDownMin, true, "Enabled");

            ms.set_tlabel_property(toolStripStatusLabelScanFile, "(idle)", "Text");
        }

        delegate void FindFileDelegate(ScanningType nm);
        FindFileDelegate calcPi;

        /// <summary>
        /// Начтать поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            calcPi = new FindFileDelegate(ScanStart);
            calcPi.BeginInvoke(ScanningType.CheckSigns, null, null);
        }

        /// <summary>
        /// НАчать добавление новых сигнатур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_begin_scan_add_sign_Click(object sender, EventArgs e)
        {
            calcPi = new FindFileDelegate(ScanStart);
            calcPi.BeginInvoke(ScanningType.AddNewSigns, null, null);
        }

        ///////////////////////////////////////////////////////////////////////

        void folderTreeExploer_RemoveFolder(object FolderName)
        {
            listView_trace.Items.RemoveByKey(FolderName.ToString());
        }

        void folderTreeExploer_AddFolder(object FolderName)
        {
            ms.write_lview_message("Проверить", FolderName.ToString(), Color.LightGreen, 0, listView_trace);
        }


        void ОчиститьtoolStripMenuItem_trace_Click(object sender, EventArgs e)
        {
            listView_trace.Items.Clear();
        }

        void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_journal.Items.Clear();
        }

        void contextMenuStrip_journal_Opened(object sender, EventArgs e)
        {
            if (listView_journal.SelectedItems == null || listView_journal.SelectedItems.Count < 1)
            {
                информацияОФайлеToolStripMenuItem.Enabled = false;
                принудительноДобавитьВБДToolStripMenuItem.Enabled = false;
                сгенерироватьСигнатуруToolStripMenuItem.Enabled = false;
                удалитьФайлToolStripMenuItem.Enabled = false;
                if (listView_journal.Items == null || listView_journal.Items.Count < 1)
                {
                    очиститьToolStripMenuItem.Enabled = false;
                }
                else
                {
                    очиститьToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                информацияОФайлеToolStripMenuItem.Enabled = true;
                сгенерироватьСигнатуруToolStripMenuItem.Enabled = true;
                очиститьToolStripMenuItem.Enabled = true;
                удалитьФайлToolStripMenuItem.Enabled = true;
                принудительноДобавитьВБДToolStripMenuItem.Enabled = true;
            }
        }

        void contextMenuStrip_trace_Opened(object sender, EventArgs e)
        {
            if (listView_trace.SelectedItems == null || listView_trace.SelectedItems.Count < 1)
            {
                if (listView_trace.Items == null || listView_trace.Items.Count < 1)
                {
                    ОчиститьtoolStripMenuItem_trace.Enabled = false;
                }
                else
                {
                    ОчиститьtoolStripMenuItem_trace.Enabled = true;
                }
            }
            else
            {
                ОчиститьtoolStripMenuItem_trace.Enabled = true;
            }
        }

        void button_add_sign_stop_Click(object sender, EventArgs e)
        {
            ScanStop(ScanningType.AddNewSigns, false);
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            ScanPause();
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            ScanStop(ScanningType.CheckSigns, false);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        void удалитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] prop = new string[2];
            if (listView_journal.SelectedItems.Count > 0)
            {
                prop = (string[])listView_journal.SelectedItems[0].Tag;
                if (MessageBox.Show("Вы действительно хотите удалить файл" + Environment.NewLine + Environment.NewLine + prop[1], "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(prop[1]);
                        listView_journal.SelectedItems[0].Remove();
                        ms.write_lview_message("Файл удален", prop[1], Color.White, 5, listView_trace);
                    }
                    catch (Exception ex)
                    {
                        ms.write_lview_message("Ошибка удаления", ex.Message, Color.Red, 4, listView_trace);
                    }
                }
            }
        }

        void сгенерироватьСигнатуруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] prop = new string[2];
            if (listView_journal.SelectedItems.Count > 0)
            {
                prop = (string[])listView_journal.SelectedItems[0].Tag;//0 = md5, 1 = path
                try
                {
                    AddNewSignFromBytes(srch.ReadFile(prop[1]), prop[1]);
                    SaveNewSigns();
                }
                catch (Exception ex)
                {
                    ms.write_lview_message("Не добавлен", "Ошибка добавления в сигнатуры (не найден в локальной базе) -- " + ex.Message, Color.Red, 4, listView_trace);
                }
            }
        }

        void принудительноДобавитьВБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] prop = new string[2];
            if (listView_journal.SelectedItems.Count > 0)
            {
                prop = (string[])listView_journal.SelectedItems[0].Tag;//0 = md5, 1 = path
                try
                {
                    AddFileInfoToOnlineDB(prop[1], prop[0]);
                    //ms.write_lview_message("Добавлен", "Файл добавлен в онлайн БД", Color.White, 5, listView_trace);
                }
                catch (Exception ex)
                {
                    ms.write_lview_message("Не добавлен", "Ошибка добавления в онлайн БД -- " + ex.Message, Color.Red, 4, listView_trace);
                }
            }
        }

        void информацияОФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] prop = new string[2];
            if (listView_journal.SelectedItems.Count > 0)
            {
                prop = (string[])listView_journal.SelectedItems[0].Tag;
                try
                {
                    using (FormDetails frm_d = new FormDetails(prop))
                    {
                        frm_d.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    ms.write_lview_message("Ошибка", ex.Message, Color.Red, 4, listView_trace);
                }
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string AppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.txt");

        //    Hashtable ht = new Hashtable(100000);

        //    for (int i = 0; i < 100000; i++)
        //    {
        //        string hash = string.Empty;
        //        for (int j = 0; j < 5; j++)
        //        {                    
        //            hash += new Random(i).Next(10000, 99999).ToString();
        //        }
        //        ht.Add(i+1, hash);
        //    }

            


        //    //XmlSerializer myXmlSer = new XmlSerializer(ht.GetType());
        //    using (FileStream fStream = File.Open(AppPath, FileMode.Create, FileAccess.Write))
        //    {
        //        //Rijndael rijndaelAlg = Rijndael.Create();
        //        //PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd_cr_file_sett, null); //класс, позволяющий генерировать ключи на базе паролей
        //        //pdb.HashName = "SHA512"; //будем использовать SHA512
        //        //byte[] iv = new Byte[rijndaelAlg.BlockSize >> 3];
        //        //byte[] key = pdb.GetBytes(rijndaelAlg.KeySize >> 3);

        //        //using (CryptoStream cStream = new CryptoStream(fStream,
        //        //  rijndaelAlg.CreateEncryptor(key, iv),
        //        //  CryptoStreamMode.Write))
        //        //using (StreamWriter sWriter = new StreamWriter(fStream))
        //        //{
        //        //    myXmlSer.Serialize(sWriter, ht);
        //        //    sWriter.Close();
        //        //}
        //        BinaryFormatter bf = new BinaryFormatter();
        //        bf.Serialize(fStream, ht);

        //    }

        //    //XmlSerializer myXmlSer = new XmlSerializer(o.GetType());
        //    //StreamWriter myWriter = new StreamWriter(path_to_set_file);
        //    //myXmlSer.Serialize(myWriter, o);
        //    //myWriter.Close();
        //}

//        private void button2_Click(object sender, EventArgs e)
//        {
//            string AppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.txt");

//            using (FileStream fStream = File.Open(AppPath, FileMode.Open, FileAccess.Read))
//            {
//                BinaryFormatter outFormatter = new BinaryFormatter();
//                Hashtable ht_out = (Hashtable)outFormatter.Deserialize(fStream);
//                MessageBox.Show("OK");
//                if (ht_out.ContainsValue("4997749977499774997749977"))
//                {
//                    MessageBox.Show("1");
//                }
//            }
//            /*
//                  Dim E As IDictionaryEnumerator  
//                  E = T.GetEnumerator  
//                  Console.WriteLine(ControlChars.NewLine & Header)  
//                  While E.MoveNext()  
//                   Console.WriteLine(E.Key & "=" & E.Value)  
//                  End While  
                 
                 
//Hashtable ht = new Hashtable();
//ht.Add("key1", "value1");
//ht.Add("key2", 2);
////получаем результат
//String s1 = ht["key1"].ToString();
//int i1 = (int)ht["key2"];
//                */

//        }
    }
}