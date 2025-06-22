using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TaskFlow.View
{
    /// <summary>
    /// Interaktionslogik für CreateTaskPage.xaml
    /// </summary>
    public partial class CreateTaskPage : Page
    {
        const int HourLengthWithColon = 3;
        const int MaxHourDigits = 2;
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;

            //Lets the user remove the ":"
            if (e.Key == Key.Return && textBox.Text.Length == HourLengthWithColon) 
            {
                textBox.Text = textBox.Text[0].ToString() + textBox.Text[1].ToString();
            }

            //Adds a ":" everytime the user enters 2 numbers
            if (textBox.Text.Length == 2)
                SetTextAndMoveCaret(textBox, textBox.Text + ":");
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[\d]"))
                e.Handled = true;
        }
        private void CleanInvalidChars(TextBox textBox)
        {
            //BUG: User can Paste a text with a ":" in it!! FIX IT Maybe use LINQ?
            string cleaned = Regex.Replace(textBox.Text, @"[^\d:]", string.Empty);

            if (textBox.Text != cleaned)
                SetTextAndMoveCaret(textBox, cleaned);
        }
        private void FixFirstHourDigit(TextBox textBox)
        {
            if(!string.IsNullOrEmpty(textBox.Text))
            {
                string firstHourStr = textBox.Text.Substring(0,1);

                if(int.TryParse(firstHourStr, out int firstHourDigit) && firstHourDigit > 2 && !textBox.Text.StartsWith("0"))
                    SetTextAndMoveCaret(textBox, "0" + textBox.Text); 
            }
        }
        private void FixFirstMinuteDigit(TextBox textBox)
        {
            if(!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length > HourLengthWithColon)
            {
                string firstMinuteStr = textBox.Text.Substring(3, 1);

                string hourDigits = textBox.Text.Substring(0, 2);

                if(int.TryParse(firstMinuteStr, out int firstMinuteDigit) && firstMinuteDigit > 5)
                {
                    SetTextAndMoveCaret(textBox, hourDigits + ":0" + firstMinuteStr);
                }
            }
        }
        private void EnforceMaxHour(TextBox textBox, string[] time)
        {
            if (int.TryParse(time[0], out int hourDigits) && textBox.Text.Length >= MaxHourDigits && hourDigits > 23) 
            {
                if (time.Length > 1)
                    SetTextAndMoveCaret(textBox, "23:" + time[1]);

                else
                    SetTextAndMoveCaret(textBox, "23:");
            }
        }
        private void EnforceMaxMinute(TextBox textBox, string[] time)
        {
            if(time.Length > 1 && int.TryParse(time[1], out int minuteDigits) && minuteDigits > 59)
            {
                SetTextAndMoveCaret(textBox, time[0] + ":59");
            }
        }
        private void SetTextAndMoveCaret(TextBox textBox, string newText)
        {
            textBox.Text = newText;
            textBox.CaretIndex = textBox.Text.Length;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            string[] time = textBox.Text.Split(':');

            CleanInvalidChars(textBox);
            FixFirstHourDigit(textBox);
            FixFirstMinuteDigit(textBox);
            EnforceMaxHour(textBox, time);
            EnforceMaxMinute(textBox, time);
        }
        public CreateTaskPage()
        {
            InitializeComponent();
        }
    }
}
