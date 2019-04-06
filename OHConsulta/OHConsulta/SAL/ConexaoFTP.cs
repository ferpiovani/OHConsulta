using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace OHConsulta.SAL
{
    public class ConexaoFTP
    {
        WebClient request = null;
        String url = string.Empty;

        public ConexaoFTP(DTO.Config cfg, String Pasta, String Ext) 
        {
            request = new WebClient();
            this.url = Path.Combine(cfg.ConfigEndereco, Pasta + cfg.ConfigCNPJ + Ext);
            request.Credentials = new NetworkCredential(cfg.ConfigLogin, VariaveisControle.senhaWebServer);
        }

        public String DownloadArquivo()
        {
            try
            {
                byte[] newFileData = request.DownloadData(url);
                if (newFileData != null)
                {
                    return System.Text.Encoding.UTF8.GetString(newFileData);
                }
                else return null;
            } catch (Exception ex)
            {
                return null;
            }

        }

        public static bool CheckInternet()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "8.8.8.8";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckWebService()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "ftp.oficinadohardware.com.br";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
