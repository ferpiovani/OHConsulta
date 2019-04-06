using System;
using System.Collections.Generic;
using System.Text;

namespace OHConsulta.DAL
{
    public class Config
    {
        public Config()
        {
            new ConexaoBD().getConexao().CreateTable<DTO.Config>();
        }

        public DTO.Config PesquisarPorId(int id)
        {
            return new ConexaoBD().getConexao().Table<DTO.Config>().Where(a => a.ConfigID == id).FirstOrDefault();
        }

        public Int32 Inserir(DTO.Config config)
        {
            return new ConexaoBD().getConexao().Insert(config);
        }

        public Int32 Alterar(DTO.Config config)
        {
            return new ConexaoBD().getConexao().Update(config);
        }

        public void Limpar()
        {
            new ConexaoBD().getConexao().DropTable<OHConsulta.DTO.Config>();
        }

        
    }
}
