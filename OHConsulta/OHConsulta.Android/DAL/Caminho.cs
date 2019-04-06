using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using OHConsulta.DAL;
using System.IO;

[assembly:Dependency(typeof(OHConsulta.Droid.DAL.Caminho))]
namespace OHConsulta.Droid.DAL
{
    class Caminho : ICaminho
    {
        public string getCaminho(string NomeArquivoBanco)
        {
            var caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

            return caminhoBanco;

            throw new NotImplementedException();
        }
    }
}