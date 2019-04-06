using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OHConsulta.DTO;

namespace OHConsulta.DAL
{
    public class CEDados
    {
        public CEDados()
        {
            new ConexaoBD().getConexao().CreateTable<OHConsulta.DTO.CEDados>();
        }

        public List<OHConsulta.DTO.CEDados> Pesquisar()
        {
            return new ConexaoBD().getConexao().Table<OHConsulta.DTO.CEDados>().ToList();
        }

        public OHConsulta.DTO.CEDados PesquisarPorId(int id)
        {
            return new ConexaoBD().getConexao().Table<OHConsulta.DTO.CEDados>().Where(a => a.DadosID == id).FirstOrDefault();
        }

        public List<OHConsulta.DTO.CEDados> PesquisarPorNome(String CNPJ)
        {
            return new ConexaoBD().getConexao().Table<OHConsulta.DTO.CEDados>().Where(a => a.DadosCNPJ.Contains(CNPJ)).ToList();
        }

        public Int32 Inserir(OHConsulta.DTO.CEDados dados)
        {
            return new ConexaoBD().getConexao().Insert(dados);
        }

        public Int32 Alterar(OHConsulta.DTO.CEDados dados)
        {
            return new ConexaoBD().getConexao().Update(dados);
        }

        public void Limpar()
        {
            new ConexaoBD().getConexao().DropTable<OHConsulta.DTO.CEDados>();
        }
    }
}

