using Microsoft.AspNetCore.Http;
using AppNFe.Core.Enumeradores;
using AppNFe.Core.Utilitarios;
using System;
using System.IO;


namespace AppNFe.Core.Arquivos
{
    public class GerenciadorArquivo
    {
        /// <summary>
        /// Define um padrão para o nome dos arquivos seguindo boas práticas de segurança
        /// e também sendo compatível com Links Externos, S3 e Linux
        /// </summary>
        /// <param name="nomeArquivo"></param>
        /// <returns></returns>
        public string GerarNomeArquivo(string nomeArquivo)
        {
            string nomeArquivoPadronizado = "";
            string extensao = "";
            if (!string.IsNullOrEmpty(nomeArquivo))
            {
                nomeArquivo = nomeArquivo.ToLower();
                if (nomeArquivo.Contains("."))
                {
                    extensao = ObtemExtensaoArquivo(nomeArquivo);
                    nomeArquivoPadronizado = UtilitarioTexto.UrlAmigavel(ObtemNomeArquivoSemExtensao(nomeArquivo)) + "." + extensao;
                }
                else
                {
                    nomeArquivoPadronizado = UtilitarioTexto.UrlAmigavel(ObtemNomeArquivoSemExtensao(nomeArquivo));
                }                
            }
            return nomeArquivoPadronizado;
        }

        public string ObtemTamanhoArquivo(long bytes)
        {
            const long OneKB = 1024;
            const long OneMB = OneKB * OneKB;
            const long OneGB = OneMB * OneKB;
            const long OneTB = OneGB * OneKB;

            return bytes switch
            {
                (< OneKB) => $"{bytes}-B",
                (>= OneKB) and (< OneMB) => $"{bytes / OneKB}-KB",
                (>= OneMB) and (< OneGB) => $"{bytes / OneMB}-MB",
                (>= OneGB) and (< OneTB) => $"{bytes / OneMB}-GB",
                (>= OneTB) => $"{bytes / OneTB}"
                //...
            };
        }

        public string ObtemNomeArquivoSemExtensao(string nomeArquivo)
        {
            string nomeArquivoSemExtensao = "";
            if (!string.IsNullOrEmpty(nomeArquivo))
            {
                if (nomeArquivo.Contains("."))
                {
                    int lastIndex = nomeArquivo.LastIndexOf('.');
                    if (lastIndex > 0)
                        nomeArquivoSemExtensao = nomeArquivo.Substring(0, lastIndex);
                }
            }
            return nomeArquivoSemExtensao;
        }

        public string ObtemExtensaoArquivo(string nomeArquivo)
        {
            string extensao = "";
            if (!string.IsNullOrEmpty(nomeArquivo))
            {
                if (nomeArquivo.Contains("."))
                {
                    int lastIndex = nomeArquivo.LastIndexOf('.');
                    if (lastIndex > 0)
                        extensao = nomeArquivo.Substring(lastIndex + 1).ToLower();
                }
            }
            return extensao;
        }

        public string ObtemNomeDiretorioEspecifico(ERelacaoArquivo relacaoArquivo)
        {
            string diretorioEspecifico = "";

            switch (relacaoArquivo)
            {
                case ERelacaoArquivo.Sistema:
                    diretorioEspecifico = "sistema";
                    break;
                case ERelacaoArquivo.Anexos:
                    diretorioEspecifico = "anexos";
                    break;
                case ERelacaoArquivo.Relatorios:
                    diretorioEspecifico = "relatorios";
                    break;
                case ERelacaoArquivo.DocumentosFiscais:
                    diretorioEspecifico = "documentos-fiscais";
                    break;
                case ERelacaoArquivo.Financeiro:
                    diretorioEspecifico = "financeiro";
                    break;
                case ERelacaoArquivo.ImportacaoArquivos:
                    diretorioEspecifico = "importacao-arquivos";
                    break;
                default:
                    diretorioEspecifico = "";
                    break;
            }
            return diretorioEspecifico;
        }

