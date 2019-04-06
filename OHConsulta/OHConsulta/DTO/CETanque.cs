using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace OHConsulta.DTO
{
    [Table("Tanque")]
    public class CETanque
    {
        
        public CETanque()
        {
            TanqueCombTipo = string.Empty;
        }

        [PrimaryKey, AutoIncrement]
        public Int32 TanqueID { get; set; }
        public String TanqueCombTipo { get; set; }
        public Double TanqueQtdAnterior { get; set; }
        public Double TanqueCombVendido { get; set; }
        public Double TanqueCombVendidoValor { get; set; }
        public Int32 TanqueCapacidade { get; set; }
    }
}
