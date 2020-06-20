
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;


namespace protótipos
{
    public class get
    {
        public static JObject post(string url, string dado)
        {
            string dadosPOST = "title=macoratti";
            dadosPOST = dadosPOST + "&body=teste de envio de post";
            dadosPOST = dadosPOST + dado;
            var dados = Encoding.UTF8.GetBytes(dadosPOST);
            var requisicaoWeb = WebRequest.CreateHttp(url);
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
            try
            {
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                       //  MessageBox.Show("retorno: "+objResponse.ToString());//#####################
                    var jo = JObject.Parse(objResponse.ToString());
                    streamDados.Close();
                    resposta.Close();
                    return (jo);
                }
            }
            catch
            {
                return (null);
            }

        }

    }


}

