﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoFinal231445E231224.Models
{
    public class Marca
    {
        public int id { get; set; }
        public string NomeMarca { get; set; }


        public void Incluir()
        {
            try
            {
                //abrindo a conexão com o banco
                Banco.Abrirconexao();
                //Alimentando o método Command com a instrução desejada e indica a conexão utilizada
                Banco.Comando = new MySqlCommand("INSERT INTO marcas (nome) VALUES (@nome)", Banco.Conexao);
                //Cria os parâmetros utilizados na instrução SQL com seu respectivo conteúdo 
                Banco.Comando.Parameters.AddWithValue("@nome", NomeMarca);//Parâmetro string 
                //execura o Comando, no MYSQL, tem afunção do raio do Workbench
                Banco.Comando.ExecuteNonQuery();
                //Fecha a conexão
                Banco.Fechar_Conexao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Alterar()
        {
            try
            {
                //abrindo a conexão com o banco
                Banco.Abrirconexao();
                //Alimentando o método Command com a instrução desejada e indica a conexão utilizada
                Banco.Comando = new MySqlCommand("UPDATE marcas SET nome = @nome where id = @id", Banco.Conexao);
                //Cria os parâmetros utilizados na instrução SQL com seu respectivo conteúdo 
                Banco.Comando.Parameters.AddWithValue("@nome", NomeMarca);//Parâmetro string 
                Banco.Comando.Parameters.AddWithValue("@id", id);
                //execura o Comando, no MYSQL, tem afunção do raio do Workbench
                Banco.Comando.ExecuteNonQuery();
                //Fecha a conexão
                Banco.Fechar_Conexao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Excluir()
        {
            try
            {
                //abrindo a conexão com o banco
                Banco.Abrirconexao();
                //Alimentando o método Command com a instrução desejada e indica a conexão utilizada
                Banco.Comando = new MySqlCommand("DELETE FROM marcas WHERE id = @id", Banco.Conexao);
                //Cria os parâmetros utilizados na instrução SQL com seu respectivo conteúdo 
                Banco.Comando.Parameters.AddWithValue("@id", id);
                //execura o Comando, no MYSQL, tem afunção do raio do Workbench
                Banco.Comando.ExecuteNonQuery();
                //Fecha a conexão
                Banco.Fechar_Conexao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable Consultar()
        {
            try
            {
                //abrindo a conexão com o banco
                Banco.Abrirconexao();
                //Alimentando o método Command com a instrução desejada e indica a conexão utilizada
                Banco.Comando = new MySqlCommand("SELECT * FROM marcas WHERE NomeMarca LIKE @nome " +   //Esse N maiúsculo é bem sus
                                                                       "order by NomeMarca", Banco.Conexao);
                //Cria os parâmetros utilizados na instrução SQL com seu respectivo conteúdo 
                Banco.Comando.Parameters.AddWithValue("@nome", NomeMarca + "%");
                Banco.Adaptador = new MySqlDataAdapter(Banco.Comando);
                Banco.datTabela = new DataTable();
                Banco.Adaptador.Fill(Banco.datTabela);
                Banco.Fechar_Conexao();
                return Banco.datTabela;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
