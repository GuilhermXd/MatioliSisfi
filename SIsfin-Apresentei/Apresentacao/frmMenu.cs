using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void cadastroClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmFornecedor>().Count() >= 1) return;
            frmCliente frmcliente = new frmCliente();
            frmcliente.Show();
        }

        private void alunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aluno: Guilherme Liesemberg Massari\nAluno: Samuel Gilvane da Silva","Informação",MessageBoxButtons.OK ,MessageBoxIcon.Information);
        }

        private void cadastroFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmCliente>().Count() >= 1) return;

            frmFornecedor frmfornecedor = new frmFornecedor();
            frmfornecedor.Show();
        }
    }
}
