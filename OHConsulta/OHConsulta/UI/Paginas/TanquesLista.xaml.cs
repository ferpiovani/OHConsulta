using SQLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OHConsulta.UI.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TanquesLista : ContentPage
	{
        String arquivo = string.Empty;
        DTO.Config cfg = null;

		public TanquesLista (String arquivo)
		{
			InitializeComponent();
            this.arquivo = arquivo;
            cfg = new DAL.Config().PesquisarPorId(1);
            PreencheDados();
        }

        private void Recarregar()
        {
            try
            {
                if (!SAL.ConexaoFTP.CheckInternet())
                {
                    DisplayAlert("Sem conexão com a internet", "Verifique sua conexão e tente novamente", "OK");
                    Sair();
                }
                else if (!SAL.ConexaoFTP.CheckWebService())
                {
                    DisplayAlert("Sem conexão com o WebService", "Contate o seu provedor de serviços: " + VariaveisControle.telOH, "OK");
                    Sair();
                }

                arquivo = new SAL.ConexaoFTP(this.cfg, VariaveisControle.dir, VariaveisControle.extXML).DownloadArquivo();

                if (arquivo == null)
                {
                    DisplayAlert("Arquivo não localizado", "Verifique o CNPJ", "OK");
                    Sair();
                }else if (arquivo.Equals(String.Empty))
                {
                    DisplayAlert("Não foi possível se conectar ao Web Service", "Verifique o endereço e o login do WebService", "OK");
                    Sair();
                }
               
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro inexperado", ex.ToString(), "OK");
                Sair();
            } 
            finally
            {
                PreencheDados();
            }

        }

        //somente estoque atual / total de vendas / total geral

        private void Sair()
        {
            Navigation.PopModalAsync();
        }

        private void PreencheDados()
        {
            ListaTanque.Children.Clear();
            // Carrega info. de config. do banco
            if (!string.IsNullOrEmpty(arquivo))
            {
                new SAL.ConsomeXML(arquivo).Recarregar();
                DTO.CEDados dados = new DAL.CEDados().PesquisarPorId(1);
                if (this.cfg.ConfigCNPJ.Equals(dados.DadosCNPJ))
                {
                    List<DTO.CETanque> lstTanques = new DAL.CETanque().Pesquisar();
                    if (dados != null) AddEmpresa(dados);
                    if (lstTanques != null)
                        if (lstTanques.Count == 0)
                        {
                            DisplayAlert("Atenção", "Não existem dados a serem carregados. Verifique se o LMC foi iniciado.", "OK");
                        }
                        else
                        {
                            Double totalGeral = 0;
                            foreach (DTO.CETanque tanque in lstTanques)
                            {
                                AddTanque(tanque);
                                totalGeral += tanque.TanqueCombVendido;
                            }
                            AddValorTotal(totalGeral);
                        }
                    PreencheBotoes();
                }
                else
                {
                    DisplayAlert("Atenção", "O CNPJ informado não é válido! Verifique o arquivo .xml", "OK");
                    Sair();
                }
            }
            else Recarregar();
            
        }

        private void PreencheBotoes()
        {
            AddRecarregar();
            AddSair();
        }

        #region Interface
        private void AddTanque(OHConsulta.DTO.CETanque tanque)
        {
            Double progresso = 0;
            Double qtdRestantePorc = 0;
            Double qtdRestante = tanque.TanqueQtdAnterior - tanque.TanqueCombVendido;

            if (qtdRestante != 0)
            {
                qtdRestantePorc = (qtdRestante * 100) / tanque.TanqueCapacidade;
                progresso = (qtdRestante) / tanque.TanqueCapacidade;
            }
            
            Color cor;

            if (progresso <= 0.20) cor = Color.FromHex("#cc3300");
            else if (progresso > 0.20 && progresso <= 0.40) cor = Color.FromHex("#ff9966");
            else if (progresso > 0.40 && progresso <= 0.60) cor = Color.FromHex("#ffcc00");
            else if (progresso > 0.60 && progresso <= 0.80) cor = Color.FromHex("#99cc33");
            else cor = Color.FromHex("#339900");

            var tanqueView = new StackLayout
            {
                BackgroundColor = Color.GhostWhite,
                Padding = new Thickness(5,0)
            };

            var tanqueInfo = new StackLayout { };

            var tanqueID = new Label
            {
                Text = "Tanque " + tanque.TanqueID.ToString() + " - " + tanque.TanqueCombTipo,
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            var tanqueVendido = new Label
            {
                Text =  "Estoque Atual: " + qtdRestante.ToString("N2") + " / " + "Vendido: " + tanque.TanqueCombVendido.ToString("N2"),
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            var tanqueVendidoValor = new Label
            {
                Text = "Total Vendido: R$ " + tanque.TanqueCombVendidoValor.ToString("N2"),
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            var tanqueResto = new Label
            {
                Text = "(" + (qtdRestante).ToString() + "/" + tanque.TanqueCapacidade.ToString()  + ")    " + qtdRestantePorc.ToString("N2") + "%",
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.End
            };



            var tanqueProgresso = new ProgressBar {
                
                Progress = progresso,
                ProgressColor = cor
            };

            tanqueInfo.Children.Add(tanqueID);
            tanqueInfo.Children.Add(tanqueVendido);
            tanqueInfo.Children.Add(tanqueVendidoValor);
            tanqueInfo.Children.Add(tanqueResto);
            tanqueView.Children.Add(tanqueInfo);
            tanqueView.Children.Add(tanqueProgresso);
            ListaTanque.Children.Add(tanqueView);
        }

        private void AddEmpresa(OHConsulta.DTO.CEDados dados)
        {
            var emprView = new StackLayout { };

            var emprNome = new Label
            {
                Text = dados.DadosNome,
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            var emprData = new Label
            {
                Text = dados.Data.ToString(),
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            emprView.Children.Add(emprNome);
            emprView.Children.Add(emprData);
            ListaTanque.Children.Add(emprView);
        }

        private void AddValorTotal(Double totalGeral)
        {
            var vrTotalView = new StackLayout { };

            var valorTotal = new Label
            {
                Text = "Total: " + totalGeral.ToString("N2"),
                TextColor = Color.FromHex("#290e72"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            vrTotalView.Children.Add(valorTotal);
            ListaTanque.Children.Add(vrTotalView);
        }

        private void AddRecarregar()
         {
             var btnRecarregar = new Button
             {
                 Padding = new Thickness(50, 20),
                 Text="Recarregar",
                 BackgroundColor = Color.FromHex("#290e72"),
                 TextColor = Color.FromHex("#FFFFFF")
             };

             btnRecarregar.Clicked += ClickRecarregar;
             ListaTanque.Children.Add(btnRecarregar);

         }
         
        private void AddSair()
        {
            var btnSair = new Button
            {
                Padding = new Thickness(50, 20),
                Text = "Sair",
                BackgroundColor = Color.FromHex("#290e72"),
                TextColor = Color.FromHex("#FFFFFF")
            };

            btnSair.Clicked += ClickSair;
            ListaTanque.Children.Add(btnSair);

        }
        

        #endregion

        #region Eventos Botões
        private void ClickRecarregar(Object sender, EventArgs args)
        {
            Recarregar();
        }

        private void ClickSair(Object sender, EventArgs args)
        {
            Sair();
        }
        #endregion
    }
}