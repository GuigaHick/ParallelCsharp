using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwait
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int Calculate()
        {
            Thread.Sleep(5000);
            return 123;
        }

        public Task<int> CalculateValueAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return 123;
            });
        }

        public async Task<int> CalculateAsync()
        {
            await Task.Delay(5000);
            return 123;
        }

        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            //Blocking Call
            //int n = Calculate();
            //lblResult.Text = n.ToString();

            //Unblocking Call using Task

            //var calc = CalculateValueAsync();
            //calc.ContinueWith(t => {lblResult.Text = t.Result.ToString(); },
            //    TaskScheduler.FromCurrentSynchronizationContext());

            //Unblocking Call using Async
            int value = await CalculateAsync();
            lblResult.Text = value.ToString();
        }
    }
}
