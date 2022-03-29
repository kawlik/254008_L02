using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _254008_L02
{

    public partial class Form1 : Form
    {
        public DB context;
        public int ID = 0;

        private string currency;
        private string date;

        public Form1()
        {
            InitializeComponent();

            context = new DB();

            update();
        }

        private void update()
        {
            button1.Enabled = false;

            if (currency == null) return;
            if (date == null) return;

            button1.Enabled = true;
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            ModelTable table = await NBP.GetTable(currency, date);

            if( table != null)
            {
                richTextBox1.Text = $"Kurs { table.code } ({ table.currency }) na dzień { table.rates[0].effectiveDate } wynosił { table.rates[0].mid }.\n" + richTextBox1.Text;

                table.ID = ID++;

                context.Serialized.Add( NBP.serialize( table ));
                context.SaveChanges();
            }
            else
            {
                richTextBox1.Text = $"Brak kursu { currency } na dzień { date }.\n" + richTextBox1.Text;
            }

            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            update();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currency = comboBox1.Text;

            update();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value.ToString( "yyyy-MM-dd" );

            update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string history = "Recent history: ---+---+---+---+---+---+---\n";

            var serialized = (from s in context.Serialized select s).ToList<Serial>();
            foreach (var serial in serialized)
            {
                ModelTable table = NBP.deserialize( serial.data );

                history += $"Kurs { table.code } ({ table.currency }) na dzień { table.rates[0].effectiveDate } wynosił { table.rates[0].mid }.\n";
            }

            richTextBox1.Text = history;

            update();
        }
    }
}
