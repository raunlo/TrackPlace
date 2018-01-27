﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TrackPlace.WPF.Controls
{
    /// <summary>
    /// Custom date picker for addin trucks. Source: https://social.technet.microsoft.com/wiki/contents/articles/26908.wpfmvvm-binding-the-datepickertextbox-in-wpf.aspx#Sample_application
    /// </summary>
    public class CustomDatePicker : DatePicker
    {
        protected DatePickerTextBox _datePickerTextBox;
        protected readonly string _shortDatePattern;

        public CustomDatePicker()
            : base()
        {
            _shortDatePattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            _datePickerTextBox = this.Template.FindName("PART_TextBox", this) as DatePickerTextBox;
            _datePickerTextBox.Text = "Vali kuupäev";
            if (_datePickerTextBox != null)
            {
                _datePickerTextBox.Text = "Vali kuupäev";
                _datePickerTextBox.TextChanged += dptb_TextChanged;
            }
        }
        private void dptb_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParseExact(_datePickerTextBox.Text, _shortDatePattern, Thread.CurrentThread.CurrentCulture,
                System.Globalization.DateTimeStyles.None, out dt))
            {
                this.SelectedDate = dt;
            }

        }
    }
}