using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace protótipos
{
    public partial class login : Form
    {
        string url = "http://localhost/Projeto-Estoque-master/";
        public login()
        {
            InitializeComponent();
        }

      

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                JObject user = get.post(url + "login.php", "&cpf=" + txtCpf.Text + "&senha=" + txtSenha.Text);
                var status = user["status"];
                if (status.ToString() == "True")
                {
                    txtCpf.Text = "";
                    txtSenha.Text = "";
                    Form1 form = new Form1();
                    form.Show();
                   
                    if (user["type"].ToString() == "2")
                    {
                        form.bloqueia();
                        
                    }
                   


                    this.Visible = false;

                }
                else
                {
                    MessageBox.Show("CPF ou senha errados!");
                }
            }
            catch
            {
                MessageBox.Show("CPF ou senha errados");
            }


        }

      

    }
}
