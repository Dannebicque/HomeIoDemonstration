using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EngineIO;

namespace ApplicationDemo
{
    public partial class Form1 : Form
    {
        private bool ft1 = false;
        private bool ft2 = false;
        private bool X1 = true;
        private bool X2 = false;
        private bool front = false;
        private bool bpPrec = false;
        private bool bp;

        private MemoryBit lampe;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //conditions initiales
            this.X1 = true;
            this.X2 = false;
            this.bpPrec = false;
            this.lampe = MemoryMap.Instance.GetBit(0, MemoryType.Output);

            timer1.Start();
        }

        private void runCycleApi()
        {

            bpPrec = this.bp;
            //lecture des entrées
            this.bp = MemoryMap.Instance.GetBit(2, MemoryType.Input).Value;

            //calculs des FTs
            this.front = !this.bpPrec && this.bp;

            this.ft1 = this.X1 && this.front;
            this.ft2 = this.X2 && this.front;
            //calculs des étapes
            this.X1 = this.ft2 || this.X1 && !this.ft1;
            this.X2 = this.ft1 || this.X2 && !this.ft2;

            //écriture des sorties
            lampe.Value = X2;

            //mise à jour HomeIO
            MemoryMap.Instance.Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            runCycleApi();
        }
    }
}
