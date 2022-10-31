using AppNFe.Core.Enumeradores;

namespace AppNFe.Dominio.Relatorios
{
    public class ConfiguracaoRelatorio
    {        
        public string Titulo { get; set; }
        public string NomeArquivo { get; set; }
        public ETipoArquivo TipoArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string DiretorioServidor { get; set; }
        public string DiretorioServidorRelatorios { get; set; }
        public string DiretorioContratante { get; set; }
        public string UrlBaseApi { get; set; }
        public bool VariasEmpresas { get; set; }
        public string Empresa { get; set; }
        public bool Paisagem { get; set; }
    }
}
