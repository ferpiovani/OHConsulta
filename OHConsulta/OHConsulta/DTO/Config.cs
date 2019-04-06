using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace OHConsulta.DTO
{
    [Table("Config")]
    public class Config
    {
        public Config()
        {
            ConfigEndereco = string.Empty;
            ConfigLogin = string.Empty;
            ConfigCNPJ = string.Empty;
        }
        [PrimaryKey, AutoIncrement]
        public int ConfigID { get; set; }
        public String ConfigEndereco { get; set; }
        public String ConfigLogin { get; set; }
        public String ConfigCNPJ { get; set; }
        public String ConfigSenhaApp { get; set; }
        public Boolean ConfigLogou { get; set; }
    }
}
