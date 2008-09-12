using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Netcode.Controls;

namespace Netcode.Common
{
    public class Messages
    {
        public Messages()
        {
        }

        public string EyeFriendlyPath(string path)
        {
            if (path.Length > 5)
            {
                string lp = path.Substring(0, 3);
                string rp = path.Substring(path.LastIndexOf("\\"));
                path = lp + "..." + rp;
                return path;
            }
            else
            {
                return path;
            }
        }

        #region ListView
        delegate void PrintListViewDelegate(string events, string commment, Color cl, int indx, ListView liv);
        /// <summary>
        /// Написать в ListView 2 столбца
        /// </summary>
        /// <param name="events"></param>
        /// <param name="commment"></param>
        /// <param name="cl"></param>
        /// <param name="indx"></param>
        /// <param name="liv"></param>
        public void write_lview_message(string events, string commment, Color cl, int indx, ListView liv)
        {
            PrintListViewDelegate Progress = new PrintListViewDelegate(write_lview_message2);
            if (Application.OpenForms["FormMain"] != null)
            {
                Application.OpenForms["FormMain"].Invoke(Progress, new object[] { events, commment, cl, indx, liv });
            }
            else
            {
                try
                {
                    write_lview_message2(events, commment, cl, indx, liv);
                }
                catch
                {
                }
            }
        }

        private void write_lview_message2(string events, string commment, Color cl, int indx, ListView liv)
        {
            DateTime dt = DateTime.Now;
            ListViewItem li = new ListViewItem(dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")");
            li.Name = commment;
            li.SubItems.Add(events);
            li.SubItems.Add(commment);

            li.BackColor = cl;
            li.ImageIndex = indx;
            liv.Items.Add(li);
            liv.Items[liv.Items.Count - 1].EnsureVisible();
        }
        #endregion

        #region ExListView
        delegate void PrintExListViewDelegate(string fname, string path, string opus, string hash, ListView liv, Color cl, int indx);
        /// <summary>
        /// Написать в ListView 4 столбца
        /// </summary>
        /// <param name="events"></param>
        /// <param name="commment"></param>
        /// <param name="cl"></param>
        /// <param name="indx"></param>
        /// <param name="liv"></param>
        public void write_lview_ex_message(string fname, string path, string opus, string hash, ListView liv, Color cl, int indx)
        {
            PrintExListViewDelegate showProgress = new PrintExListViewDelegate(write_lview_ex_message2);
            Application.OpenForms["FormMain"].Invoke(showProgress, new object[] { fname, path, opus, hash, liv, cl, indx });
        }

        private void write_lview_ex_message2(string fname, string path, string opus, string hash, ListView liv, Color cl, int indx)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = fname;
            lvi.BackColor = cl;
            lvi.ImageIndex = indx;
            lvi.SubItems.Add(path);
            lvi.SubItems.Add(opus);
            lvi.SubItems.Add(hash);
            lvi.Tag = new string[] { hash, path };
            liv.Items.Add(lvi);
            liv.Items[liv.Items.Count - 1].EnsureVisible();
        }
        #endregion

