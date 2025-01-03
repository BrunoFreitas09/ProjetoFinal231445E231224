﻿using ProjetoFinal231445E231224.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoFinal231445E231224.Views
{
    public partial class FrmClientes : Form
    {
        Cidade C;
        Cliente cl;
        public FrmClientes()
        {
            InitializeComponent();
        }

        void LimpaControles()
        {
            txtID.Clear();
            txtNomeCliente.Clear();
            cboCidades.SelectedIndex = -1;
            txtUf.Clear();
            mskCPF.Clear();
            txtRenda.Clear();
            dtpDataNasc.Value = DateTime.Now;
            picFoto.ImageLocation = "";
            chkVenda.Checked = false;
        }

        void CarregarGrid(string pesquisa)
        {
            cl = new Cliente()
            {
                nome = pesquisa
            };

            dgvClientes.DataSource = cl.Consultar();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            C = new Cidade();
            cboCidades.DataSource = C.Consultar();
            cboCidades.DisplayMember = "nome";
            cboCidades.ValueMember = "id";

            LimpaControles();
            CarregarGrid("");

            dgvClientes.Columns["idCidade"].Visible = false;
            dgvClientes.Columns["foto"].Visible = false;
        }
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtNomeCliente.Text == "") return;

            cl = new Cliente()
            {
                nome = txtNomeCliente.Text,
                idCidade = (int)cboCidades.SelectedValue,
                dataNasc = dtpDataNasc.Value,
                renda = double.Parse(txtRenda.Text),
                cpf = mskCPF.Text,
                venda = chkVenda != null && chkVenda.Checked
            };
            cl.Incluir();

            LimpaControles();
            CarregarGrid("");
        }

        private void cboCidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidades.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboCidades.SelectedItem;
                txtUf.Text = reg["UF"].ToString();
            }
        }

        private void picFoto_Click(object sender, EventArgs e)
        {
            ofdArquivo.InitialDirectory = "D:/fotos/clientes/";
            ofdArquivo.FileName = "";
            ofdArquivo.ShowDialog();
            picFoto.ImageLocation = ofdArquivo.FileName;
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.RowCount > 0)
            {
                txtID.Text = dgvClientes.CurrentRow.Cells["id"].Value.ToString();
                txtNomeCliente.Text = dgvClientes.CurrentRow.Cells["nome"].Value.ToString();
                cboCidades.Text = dgvClientes.CurrentRow.Cells["cidade"].Value.ToString();
                txtUf.Text = dgvClientes.CurrentRow.Cells["uf"].Value.ToString();
                chkVenda.Checked = (bool)dgvClientes.CurrentRow.Cells["venda"].Value;
                mskCPF.Text = dgvClientes.CurrentRow.Cells["cpf"].Value.ToString();
                dtpDataNasc.Text = dgvClientes.CurrentRow.Cells["dataNasc"].Value.ToString();
                txtRenda.Text = dgvClientes.CurrentRow.Cells["renda"].Value.ToString();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            cl = new Cliente()
            {
                id = int.Parse(txtID.Text),
                nome = txtNomeCliente.Text,
                idCidade = (int)cboCidades.SelectedValue,
                dataNasc = dtpDataNasc.Value,
                renda = double.Parse(txtRenda.Text),
                cpf = mskCPF.Text,
                venda = chkVenda.Checked
            };
            cl.Alterar();

            LimpaControles();
            CarregarGrid("");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaControles();
            CarregarGrid("");

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return ;

            if (MessageBox.Show("Deseja excluir o cliente? ", "Exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;

            cl = new Cliente()
            {
                id = int.Parse(txtID.Text)
            };
            cl.Excluir();

            LimpaControles();
            CarregarGrid("");

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrid(txtPesquisar.Text);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
