using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroUsuario
{
    public partial class Form1 : Form
    {

        SqlConnection conexao = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\agobb\Downloads\CadastroUsuario\CadastroUsuario\Dados.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr; // Armazena retornos do SELECT

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
            conexao.Open();
            MessageBox.Show("Conexão Realizada", "Conexão com Banco");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message.ToString(), "Conexão com Banco");
            }

        }

        private void Limpar()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtCurso.Text = "";
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            /*
             INSERT INTO ALUNO (NOME, CURSO) VALUES ('ALUNO','CURSO')
             */
            int retorno;
            String cmd_sql;
            cmd_sql = "INSERT INTO ALUNO (NOME, CURSO) VALUES ('" + txtNome.Text + "','" + txtCurso.Text + "')";
            cmd = new SqlCommand(cmd_sql, conexao);
            retorno = cmd.ExecuteNonQuery(); //somente para instruções INSERT/DELETE/UPDATE
            
            if(retorno > 0)
            {
                MessageBox.Show("Cadastrado com sucesso!", "Cadastro");
                Limpar();
            } 
            else
            {
                MessageBox.Show("Cadastro não realizado!", "Cadastro");
            }

            cmd.Dispose();

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
            /*
             SELECT NOME, CURSO FROM ALUNO WHERE ID = 1
            */
                String cmd_sql;
                cmd_sql = "SELECT NOME, CURSO FROM ALUNO WHERE ID = " + txtId.Text;
                cmd = new SqlCommand(cmd_sql, conexao);
                dr = cmd.ExecuteReader();
                
                if (dr.HasRows)
                {
                    dr.Read();
                    txtNome.Text = dr["NOME"].ToString();
                    txtCurso.Text = dr["CURSO"].ToString();
                }
                else
                {
                    MessageBox.Show("Registro não localizado!", "Consulta");
                    Limpar();
                }

            dr.Close();
            cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registro não localizado! " + ex.Message.ToString(), "Consulta");
            }
            
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 UPDATE ALUNO SET NOME = 'NOME', CURSO = 'CURSO' WHERE ID = ?
                 */
                int retorno;
                String cmd_sql;
                cmd_sql = "UPDATE ALUNO SET NOME = '" + txtNome.Text + "', CURSO = '" + txtCurso.Text + "' WHERE ID = " + txtId.Text;
                cmd = new SqlCommand(cmd_sql, conexao);
                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                {
                    MessageBox.Show("Alteração realizada com sucesso!", "Alterar");
                }
                else
                {
                    MessageBox.Show("Não foi possível realizar a alteraçãp!", "Alterar");
                }

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registro não localizado! " + ex.Message.ToString(), "Alterar");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 DELETE ALUNO WHERE ID = ?
                 */
                int retorno;
                String cmd_sql;
                cmd_sql = "DELETE ALUNO WHERE ID = " + txtId.Text;
                cmd = new SqlCommand(cmd_sql, conexao);
                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                {
                    MessageBox.Show("Excluido com sucesso!", "Excluir");
                    Limpar();
                }
                else
                {
                    MessageBox.Show("Registro não localizado!", "Excluir");
                }

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registro não localizado! " + ex.Message.ToString(), "Excluir");
            }

        }

        private void btnExibir_Click(object sender, EventArgs e)
        {

        }
    }
}
