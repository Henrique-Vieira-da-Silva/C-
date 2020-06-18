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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace protótipos
{
    public partial class Form1 : Form
    {

        string url = "http://localhost/Projeto-Estoque-master/";
        public Form1()
        {
            InitializeComponent();



        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private bool login(string cpf, string senha)
        {
            string dadosPOST = "title=macoratti";
            dadosPOST = dadosPOST + "&body=teste de envio de post";
            dadosPOST = dadosPOST + "&userId=1";
            var dados = Encoding.UTF8.GetBytes(dadosPOST);
            var requisicaoWeb = WebRequest.CreateHttp("http://localhost/Projeto-Estoque-master/login.php");
            requisicaoWeb.Method = "POST";
            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
            requisicaoWeb.ContentLength = dados.Length;
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            //precisamos escrever os dados post para o stream
            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(dados, 0, dados.Length);
                stream.Close();
            }
            //ler e exibir a resposta
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<resultado>(objResponse.ToString());

                streamDados.Close();
                resposta.Close();
                return (post.status);
            }

        }


        private void gerenciarUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usuariosForm user = new usuariosForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
        }

        public void painel(Form novo)
        {
            panel3.Controls.Add(novo);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                JObject user = get.post(url + "sair.php", "");
                var status = user["status"];
                if (status.ToString() == "True")
                {
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm is login)
                        {
                            frm.Close();
                            break;
                        }
                    }
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Tente novamente, erro de conexão!");
                }
            }
            catch
            {
                MessageBox.Show("Tente novamente, erro de conexão!");
            }

        }

        public void bloqueia()
        {
            gerenciarUsuarios.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            sairToolStripMenuItem_Click(sender, e);
        }

        private void entrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                JObject user = get.post(url + "sair.php", "");
                var status = user["status"];
                if (status.ToString() == "True")
                {
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm is login)
                        {
                            frm.Visible = true;
                            break;
                        }
                    }
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Tente novamente, erro de conexão!");
                }
            }
            catch
            {
                MessageBox.Show("Tente novamente, erro de conexão!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.MaximizeBox = true;

            if (button7.Text == "Maximizar")
            {
                this.MaximizeBox = true;

                button7.Text = "Minimizar";
            }
            else if (button7.Text == "Minimizar")
            {
                button7.Text = "Maximizar";
                this.MaximizeBox = false;

            }
        }

        private void gerenciarProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProdutosForm user = new ProdutosForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
        }

        private void gerenciarComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendaForm user = new vendaForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
        }

        private void gerenciarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstoqueForm user = new EstoqueForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
        }

        private void novaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void gerenciarVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendaForm user = new vendaForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            usuariosForm user = new usuariosForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            user.esconderEdicao(true);
            
        }
    }




}
