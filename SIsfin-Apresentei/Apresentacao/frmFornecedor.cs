using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dados;
using Negocio;

namespace Apresentacao
{
    public partial class frmFornecedor : Form
    {
        private readonly FornecedorService _fornecedorService;
        private DataTable tblFornecedor = new DataTable();

        private int modo = 0;
        public frmFornecedor()
        {
            InitializeComponent();
            _fornecedorService = new FornecedorService();

            dgFornecedor.Columns.Add("Id", "ID");
            dgFornecedor.Columns.Add("Nome", "NOME");
            dgFornecedor.Columns.Add("tipoPesso", "TIPO PESSOA");
            dgFornecedor.Columns.Add("email", "EMAIL");
            dgFornecedor.Columns.Add("cpf_cnpj", "CPF_CNPJ");
            dgFornecedor.Columns.Add("razao_social", "RAZÃO SOCIAL");
            dgFornecedor.Columns.Add("rua", "RUA");
            dgFornecedor.Columns.Add("numero", "NUMERO");
            dgFornecedor.Columns.Add("cidade","CIDADE");
            dgFornecedor.Columns.Add("complemento","COMPLEMENTO");
            dgFornecedor.Columns.Add("cep", "CEP");
            dgFornecedor.Columns.Add("telefone", "TELEFONE");
            dgFornecedor.Columns.Add("celular", "CELULAR");

            dgFornecedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFornecedor.AllowUserToAddRows = false;
            dgFornecedor.AllowUserToDeleteRows = false;

            dgFornecedor.AllowUserToOrderColumns = true;
            dgFornecedor.ReadOnly = true;

            tblFornecedor = _fornecedorService.getAll();
        }

        private void Habilita()
        {
            switch (modo)
            {
                case 0: //neutro
                    btnInclui.Enabled = true;
                    btnAltera.Enabled = true;
                    btnExclui.Enabled = true;
                    btnSalva.Enabled = false;
                     //grpDados.Enabled = false;
                    txtNome.Enabled = false;
                    txtEmail.Enabled = false;
                    txtId.Enabled = false;
                    txtCelular.Enabled = false;
                    txtCep.Enabled = false;
                    txtNumero.Enabled = false;
                    txtRazao.Enabled = false;
                    txtcomplemento.Enabled = false;
                    txtRua.Enabled = false;
                    txtCpf.Enabled = false;
                    txtTelefone.Enabled = false;
                    txtCidade.Enabled = false;
                    radioPessoaFisica.Enabled = false;
                    radioPessoaJuridica.Enabled = false;
                    btnBusca.Enabled = true;
                    dgFornecedor.Enabled = true;
                    break;
                case 1: //inclusão
                    btnInclui.Enabled = false;
                    btnAltera.Enabled = false;
                    btnExclui.Enabled = false;
                    btnSalva.Enabled = true;
                    btnCancela.Enabled = true;

                    // grpDados.Enabled = true;
                    txtCidade.Enabled = true;
                    txtTelefone.Enabled = true;
                    txtNome.Enabled = true;
                    txtEmail.Enabled = true;
                    txtId.Enabled = true;
                    txtCelular.Enabled = true;
                    txtCep.Enabled = true;
                    txtNumero.Enabled = true;
                    txtRazao.Enabled = true;
                    txtNumero.Enabled = true;
                    txtcomplemento.Enabled = true;
                    txtRua.Enabled = true;
                    txtCpf.Enabled = true;
                    radioPessoaFisica.Enabled = true;
                    radioPessoaJuridica.Enabled = true; 
                    btnBusca.Enabled = true;

                    dgFornecedor.Enabled = false;
                    break;
                case 2:
                    btnInclui.Enabled = false;
                    btnAltera.Enabled = false;
                    btnExclui.Enabled = false;
                    btnSalva.Enabled = true;
                    btnCancela.Enabled = true;
                    grpDados.Enabled = true;
                    dgFornecedor.Enabled = false;
                    txtNome.Enabled = true;
                    txtCidade.Enabled = true;
                    txtTelefone.Enabled = true;
                    txtEmail.Enabled = true;
                    txtId.Enabled = true;
                    txtCelular.Enabled = true;
                    txtCep.Enabled = true;
                    txtNumero.Enabled = true;
                    txtRazao.Enabled = true;
                    txtcomplemento.Enabled = true;
                    txtRua.Enabled = true;
                    txtCpf.Enabled = true;
                    radioPessoaFisica.Enabled = true;
                    radioPessoaJuridica.Enabled = true;
                    btnBusca.Enabled = true;
                    break;
            }

        }
        public void LimpaForm()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtId.Clear();
            txtCelular.Clear();
            txtCep.Clear();
            txtNumero.Clear();
            txtRazao.Clear();
            txtcomplemento.Clear();
            txtRua.Clear();
            txtCpf.Clear();
            txtTelefone.Clear();
            txtCidade.Clear();
            radioPessoaFisica.Checked = false;
            radioPessoaJuridica.Checked = false;

