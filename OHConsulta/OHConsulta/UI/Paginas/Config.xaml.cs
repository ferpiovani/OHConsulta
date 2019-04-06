using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OHConsulta.SAL;
using System.IO;
using System.Net;
using System.Xml;
using SQLite;

namespace OHConsulta.UI.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Config : ContentPage
	{
        DTO.Config cfg = null;


        public Config ()
		{
			InitializeComponent ();
            LimpaCampos();
            cfg = new DAL.Config().PesquisarPorId(1);
            if (cfg != null)
                PreencheCampos(cfg);
        }

        private Boolean validaCampos()
        {
            if (CNPJ.Text.Length != 14)
            {
                DisplayAlert("Erro!", "O CNPJ deve conter 14 digitos!", "OK");
                CNPJ.Text = string.Empty;
                CNPJ.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Endereco.Text))
            {
                DisplayAlert("Erro!", "O endereço do servidor deve ser fornecido!", "OK");
                Endereco.Text = string.Empty;
                Endereco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Login.Text))
            {
                DisplayAlert("Erro!", "O login deve ser fornecido!", "OK");
                Login.Text = string.Empty;
                Login.Focus();
                return false;
            }

            return true;
        }

        private void LimpaCampos()
        {
            Login.Text = string.Empty;
            Endereco.Text = string.Empty;
            CNPJ.Text = string.Empty;
        }

        private void PreencheCampos(DTO.Config cfg)
        {
            Login.Text = cfg.ConfigLogin;
            Endereco.Text = cfg.ConfigEndereco;
            CNPJ.Text = cfg.ConfigCNPJ;
        }

        private Int32 Gravar()
        {
            cfg = new DTO.Config();

            cfg.ConfigLogin = Login.Text;
            cfg.ConfigEndereco = Endereco.Text;
            cfg.ConfigCNPJ = CNPJ.Text;

            return new DAL.Config().Inserir(cfg);
        }

        private void gravaConfig(Object sender, EventArgs args)
        {
            if (validaCampos())
            {
                new DAL.Config().Limpar();
                if (Gravar() > 0) Navigation.PopAsync();
            }
        }
    }
}