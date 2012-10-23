﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OmGui
{
    public partial class DateRangeForm : Form
    {
        public DateRangeForm(string title, string prompt)
        {
            InitializeComponent();
            Text = title;
            labelPrompt.Text = prompt;
            FromDate = DateTime.MinValue;
            UntilDate = DateTime.MaxValue;
        }

        public DateTime FromDate
        {
            get
            {
                if (!dateTimePickerFrom.Checked) { return DateTime.MinValue; }
                return dateTimePickerFrom.Value; 
            }
            set
            {
                if (value < dateTimePickerFrom.MinDate)
                {
                    dateTimePickerFrom.Checked = false;
                    dateTimePickerFrom.Value = dateTimePickerFrom.MinDate;
                }
                else if (value > dateTimePickerFrom.MaxDate)
                {
                    dateTimePickerFrom.Checked = false;
                    dateTimePickerFrom.Value = dateTimePickerFrom.MaxDate;
                }
                else
                {
                    dateTimePickerFrom.Value = value;
                }

            }
        }

        public DateTime UntilDate
        {
            get 
            {
                if (!dateTimePickerUntil.Checked) { return DateTime.MaxValue; }
                return dateTimePickerUntil.Value; 
            }
            set
            {
                if (value < dateTimePickerUntil.MinDate)
                {
                    dateTimePickerUntil.Checked = false;
                    dateTimePickerUntil.Value = dateTimePickerUntil.MinDate;
                }
                else if (value > dateTimePickerUntil.MaxDate)
                {
                    dateTimePickerUntil.Checked = false;
                    dateTimePickerUntil.Value = dateTimePickerUntil.MaxDate;
                }
                else
                {
                    dateTimePickerUntil.Value = value;
                }

            }
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void DateRangeForm_Load(object sender, EventArgs e)
        {

        }
    }
}