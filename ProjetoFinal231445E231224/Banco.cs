﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoFinal231445E231224
{
    public class Banco
    {

        //Criando as variáveis publicas para a conexão e consulta serão usadas em todo o projeto
        //Connection responsável pela conexão com o MySQL
        public static MySqlConnection Conexao;
        //command responsável pekas instruções SQL a serem executados 
        public static MySqlCommand Comando;
        //adapter responsável por inserir dados em um dataTable
        public static MySqlDataAdapter Adaptador;
        //Database responsável por ligar o banco em controles com a propriedade DataSource
        public static DataTable datTabela;

        public static void Abrirconexao()
        {
            try
            {
                //Estabelece os parâmetros para a conexão com o banco
                Conexao = new MySqlConnection("server=localhost;port=3307;uid=root;pwd=etecjau; DATABASE =vendas");

                //Abre a conexão com o banco de dados 
                Conexao.Open();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void Fechar_Conexao()
        {
            try
            {
                //Fecha a conexão com o banco de dados 
                Conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CriarBanco()
        {
            try
            {


                //Abrindo a conexão
                Abrirconexao();

                //Informa a instrução SQL
                Comando = new MySqlCommand("CREATE DATABASE IF NOT EXISTS vendas; USE vendas;", Conexao);
                //Executa a query no MySQL (é o raínho do banco)
                Comando.ExecuteNonQuery();

                //Chama a função para fechar a conexão com o banco
                Fechar_Conexao();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmMenu_Load(Object sender, EventArgs e)
        {
            Banco.CriarBanco();
        }

        public static void CriarTabelas()
        {
            try
            {


                //Abrindo a conexão
                Abrirconexao();



                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS Cidades " + // esse c em 
                    "(id integer auto_increment primary key, " +
                    "nome char(40), " +
                    "uf char(02))", Conexao);
                Comando.ExecuteNonQuery();

                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS marcas " +
                    "(id integer auto_increment primary key," +
                    "NomeMarca char(40))", Conexao);
                Comando.ExecuteNonQuery();


                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS Categorias " +
                    "(id integer auto_increment primary key," +
                    "categoria char(40))", Conexao);
                Comando.ExecuteNonQuery();

                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS produtos " +
                    "(Id integer auto_increment primary key, " +
                    "descricao char(40), " +
                    "idCategoria integer," +
                    "idMarca integer," +
                    "estoque decimal(10,3), " +
                    "valorVenda decimal(10,2), " +
                    "foto varchar(100))", Conexao);

                Comando.ExecuteNonQuery();

                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS clientes" +
                  "(id integer auto_increment primary key, " +
                  "nome char(40), " +
                  "idCidade integer, " +
                  "dataNasc date, " +
                  "renda decimal(10,2)," +
                  "cpf char(14)," +
                  "foto varchar(100)," +
                  "venda boolean)", Conexao);

                Comando.ExecuteNonQuery();

                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS vendaCab" +
                    "(id integer auto_increment primary key," +
                    "idCliente int," +
                    "data date," +
                    "total decimal(10,2))", Conexao);
                Comando.ExecuteNonQuery();

                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS vendaDet" +
                    "(id integer auto_increment primary key," +
                    "idVendaCab int," +
                    "idProduto int," +
                    "qtde decimal(10,3)," +
                    "valorUnitario decimal(10,2))", Conexao);
                Comando.ExecuteNonQuery();



                //Chama a função para fechar a conexão com o banco
                Fechar_Conexao();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
