using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Medium.BoardGameKit.Code;

namespace Medium.BoardGameKit
{
    public partial class MainForm : Form
    {
        private BoardBase _chess = null;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _chess = new Chess(this);
        }
    }
}
