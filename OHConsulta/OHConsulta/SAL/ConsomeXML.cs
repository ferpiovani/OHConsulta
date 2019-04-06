using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OHConsulta.SAL
{
    public class ConsomeXML
    {
        String arquivoString = string.Empty;

        public ConsomeXML(String arquivoString)
        {
            this.arquivoString = arquivoString;
        }

        private XmlDocument StringParaXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(arquivoString);
            return xmlDoc;
        }

        private void GravarDados(XmlDocument xmlDoc)
        {
            if (xmlDoc != null)
            {
                XmlNode dadosNode = xmlDoc.SelectSingleNode("//EstoqueTanques/Dados");
                if (dadosNode != null)
                {
                    new DAL.CEDados().Limpar();
                    XmlNode cnpjNode = dadosNode.SelectSingleNode("CNPJ");
                    XmlNode empresaNode = dadosNode.SelectSingleNode("Empresa");
                    XmlNode senhaNode = dadosNode.SelectSingleNode("Senha");
                    XmlNode dataNode = dadosNode.SelectSingleNode("Data");

                    if (cnpjNode != null && empresaNode != null && senhaNode != null && dataNode != null)
                    {
                        DTO.CEDados dados = new DTO.CEDados();
                        dados.DadosCNPJ = cnpjNode.InnerText;
                        dados.DadosNome = empresaNode.InnerText;
                        dados.DadosSenha = senhaNode.InnerText;
                        dados.Data = Convert.ToDateTime(dataNode.InnerText);
                        new DAL.CEDados().Inserir(dados);
                    }
                }
            }
        }

        private void GravarEstoque(XmlDocument xmlDoc)
        {
            if (xmlDoc != null)
            {
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//EstoqueTanques/det");
                new DAL.CETanque().Limpar();
                foreach (XmlNode itemNode in itemNodes)
                {
                    XmlNode combNode = itemNode.SelectSingleNode("Combustivel");
                    XmlNode vendNode = itemNode.SelectSingleNode("Vendido");
                    XmlNode vendValorNode = itemNode.SelectSingleNode("VendidoValor");
                    XmlNode qtdANode = itemNode.SelectSingleNode("QuantAnt");
                    XmlNode capacNode = itemNode.SelectSingleNode("Capacidade");
                    if ((combNode != null) && (vendNode != null) && (qtdANode != null))
                    {
                        DTO.CETanque tanque = new DTO.CETanque();
                        tanque.TanqueCombTipo = combNode.InnerText;
                        tanque.TanqueCombVendido = Convert.ToDouble(vendNode.InnerText);
                        tanque.TanqueCombVendidoValor = Convert.ToDouble(vendValorNode.InnerText);
                        tanque.TanqueQtdAnterior = Convert.ToDouble(qtdANode.InnerText);
                        tanque.TanqueCapacidade = Convert.ToInt32(capacNode.InnerText);
                        new DAL.CETanque().Inserir(tanque);
                    }
                }
            }
        }

        public void Recarregar()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc = StringParaXML();
            GravarDados(xmlDoc);
            GravarEstoque(xmlDoc);
        }
    }
}