            txtNome.Focus();
        }
        private void carregaGridView()
        {
            dgFornecedor.DataSource = _fornecedorService.getAll();
            dgFornecedor.Refresh();
        }



        private void btnExclui_Click(object sender, EventArgs e)
        {
            string resultado;
            String msg;
            DialogResult resposta;
            resposta = MessageBox.Show("Confirma exclusão?", "Aviso do sistema!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resposta == DialogResult.OK)
            {
                int idFornecedor = Convert.ToInt32(txtId.Text);
                resultado = _fornecedorService.Remove(idFornecedor);
                if (resultado == "SUCESSO")
                {
                    msg = "Fornecedor excluido com sucesso!";
                    carregaGridView();
                }
                else
                {
                    msg = "Falha ao excluir Fornecedor!";
                }
                MessageBox.Show(msg, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            frmPrompt f = new frmPrompt();
            string txtBusca = "";
            f.ShowDialog();
            txtBusca = f.Texto;
            DataTable tbClientes = _fornecedorService.filterByName(txtBusca);
            if (tbClientes != null)
            {
                dgFornecedor.DataSource = tbClientes;
                dgFornecedor.Refresh();
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            modo = 0;
            Habilita();
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            int id = 0;
            string nome;
            string email;
            string cpf_cnpj;
            string razao_social;
            string rua;
            int numero;
            string cidade;
            string complemento;
            string cep;
            string telefone;
            string celular;
            string resultado;
            string msg;
            int regAtual = 0;

            if (String.IsNullOrEmpty(txtId.Text))
                id = -1;
            else
                id = Convert.ToInt32(txtId.Text);

            nome = txtNome.Text;
            email = txtEmail.Text;
            if (!email.Contains("@"))
            {
                MessageBox.Show("Por favor, preencha o email com '@'", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cpf_cnpj = txtCpf.Text;
            razao_social = txtRazao.Text;
            rua = txtRua.Text; 
            numero = int.Parse(txtNumero.Text);
            if (numero < 0) {
                MessageBox.Show("Por favor, preencha o número de forma correta!", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cidade = txtCidade.Text;
            complemento = txtcomplemento.Text;
            cep = txtCep.Text;
            telefone = txtTelefone.Text;
            celular = txtCelular.Text;
           
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(cpf_cnpj) ||
       string.IsNullOrWhiteSpace(razao_social) || string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(cidade) ||
       string.IsNullOrWhiteSpace(complemento) || string.IsNullOrWhiteSpace(cep) || string.IsNullOrWhiteSpace(telefone) ||
       string.IsNullOrWhiteSpace(celular))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (numero <= 0)
            {
                MessageBox.Show("O número deve ser maior que zero.", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            TipoPessoa tp = radioPessoaFisica.Checked ? TipoPessoa.PESSOA_FISICA : TipoPessoa.PESSOA_JURIDICA;

            if (modo == 1)
            {
                resultado = _fornecedorService.Update(null, tp, nome, email, cpf_cnpj, razao_social, rua, numero, cidade, complemento, cep, telefone, celular);
                if (resultado == "SUCESSO")
                {
                    msg = "Fornecedor cadastrado com sucesso!";
                    carregaGridView();
                }
                else
                {
                    msg = "Falha ao cadastrar Fornecedor!";
                }
                MessageBox.Show(msg, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (modo == 2)
            {
                resultado = _fornecedorService.Update(id, tp, nome, email, cpf_cnpj, razao_social, rua, numero, cidade, complemento, cep, telefone, celular);
                if (resultado == "SUCESSO")
                {
                    msg = "FORNECEDOR atualizado com sucesso!";
                    carregaGridView();
                }
                else
                {
                    msg = "Falha ao atualizar FORNECEDOR!";
                }
                MessageBox.Show(msg, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            modo = 0;
            Habilita();

        }

        private void btnInclui_Click(object sender, EventArgs e)
        {
            modo = 1;
            Habilita();
            LimpaForm();
        }

        private void btnAltera_Click(object sender, EventArgs e)
        {
            modo = 2;
            Habilita();
        }

        private void dgFornecedor_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView row = (DataGridView)sender;
            if (row.CurrentRow == null)
                return;

            //limpa os TextBoxes
            txtId.Text = dgFornecedor.CurrentRow.Cells[0].Value.ToString();

            txtNome.Text = dgFornecedor.CurrentRow.Cells[1].Value.ToString();

            txtEmail.Text = dgFornecedor.CurrentRow.Cells[2].Value.ToString();

            txtCpf.Text = dgFornecedor.CurrentRow.Cells[4].Value.ToString();

            txtRazao.Text = dgFornecedor.CurrentRow.Cells[5].Value.ToString();

            txtRua.Text = dgFornecedor.CurrentRow.Cells[6].Value.ToString();

            txtNumero.Text = dgFornecedor.CurrentRow.Cells[7].Value.ToString();

            txtCidade.Text = dgFornecedor.CurrentRow.Cells[8].Value.ToString();

            txtcomplemento.Text = dgFornecedor.CurrentRow.Cells[9].Value.ToString();

            txtCep.Text = dgFornecedor.CurrentRow.Cells[10].Value.ToString();

            txtTelefone.Text = dgFornecedor.CurrentRow.Cells[11].Value.ToString();

            txtCelular.Text = dgFornecedor.CurrentRow.Cells[12].Value.ToString();


            if (dgFornecedor.CurrentRow.Cells[3].Value.ToString() == ((int)TipoPessoa.PESSOA_FISICA).ToString())
                radioPessoaFisica.Checked = true;
            else
                radioPessoaJuridica.Checked = true;
        }

        private void frmFornecedor_Load(object sender, EventArgs e)
        {
            radioPessoaFisica.Text = TipoPessoa.PESSOA_FISICA.ToString();
            radioPessoaJuridica.Text = TipoPessoa.PESSOA_JURIDICA.ToString();

       
            // NOVO ====================
            dgFornecedor.ColumnCount = 13;
            dgFornecedor.AutoGenerateColumns = false;
            dgFornecedor.Columns[0].Width = 40;
            dgFornecedor.Columns[0].HeaderText = "ID";
            dgFornecedor.Columns[0].DataPropertyName = "Id";
            //dgCliente.Columns[0].Visible = false;
            dgFornecedor.Columns[1].Width = 275;
            dgFornecedor.Columns[1].HeaderText = "NOME";
            dgFornecedor.Columns[1].DataPropertyName = "Nome";

            dgFornecedor.Columns[2].Width = 300;
            dgFornecedor.Columns[2].HeaderText = "EMAIL";
            dgFornecedor.Columns[2].DataPropertyName = "email";

            dgFornecedor.Columns[3].Width = 100;
            dgFornecedor.Columns[3].HeaderText = "TIPO";
            dgFornecedor.Columns[3].DataPropertyName = "tipoPessoa";

            dgFornecedor.Columns[4].Width = 200;
            dgFornecedor.Columns[4].HeaderText = "CPF_CNPJ ";
            dgFornecedor.Columns[4].DataPropertyName = "cpf_cnpj";

            dgFornecedor.Columns[5].Width = 150;
            dgFornecedor.Columns[5].HeaderText = "RAZAOSOCIAL";
            dgFornecedor.Columns[5].DataPropertyName = "razao_social";

            dgFornecedor.Columns[6].Width = 200;
            dgFornecedor.Columns[6].HeaderText = "RUA";
            dgFornecedor.Columns[6].DataPropertyName = "rua";

            dgFornecedor.Columns[7].Width = 40;
            dgFornecedor.Columns[7].HeaderText = "NUMERO";
            dgFornecedor.Columns[7].DataPropertyName = "numero";

            dgFornecedor.Columns[8].Width = 75;
            dgFornecedor.Columns[8].HeaderText = "CIDADE";
            dgFornecedor.Columns[8].DataPropertyName = "cidade";

            dgFornecedor.Columns[9].Width = 75;
            dgFornecedor.Columns[9].HeaderText = "COMPLEMENTO";
            dgFornecedor.Columns[9].DataPropertyName = "complemento";

            dgFornecedor.Columns[10].Width = 75;
            dgFornecedor.Columns[10].HeaderText = "CEP";
            dgFornecedor.Columns[10].DataPropertyName = "cep";

            dgFornecedor.Columns[11].Width = 75;
            dgFornecedor.Columns[11].HeaderText = "TELEFONE";
            dgFornecedor.Columns[11].DataPropertyName = "telefone";

            dgFornecedor.Columns[12].Width = 75;
            dgFornecedor.Columns[12].HeaderText = "CELULAR";
            dgFornecedor.Columns[12].DataPropertyName = "celular";


            dgFornecedor.AllowUserToAddRows = false;
            dgFornecedor.AllowUserToDeleteRows = false;
            dgFornecedor.MultiSelect = false;
            dgFornecedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            carregaGridView();
        }
    }
}