        #region ToolStripButton
        delegate void PropertySetTBtnPropDelegate(ToolStripButton tb, object value, string property);
        /// <summary>
        /// Установить значение свойства ToolStripButton
        /// </summary>
        /// <param name="tb">ToolStripButton</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public void set_tbtn_property(ToolStripButton tb, object value, string property)
        {
            PropertySetTBtnPropDelegate showProperty = new PropertySetTBtnPropDelegate(set_tbtn_property2);
            Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { tb, value, property });
        }

        private void set_tbtn_property2(ToolStripButton tb, object value, string property)
        {
            switch (property)
            {
                case "Enabled":
                    {
                        tb.Enabled = (bool)value;
                    }
                    break;

                case "ToolTipText":
                    {
                        tb.ToolTipText = (string)value;
                    }
                    break;

                case "Checked":
                    {
                        tb.Checked = (bool)value;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Button
        delegate void PropertySetBtnPropDelegate(Button btn, object value, string property);
        /// <summary>
        /// Установить значение свойства Button
        /// </summary>
        /// <param name="tb">Button</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public void set_btn_property(Button btn, object value, string property)
        {
            PropertySetBtnPropDelegate showProperty = new PropertySetBtnPropDelegate(set_btn_property2);
            Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { btn, value, property });
        }

        private void set_btn_property2(Button btn, object value, string property)
        {
            switch (property)
            {
                case "Enabled":
                    {
                        btn.Enabled = (bool)value;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region NumericUpDown
        delegate void PropertySetNumUDPropDelegate(NumericUpDown nud, object value, string property);
        /// <summary>
        /// Установить значение свойства NumericUpDown
        /// </summary>
        /// <param name="tb">NumericUpDown</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public void set_num_ud_property(NumericUpDown nud, object value, string property)
        {
            PropertySetNumUDPropDelegate showProperty = new PropertySetNumUDPropDelegate(set_num_ud_property2);
            Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { nud, value, property });
        }

        private void set_num_ud_property2(NumericUpDown nud, object value, string property)
        {
            switch (property)
            {
                case "Enabled":
                    {
                        nud.Enabled = (bool)value;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region FolderTreeExplorer
        delegate void PropertySetFoldrTEPropDelegate(FolderTreeExplorer fte, object value, string property);
        /// <summary>
        /// Установить значение свойства FolderTreeExplorer
        /// </summary>
        /// <param name="tb">FolderTreeExplorer</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public void set_fte_property(FolderTreeExplorer fte, object value, string property)
        {
            PropertySetFoldrTEPropDelegate showProperty = new PropertySetFoldrTEPropDelegate(set_fte_property2);
            Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { fte, value, property });
        }

        private void set_fte_property2(FolderTreeExplorer fte, object value, string property)
        {
            switch (property)
            {
                case "Enabled":
                    {
                        fte.Enabled = (bool)value;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region ToolStripLabel
        delegate void PropertySetTLabelPropDelegate(ToolStripLabel tlb, object value, string property);
        /// <summary>
        /// Установить значение свойства ToolStripLabel
        /// </summary>
        /// <param name="tb">ToolStripLabel</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public void set_tlabel_property(ToolStripLabel tlb, object value, string property)
        {
            PropertySetTLabelPropDelegate showProperty = new PropertySetTLabelPropDelegate(set_tlabel_property2);
            Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { tlb, value, property });
        }

        private void set_tlabel_property2(ToolStripLabel tlb, object value, string property)
        {
            switch (property)
            {
                case "Text":
                    {
                        tlb.Text = (string)value;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region CheckedListBox
        delegate object PropertyGetChLBPropDelegate(CheckedListBox list_box, object value, string property);
        /// <summary>
        /// Установить значение свойства CheckedListBox
        /// </summary>
        /// <param name="tb">CheckedListBox</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public object get_ch_box_property(CheckedListBox list_box, object value, string property)
        {
            PropertyGetChLBPropDelegate showProperty = new PropertyGetChLBPropDelegate(get_ch_box_property2);
            return (object)Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { list_box, value, property });
        }

        public object get_ch_box_property2(CheckedListBox list_box, object value, string property)
        {
            object ret = null;
            switch (property)
            {
                case "GetItemChecked":
                    {
                        ret = list_box.GetItemChecked(int.Parse(value.ToString()));
                    }
                    break;

                default:
                    break;
            }
            return ret;
        }
        #endregion

        #region NumericUpDown
        delegate object PropertyGetNumUDPropDelegate(NumericUpDown num_ud, string property);
        /// <summary>
        /// Установить значение свойства NumericUpDown
        /// </summary>
        /// <param name="tb">NumericUpDown</param>
        /// <param name="value">значение</param>
        /// <param name="property">свойство</param>
        public object get_num_up_down_property(NumericUpDown num_ud, string property)
        {
            PropertyGetNumUDPropDelegate showProperty = new PropertyGetNumUDPropDelegate(get_num_up_down_property2);
            return (object)Application.OpenForms["FormMain"].Invoke(showProperty, new object[] { num_ud, property });
        }

        public object get_num_up_down_property2(NumericUpDown num_ud, string property)
        {
            object ret=null;
            switch (property)
            {
                case "Value":
                    {
                        ret = num_ud.Value;
                    }
                    break;

                default:
                    break;
            }
            return ret;
        }
        #endregion
    }
}
