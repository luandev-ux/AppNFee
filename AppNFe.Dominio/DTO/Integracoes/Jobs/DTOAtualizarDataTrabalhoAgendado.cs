using AppNFe.Core.Enumeradores;

namespace AppNFe.Dominio.DTO.Integracoes.Jobs
{
    public class DTOAtualizarDataTrabalhoAgendado
    {        
        public long Codigo { get; set; }                
        public string Contratante { get; set; }               
        public string RecursoReferencia { get; set; }        
        public long CodigoReferencia { get; set; }        
        public int DiaSemana { get; set; }
        public int Dia { get; set; }        
        public int Mes { get; set; }        
        public int Ano { get; set; }        
        public int Hora { get; set; }        
        public int Minuto { get; set; }
        
    }
}
