using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.View.SideUI
{
    public partial class MailSetting : Form
    {
        DataTable tbl;
        string selectedReceiver;
        public MailSetting()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        public void LoadReceiverGrid()
        {
            if (Properties.Settings.Default.cfg_receivers == "")
            {
                Properties.Settings.Default.cfg_receivers = null;
            }
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
            string[] receivers;
            if (Properties.Settings.Default.cfg_receivers != null)
            {
                receivers = Properties.Settings.Default.cfg_receivers.Split(';');
            }
            else
            {
                receivers = null;
            }
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("receiver", "Receivers");
            if (receivers != null)
            {
                for (int i = 0; i < receivers.Length; i++)
                {
                    if (receivers[i] != null)
                        this.dataGridView1.Rows.Add(receivers[i]);
                }
            }
            this.dataGridView1.Sort(this.dataGridView1.Columns["receiver"], ListSortDirection.Ascending);
        }
        private void btnSaveSenderSetting_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.cfg_senders = txbSender.Text.Trim();
            Properties.Settings.Default.cfg_senderPW = txbSenderPW.Text.Trim();
            Properties.Settings.Default.Save();
            CTMessageBox.Show("Success");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MailSetting_Load(object sender, EventArgs e)
        {
            txbSender.Text = Properties.Settings.Default.cfg_senders;
            txbSenderPW.Text = Properties.Settings.Default.cfg_senderPW;
            LoadReceiverGrid();
        }

        private void txbReceiver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Properties.Settings.Default.cfg_receivers != null && Properties.Settings.Default.cfg_receivers.Contains(txbReceiver.Text.Trim()))
                {
                    if (txbReceiver.Text == "")
                    {
                        CTMessageBox.Show("Receiver address is empty! Please input a value before press save!");
                    }
                    else
                    {
                        CTMessageBox.Show("Receiver '" + txbReceiver.Text + "' have existed!");
                    }
                    txbReceiver.Clear();
                }
                else
                {

                    if (!string.IsNullOrEmpty(txbReceiver.Text.Trim()))
                    {
                        if (Properties.Settings.Default.cfg_receivers == null)
                        {
                            Properties.Settings.Default.cfg_receivers += txbReceiver.Text;
                            Properties.Settings.Default.Save();
                        }
                        else if (Properties.Settings.Default.cfg_receivers != null)
                        {
                            Properties.Settings.Default.cfg_receivers += ";" + txbReceiver.Text;
                            Properties.Settings.Default.Save();
                        }
                        LoadReceiverGrid();
                        txbReceiver.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Please input receiver email address !");
                    }
                }
            }
        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            if (txbReceiver.Text == "")
            {
                CTMessageBox.Show("Please select receiver need to delete !");
            }
            else
            {
                string remainReceiver = Properties.Settings.Default.cfg_receivers;
                string[] receivers = Properties.Settings.Default.cfg_receivers.Split(';');
                if (receivers[0] == selectedReceiver)
                {
                    if (remainReceiver == selectedReceiver)
                    {
                        Properties.Settings.Default.cfg_receivers = remainReceiver.Replace(selectedReceiver, null);
                    }
                    else
                    {
                        Properties.Settings.Default.cfg_receivers = remainReceiver.Replace(selectedReceiver + ";", null);
                    }
                }
                else
                {
                    Properties.Settings.Default.cfg_receivers = remainReceiver.Replace(";" + selectedReceiver, null);
                }
                Properties.Settings.Default.Save();
                LoadReceiverGrid();

                txbReceiver.Clear();
            }

        }

        private void txbReceiver_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbReceiver.Text))
            {
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns.Add("receiver", "Receivers");
                string receiverSearch = "";
                string[] receivers;
                if (Properties.Settings.Default.cfg_receivers != null)
                {
                    receivers = Properties.Settings.Default.cfg_receivers.Split(';');
                }
                else
                {
                    receivers = null;
                }
                if (receivers!=null)
                {
                    for (int i = 0; i < receivers.Length; i++)
                    {
                        if (receivers[i].Contains(txbReceiver.Text))
                        {
                            if (receiverSearch == "")
                            {
                                receiverSearch += receivers[i];
                            }
                            else
                            {
                                receiverSearch += ";" + receivers[i];
                            }

                        }
                    }
                    string[] receiverSearchList = receiverSearch.Split(';');
                    if (receiverSearchList != null)
                    {
                        for (int j = 0; j < receiverSearchList.Length; j++)
                        {
                            this.dataGridView1.Rows.Add(receiverSearchList[j]);
                        }
                    }

                }
            }
            else
            {
                LoadReceiverGrid();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txbReceiver.Text = row.Cells[0].Value.ToString();
                selectedReceiver = txbReceiver.Text;
            }
        }
    }
}
