using System;
using System.Collections.Generic;
using System.Text;

namespace OHConsulta.Parser
{
    public abstract class Parser
    {
        protected String dir;
        protected String file;

        public abstract DTO.CEDados GetDados();
        public abstract List<DTO.CETanque> GetTanque();

        public abstract String Path();
        public abstract String Type();
    }
}
