using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace OHConsulta.DTO
{
    [Table("Dados")]
    public class CEDados
    {
        public CEDados()
        {
            DadosNome = string.Empty;
            DadosCNPJ = string.Empty;
            DadosSenha = string.Empty;
        }
        [PrimaryKey, AutoIncrement]
        public int DadosID { get; set; }
        public String DadosNome { get; set; }
        public String DadosCNPJ { get; set; }
        public DateTime Data { get; set; }
        public String DadosSenha { get; set; }
    }
}
