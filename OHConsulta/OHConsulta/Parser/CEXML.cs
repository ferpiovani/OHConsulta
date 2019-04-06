using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using OHConsulta.DTO;

namespace OHConsulta.Parser
{
    public class CEXML : Parser
    {
        XmlDocument xmlDoc = null;

        public CEXML(String stringXML)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(stringXML);
        }

        public override CEDados GetDados()
        {
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("//EstoqueTanques/Dados");

            DTO.CEDados dados = new CEDados();
            dados.DadosCNPJ = node.SelectSingleNode("CNPJ").InnerText;
            dados.DadosNome = node.SelectSingleNode("Empresa").InnerText;
            dados.Data = Convert.ToDateTime(node.SelectSingleNode("Data").InnerText);
            dados.DadosSenha = node.SelectSingleNode("Senha").InnerText;
            return dados;
        }

        public override List<CETanque> GetTanque()
        {
            List<CETanque> lstTanque = new List<CETanque>();

            XmlNodeList itemNodes = xmlDoc.SelectNodes("//EstoqueTanques/det");
            
            foreach(XmlNode itemNode in itemNodes)
            {
                XmlNode combNode = itemNode.SelectSingleNode("Combustivel");
                XmlNode vendNode = itemNode.SelectSingleNode("Vendido");
                XmlNode qtdANode = itemNode.SelectSingleNode("QuantAnt");
                XmlNode capacNode = itemNode.SelectSingleNode("Capacidade");


                if ((combNode != null) && (vendNode != null) && (qtdANode != null))
                {
                    DTO.CETanque tanque = new DTO.CETanque();
                    tanque.TanqueCombTipo = combNode.InnerText;
                    tanque.TanqueCombVendido = Convert.ToDouble(vendNode.InnerText);
                    tanque.TanqueQtdAnterior = Convert.ToDouble(qtdANode.InnerText);
                    tanque.TanqueCapacidade = Convert.ToInt32(capacNode.InnerText);
                    lstTanque.Add(tanque);
                }
            }
            return lstTanque;
        }

        public override string Path()
        {
            throw new NotImplementedException();
        }

        public override string Type()
        {
            throw new NotImplementedException();
        }
    }
}
