using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;


namespace SR_Test
{
    public partial class Form1 : Form
    {
        public Speech sp1 = new Speech();

        public Form1()
        {
            InitializeComponent();

            sp1.initRS();
            sp1.initTTS();
        }

    }
}