        public string CriaNomeUnicoDeArquivoPorUsuario(string texto, long codigoUsuario)
        {
            return UtilitarioTexto.UrlAmigavel(texto) + "-d" + DateTime.Now.ToString("ddMMyyHHmmss") + "u" + codigoUsuario;
        }
        public string ObtemExtensaoTipoArquivo(ETipoArquivo tipoArquivo)
        {
            string extensao = "";

            switch (tipoArquivo)
            {
                case ETipoArquivo.PDF:
                    extensao = ".pdf";
                    break;
                case ETipoArquivo.XML:
                    extensao = ".xml";
                    break;
                case ETipoArquivo.Word:
                    extensao = ".docx";
                    break;
                case ETipoArquivo.Excel:
                    extensao = ".xlsx";
                    break;
                case ETipoArquivo.CSV:
                    extensao = ".csv";
                    break;
                case ETipoArquivo.Texto:
                    extensao = ".txt";
                    break;
                case ETipoArquivo.JPG:
                    extensao = ".jpg";
                    break;
                case ETipoArquivo.PNG:
                    extensao = ".png";
                    break;
                case ETipoArquivo.SVG:
                    extensao = ".svg";
                    break;                
            }            

            return extensao;
        }

        public string ObtemDiretorioServidorTipoArquivo(ERelacaoArquivo relacaoArquivo)
        {
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ObtemNomeDiretorioEspecifico(relacaoArquivo));

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            return caminho;            
        }

        public string MontaDiretorioArquivo(string diretorio, string nomeArquivo)
        {            
            return Path.Combine(diretorio, nomeArquivo);            
        }

        public string MontaDiretorioTemporarioContratante(string contratante, ERelacaoArquivo relacaoArquivo)
        {
            string diretorioTemporarioContratante = "";

            if (string.IsNullOrEmpty(contratante))
                return diretorioTemporarioContratante;

            diretorioTemporarioContratante = Path.Combine(ObtemNomeDiretorioEspecifico(relacaoArquivo), "temp", contratante);            

            return diretorioTemporarioContratante;
        }    

        public string ObtemDiretorioServidor(string diretorio)
        {                                  
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", diretorio);

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            return caminho;
        }

        private string ArmazenarArquivo(IFormFile arquivo, string caminho)
        {
            if (arquivo == null)
                return "";
            
            string nomeArquivo = Path.GetFileName(arquivo.FileName);
            string extensaoArquivo = Path.GetExtension(arquivo.FileName).ToLower();            
            string caminhoCompletoArquivo = Path.Combine(caminho, nomeArquivo);

            if (File.Exists(caminhoCompletoArquivo))
            {
                string nomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(arquivo.FileName);

                for (int i = 1; ; ++i)
                {
                    if (!File.Exists(caminhoCompletoArquivo))
                        break;

                    nomeArquivo = nomeArquivoSemExtensao + "-" + i + extensaoArquivo;
                    caminhoCompletoArquivo = Path.Combine(caminho, nomeArquivo);
                }
            }
            using (var stream = new FileStream(caminhoCompletoArquivo, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }
            return caminhoCompletoArquivo;
        }

        public string UploadArquivoTemporario(string contratante, ERelacaoArquivo relacaoArquivo, IFormFile arquivo)
        {            
            string diretorioContratante = MontaDiretorioTemporarioContratante(contratante, relacaoArquivo);
            var caminho = ObtemDiretorioServidor(diretorioContratante);
             
            return ArmazenarArquivo(arquivo, caminho);
        }

        public bool CopiarArquivoServidor(string diretorioOrigem, string nomeArquivoOrigem, string diretorioDestino, string nomeArquivoDestino)
        {
            try
            {
                if (!Directory.Exists(diretorioOrigem))
                    Directory.CreateDirectory(diretorioOrigem);

                if (!Directory.Exists(diretorioDestino))
                    Directory.CreateDirectory(diretorioDestino);

                File.Copy(Path.Combine(diretorioOrigem, nomeArquivoOrigem), Path.Combine(diretorioDestino, nomeArquivoDestino), true);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
