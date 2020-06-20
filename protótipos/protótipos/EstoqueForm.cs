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
    public partial class EstoqueForm : Form
    {
        string url = "http://localhost/Projeto-Estoque-master/";

        public EstoqueForm()
        {
            InitializeComponent();
            carregaTabela();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void atualizar_Click(object sender, EventArgs e)
        {
            carregaTabela();
        }

        private void carregaTabela()
        {

            tabela.Columns.Clear();
            tabela.Items.Clear();
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "Produto";
            tabela.Columns.Add(ch);
            ch = new ColumnHeader();
            ch.Text = "peso";
            tabela.Columns.Add(ch);
            ch = new ColumnHeader();
            ch.Text = "quantidade";
            tabela.Columns.Add(ch);
            ch = new ColumnHeader();
            ch.Text = "Data de vencimento";
            tabela.Columns.Add(ch);
            ch = new ColumnHeader();
            ch.Text = "Data de Chegada";
            tabela.Columns.Add(ch);
            ch = new ColumnHeader();
            ch.Text = "Cpf do Usuário";
            tabela.Columns.Add(ch);

            //view = details
            try
            {

                var requisicaoWeb = WebRequest.CreateHttp(url + "estoque/listar.php");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.UserAgent = "RequisicaoWebDemo";
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    MessageBox.Show(objResponse.ToString());
                    var jo = JObject.Parse(objResponse.ToString());
                    var games = jo["estoques"].ToObject<Estoque[]>();

                    foreach (var game in games)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = game.produto;
                        item.SubItems.Add(game.peso.ToString());
                        item.SubItems.Add(game.quantidade.ToString());
                        item.SubItems.Add(game.data_de_vencimento);
                        item.SubItems.Add(game.data_de_chegada);
                        item.SubItems.Add(game.cpf_funcionario);
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
