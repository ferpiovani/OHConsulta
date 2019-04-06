using System;
using System.Collections.Generic;
using System.Text;
using OHConsulta.DTO;
using System.Data.SqlClient;

namespace OHConsulta.DAL
{
    public class CETanque
    {
        public CETanque()
        {
            new ConexaoBD().getConexao().CreateTable<OHConsulta.DTO.CETanque>();
        }

        public List<OHConsulta.DTO.CETanque> Pesquisar()
        {
            return new ConexaoBD().getConexao().Table<OHConsulta.DTO.CETanque>().ToList();
        }

        public OHConsulta.DTO.CETanque PesquisarPorId(int id)
        {
            return new ConexaoBD().getConexao().Table<OHConsulta.DTO.CETanque>().Where(a => a.TanqueID == id).FirstOrDefault();
        }

        public Int32 Inserir(OHConsulta.DTO.CETanque tanque)
        {
            return new ConexaoBD().getConexao().Insert(tanque);
        }

        public Int32 Alterar(OHConsulta.DTO.CETanque tanque)
        {
            return new ConexaoBD().getConexao().Update(tanque);
        }

        public Int32 Excluir(OHConsulta.DTO.CETanque tanque)
        {
            return new ConexaoBD().getConexao().Delete(tanque);
        }

        public void Limpar()
        {
            new ConexaoBD().getConexao().DropTable<OHConsulta.DTO.CETanque>();
        }
    }
}
