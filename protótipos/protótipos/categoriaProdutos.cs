using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace protótipos
{
    public partial class categoriaProdutos : Form
    {
        string url = "http://localhost/Projeto-Estoque-master/";
        public categoriaProdutos()
        {
            InitializeComponent();
            carregaTabela();
            esconderEdicao(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            
            int retorno = -1;
            try
            {
                JObject user = get.post(url + "Produtos/Categorias/buscarById.php", "&id=" + txtId.Text);
                var produtos = user["categorias"].ToObject<CategoriaP[]>();
                var user1 = produtos[0];
                //  MessageBox.Show("fefefe"+user1.id);
                retorno = user1.id;

            }
            catch
            {

            }

            if (retorno == -1)
            {
                if (txtnome2.Text != "")
                {
                    try
                    {

                        JObject user = get.post(url + "produtos/Categorias/inserir.php", "&nome=" + txtnome2.Text );
                        var status = user["status"];
                        if (status.ToString() == "True")
                        {
                            limpar();
                            esconderEdicao(false);
                            carregaTabela();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao gravar, nome igual!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Categoria não encontrado!");
                    }
                }
                else
                {
                    MessageBox.Show("Prencher os Campos!");
                }
            }
            else
            {

                if (txtId.Text != "")
                {
                    try
                    {

                        JObject user = get.post(url + "Produtos/Categorias/editar.php", "&id=" + txtId.Text + "&nome=" + txtnome2.Text );
                        var status = user["status"];
                        if (status.ToString() == "True")
                        {
                            limpar();
                            esconderEdicao(false);
                            carregaTabela();
                        }
                        else
                        {
                            MessageBox.Show("Categoria não encontrado!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Categoria não encontrado!");
                    }
                }
                else
                {
                    MessageBox.Show("Selecione ou busque uma Categoria para Alterar!");
                }
            }



        }

        public void esconderEdicao(bool esconder)
        {
          
            lblid.Visible = esconder;
            txtId.Visible = esconder;
            txtnome2.Visible = esconder;
            btnBuscar.Visible = !esconder;
            txtnome.Visible = !esconder;
            btnCancelar.Visible = esconder;
            btnGravar.Visible = esconder;
           
        }

        private void carregaTabela()
        {

            tabela.Columns.Clear();
            tabela.Items.Clear();
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "Id";
            tabela.Columns.Add(ch);
            ch = new ColumnHeader();
            ch.Text = "Nome";
            tabela.Columns.Add(ch);
        

            //view = details
            try
            {

                var requisicaoWeb = WebRequest.CreateHttp(url + "Produtos/Categorias/listar.php");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.UserAgent = "RequisicaoWebDemo";
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    var jo = JObject.Parse(objResponse.ToString());
                    var games = jo["categorias"].ToObject<CategoriaP[]>();

                    foreach (var game in games)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = game.id.ToString();
                        item.SubItems.Add(game.nome);
                        tabela.Items.Add(item);
                        tabela.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    }

                    streamDados.Close();
                    resposta.Close();
                }
            }
            catch
            {
                MessageBox.Show("Tente novamente, erro de conexão!");
            }
        }

        private void limpar()
        {
            
            txtId.Text = "";
            txtnome.Text = "";
            txtnome2.Text = "";
   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                try
                {
                    JObject user = get.post(url + "Produtos/Categorias/excluir.php", "&id=" + txtId.Text);
                    var status = user["status"];
                    if (status.ToString() == "True")
                    {
                        limpar();
                        esconderEdicao(false);
                        carregaTabela();
                    }
                    else
                    {
                        MessageBox.Show("Categoria não encontrado!");
                    }

                }
                catch
                {
                    MessageBox.Show("Erro ao excluir, tente novamente mais tarde.");
                }
            }
            else
            {
                MessageBox.Show("Selecione ou busque uma Categoria para Excluir!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            esconderEdicao(true);
        }

        private void atualizar_Click(object sender, EventArgs e)
        {
            carregaTabela();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            esconderEdicao(false);
            limpar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                JObject user = get.post(url + "Produtos/Categoria/buscar.php", "&nome=" + txtnome.Text);
                var produtos = user["categorias"].ToObject<CategoriaP[]>();
                var user1 = produtos[0];
                //  MessageBox.Show("fefefe"+user1.id);
                esconderEdicao(true);

                txtId.Text = user1.id.ToString();
                txtnome2.Text = user1.nome;
          
            }
            catch
            {
                MessageBox.Show("Nenhum produto encontrado!");
            }
        }



        private void tabela_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListView.SelectedListViewItemCollection breakfast =
       this.tabela.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                txtId.Text = item.Text;
                txtnome2.Text = item.SubItems[1].Text;
                esconderEdicao(true);
            }
        }
    }


}
