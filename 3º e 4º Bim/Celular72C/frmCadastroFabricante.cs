using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Celular72C
{
    public partial class frmCadastroFabricante : Form
    {
        string stringConexao = "Server = localhost; " +
                      "Database = CelularCTI; Port=5433;" +
                      "User ID= postgres; password = postgres;";

        NpgsqlConnection cn = new NpgsqlConnection();

        public void limpa_form()
        {
            txtId.Text = null;
            txtNome.Text = null;
            txtId.Focus();
        }

        private bool novoRegistro = false;

        private void HabilitaBotoes(bool novoRegistro)
        {
            if(novoRegistro)
            {
                txtId.Enabled = false;
                btnNovo.Enabled = false;
                btnSalvar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = false;
                btnSair.Enabled = false;
            }
            else
            {
                txtId.Enabled = true;
                btnNovo.Enabled = true;
                btnSalvar.Enabled = true;
                btnCancelar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSair.Enabled = true;
            }
        }

        public frmCadastroFabricante()
        {
            InitializeComponent();
        }

        private void frmCadastroFabricante_Load(object sender, EventArgs e)
        {
            try
            {
                cn.ConnectionString = stringConexao;
                cn.Open();
                HabilitaBotoes(false);
            }
            catch(NpgsqlException ex)
            {
                MessageBox.Show("Problemas ao conectar com o banco de dados !!! \n\n" +
                    "Mais detalhes: " + ex.Message,
                "Teste Conexão",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                this.Close();
            }

            
        }

        private void frmCadastroFabricante_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resposta;

            resposta = MessageBox.Show("Deseja realmente sair? \n\n",
            "Sair",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            
            if(resposta == DialogResult.Yes)
            {
                cn.Dispose();
                cn.Close();
                cn = null;
            }
            else
            {
                e.Cancel = true;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                if(String.IsNullOrEmpty(txtNome.Text))
                {
                    MessageBox.Show("Deve ser informado o nome do fabricante !!! \n\n",
                    "Cadastro de fabricantes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                    txtNome.Focus();
                    return;
                }

                NpgsqlCommand cmd = new NpgsqlCommand();

                if (novoRegistro == true)
                {
                    sql = "insert into fabricante (nome) values (@nome)";
                }
                else
                {
                    sql = "update fabricante set nome = @nome where id_fabricante=@id_fabricante";
                    cmd.Parameters.AddWithValue("@id_fabricante", Convert.ToInt64(txtId.Text));
                }

                cmd.CommandText = sql;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@nome", txtNome.Text);


                cmd.ExecuteNonQuery();

                MessageBox.Show("Fabricante salvo com sucesso !! \n\n",
                "Cadastro de fabricantes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);


                novoRegistro = false;
                HabilitaBotoes(false);
                limpa_form();

            }
            catch(NpgsqlException ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar os dados do fabricante !!! \n\n" +
                    "Mais detalhes: " + ex.Message,
                    "Inserção de fabricantes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);


            }
        }

        private void txtId_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                string sql;
                try
                {
                    sql = "select * from fabricante where id_fabricante = @id";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt64(txtId.Text));
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if(dr.Read())
                    {
                        txtNome.Text = (string)dr["nome"];
                        novoRegistro = false;
                    }
                    else
                    {
                        novoRegistro = false;
                        limpa_form();

                        MessageBox.Show("Fabricante não encontrado !!! \n\n",
                            "Pesquisa de fabricantes",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        
                    }
                    dr.Close();
                    novoRegistro = false;

                }
                catch (Exception ex)
                {


                    MessageBox.Show("Ocorreu um erro ao pesquisar os dados do fabricante !!! \n\n" +
                        "Mais detalhes: " + ex.Message,
                        "Pesquisa de fabricantes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    limpa_form();

                }
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpa_form();
            novoRegistro = true;
            HabilitaBotoes(novoRegistro);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitaBotoes(false);
            limpa_form();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty(txtId.Text))
                {
                    DialogResult resposta;

                    resposta = MessageBox.Show("Deseja realmente excluir o fabricante " + txtNome.Text + "?", "Celular CTI",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if(resposta == DialogResult.Yes)
                    {
                        string sql = "delete from fabricante where id_fabricante = @id_fabricante";

                        NpgsqlCommand cmd = new NpgsqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@id_fabricante", Convert.ToInt64(txtId.Text));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("O fabricante " + txtNome.Text + " foi excluido com sucesso",
                            "Celular CTI",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        HabilitaBotoes(false);
                        limpa_form();
                        novoRegistro = true;

                    }

                    



                }
            }
            catch (Exception ex)
            {


                MessageBox.Show("Ocorreu um erro ao tentar excluir o fabricante !!! \n\n" +
                    "Mais detalhes: " + ex.Message,
                    "Exclusão de Fabricantes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                limpa_form();

            }
        }
    }
}

/*

string stringSQL = "INSERT INTO Pessoa (Nome, Sobrenome, Idade, Altura, DataNascimento)" +
    "values (@nome, @sobrenome, @idade, @altura, @datanascimento)";

SqlCommand cmd = new SqlCommand(stringSQL, conn)

cmd.Parameters.AddWithValue("@nome", txtNome.Text);
cmd.Parameters.AddWithValue("@sobrenome", txtSobrenome.Text);
cmd.Parameters.AddWithValue("@idade", Int32.Parse(txtIdade.Text));
cmd.Parameters.AddWithValue("@altura", float.Parse(txtAltura.Text));
cmd.Parameters.AddWithValue("@datanascimento", DateTime.Parse(txtDataNascimento.Text));

*/