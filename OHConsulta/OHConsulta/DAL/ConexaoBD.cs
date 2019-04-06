using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using OHConsulta.DTO;
using Xamarin.Forms;

namespace OHConsulta.DAL
{
    class ConexaoBD
    {
        private SQLiteConnection _conexao;

        public ConexaoBD()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.getCaminho("database.sqlite");
            _conexao = new SQLiteConnection(caminho);
        }

        public SQLiteConnection getConexao() {
            return _conexao;
        }


    }
}
