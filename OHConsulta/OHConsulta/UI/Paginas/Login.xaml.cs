using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OHConsulta.UI.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        public Login()
        {
            InitializeComponent();
            DTO.Config cfg = new DAL.Config().PesquisarPorId(1);
            if (cfg != null)
                Senha.Text = cfg.ConfigSenhaApp;
        }

        private void Config(Object sender, EventArgs args)
        {
            Navigation.PushAsync(new OHConsulta.UI.Paginas.Config());
        }

        private void Entrar(Object sender, EventArgs args)
        {
            DTO.CEDados dados = null;
            DTO.Config cfg = new DAL.Config().PesquisarPorId(1);
            String arquivo = String.Empty;

            if (!SAL.ConexaoFTP.CheckInternet())
            {
                DisplayAlert("Sem conexão com a internet", "Verifique sua conexão e tente novamente", "OK");
                return;
            } else if (!SAL.ConexaoFTP.CheckWebService())
            {
                DisplayAlert("Sem conexão com o WebService", "Contate o seu provedor de serviços: " + VariaveisControle.telOH, "OK");
                return;
            } else if (cfg == null)
            {
                DisplayAlert("Sem informações de configuração", "Para se conectar, configure o aplicativo", "OK");
                Config(sender, args);
                return;
            }

           
            arquivo = new SAL.ConexaoFTP(cfg, VariaveisControle.dir, VariaveisControle.extXML).DownloadArquivo();

            if (arquivo != null)
            {
                dados = new Parser.CEXML(arquivo).GetDados();
                if (dados != null)
                {
                    if (Senha.Text.Equals(dados.DadosSenha)) // SENHA CORRETA
                    {
                        Navigation.PushModalAsync(new TanquesLista(arquivo));
                    }
                    else if (Senha.Text.Equals("reset"))
                    {
                        new DAL.CEDados().Limpar();
                        new DAL.CETanque().Limpar();
                        new DAL.Config().Limpar();
                        DisplayAlert("Alerta", "Banco de dados resetado!", "OK");
                        Senha.Text = string.Empty;
                    }
                    else
                    {
                        DisplayAlert("Alerta", "Senha incorreta", "OK");
                        Senha.Text = string.Empty;
                        Senha.Focus();
                    }
                }
            } else if(arquivo == null)
            {
                DisplayAlert("Arquivo não localizado", "Verifique o CNPJ", "OK");
                Config(sender, args);
            }else
            {
                DisplayAlert("Não foi possível se conectar ao Web Service", "Verifique o endereço e o login do WebService", "OK");
                Config(sender, args);
            }


            /*
            if (String.IsNullOrEmpty(cfg.ConfigSenhaApp))
            {
                try
                {
                    
                } catch (Exception)
                {
                    DisplayAlert("Alerta", "Não foi possível se conectar ao Web Service", "OK");
                    return;
                }

                if (arquivo != null)
                {
                    dados = new Parser.CEXML(arquivo).GetDados();
                    if (dados != null)
                    {
                        cfg.ConfigSenhaApp = dados.DadosSenha;
                        new DAL.Config().Alterar(cfg);
                        Entrar(sender, args);
                    }
                }
                else DisplayAlert("Alerta", "Arquivo não encontrado", "OK");
            }
            else if (Senha.Text.Equals(cfg.ConfigSenhaApp))
            {
                cfg.ConfigLogou = true;
                new DAL.Config().Alterar(cfg);
                Navigation.PushModalAsync(new Paginas.TanquesLista());
                Senha.Text = string.Empty;
            }
            else if (Senha.Text.Equals("reset"))
            {
                new DAL.CEDados().Limpar();
                new DAL.CETanque().Limpar();
                new DAL.Config().Limpar();
                DisplayAlert("Alerta", "Banco de dados resetado!", "OK");
                Senha.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Alerta", "Senha incorreta", "OK");
                Senha.Text = string.Empty;
                Senha.Focus();
            } */

        }
    }
}