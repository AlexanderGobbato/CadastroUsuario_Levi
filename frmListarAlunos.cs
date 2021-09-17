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
    public partial class frmListarAlunos : Form
    {
        SqlConnection conexao = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\agobb\Downloads\CadastroUsuario\CadastroUsuario\Dados.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

        public frmListarAlunos()
        {
            InitializeComponent();
        }

        private void frmListarAlunos_Load(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
            }catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message.ToString(), "Conexão Banco");
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            String cmd_sql;
            cmd_sql = "SELECT NOME, CURSO FROM ALUNO WHERE NOME LIKE '%" + txtNome.Text + "%'";
            cmd = new SqlCommand(cmd_sql, conexao);
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lstAlunos.Items.Clear();
                while (dr.Read())
                {
                    lstAlunos.Items.Add(dr["Nome"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Registro não localizado!!!", "Consulta Aluno");
            }

            dr.Close();
            cmd.Dispose();
        }
    }
}
