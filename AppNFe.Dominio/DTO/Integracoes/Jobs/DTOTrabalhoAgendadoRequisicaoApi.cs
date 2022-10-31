using AppNFe.Core.Enumeradores;

namespace AppNFe.Dominio.DTO.Integracoes.Jobs
{
    public class DTOTrabalhoAgendadoRequisicaoApi
    {
        public long Codigo { get; set; }
        public long CodigoTrabalhoAgendado { get; set; }        
        public EMetodoHttp TipoMetodo { get; set; }                
        public string UrlBase { get; set; }        
        public string Api { get; set; }        
        public string Rota { get; set; }
        public string Parametros { get; set; }
        public string Cabecalho { get; set; }
        public string Corpo { get; set; }
    }
}
