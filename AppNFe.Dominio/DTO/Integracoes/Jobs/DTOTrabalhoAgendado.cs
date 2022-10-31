using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNFe.Dominio.DTO.Integracoes.Jobs
{
    public class DTOTrabalhoAgendado
    {        
        public long Codigo { get; set; }        
        public DateTime DataCriacao { get; set; }
        public int Tipo { get; set; }        
        public string Contratante { get; set; }        
        public string RecursoReferencia { get; set; }        
        public long CodigoReferencia { get; set; }        
        public int FormaExecucao { get; set; }
        public int DiaSemana { get; set; }        
        public int Dia { get; set; }        
        public int Mes { get; set; }        
        public int Ano { get; set; }        
        public int Hora { get; set; }        
        public int Minuto { get; set; }        
        public int Tentativas { get; set; }        
        public int Status { get; set; }        
        public DTOTrabalhoAgendadoRequisicaoApi RequisicaoApi { get; set; }
        public DTOTrabalhoAgendado()
        {
            RequisicaoApi = new DTOTrabalhoAgendadoRequisicaoApi();
        }
    }


}
