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
            this.status();


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
            status();
            
        }


        private void gerenciarUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usuariosForm user = new usuariosForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            this.status();
        }

        public void painel(Form novo)
        {
            panel3.Controls.Add(novo);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você realmente deseja Sair?", "Sair?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;
            try
            {

                JObject user = get.post(url + "sair.php", "");
                var status = user["status"];
                if (status.ToString() == "True")
                {
                    foreach (Form frm in Application.OpenForms)//###################################################
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
            gerenciarUsuarios.Visible = false;
            button5.Visible = false;
            gerenciarCategoriasDeProdutos.Visible = false;
            gerenciarProdutos.Visible = false;
            this.status();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            sairToolStripMenuItem_Click(sender, e);

        }

        private void entrarToolStripMenuItem_Click(object sender, EventArgs e)
  { 
            if (MessageBox.Show("Você realmente deseja Trocar de usuário?","Trocar de usuário?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;
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
                    this.status();
                }
            }
            catch
            {
                MessageBox.Show("Tente novamente, erro de conexão!");
                this.status();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
     

            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            }
            else if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;

            }
        }

        private void gerenciarProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProdutosForm user = new ProdutosForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            this.status();
        }

        private void gerenciarComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendaForm user = new vendaForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            this.status();
        }

        private void gerenciarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstoqueForm user = new EstoqueForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            this.status();
        }

        private void novaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.status();
        }

        private void gerenciarVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendaForm user = new vendaForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            this.status();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            usuariosForm user = new usuariosForm();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            user.esconderEdicao(true);
            this.status();
            
        }

        public void status() {

            lblstatus.Text = "Carregando...";
            panel4.BackColor = Color.Yellow;

            try
            {
                

         
                JObject user = get.post(url + "status.php", "");
                var status = user["status"];
                if (status.ToString() == "True")
                {
                    lblstatus.Text = "Conectado";
                    panel4.BackColor = Color.Green;
                }
                else
                {
                    MessageBox.Show("Tente novamente, erro de conexão!");
                    lblstatus.Text = "Não conectado";

                   
                    panel4.BackColor = Color.Red;
                    

                }
            }
            catch
            {
                MessageBox.Show("Tente novamente, erro de conexão!");
                lblstatus.Text = "Não conectado";
                panel4.BackColor = Color.Red;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal || this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

            }
          
        }

        private void gerenciarCategoriasDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categoriaProdutos user = new categoriaProdutos();
            user.TopLevel = false;
            user.Visible = true;
            user.MaximizeBox = true;
            panel3.Controls.Add(user);
            user.esconderEdicao(true);
            this.status();
        }
    }




}
