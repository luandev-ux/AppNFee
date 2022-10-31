namespace AppNFe.Dominio.DTO.Integracoes.Jobs
{
    public class DTOCancelarTrabalhoAgendado
    {
        public long Codigo { get; set; }
        public string Contratante { get; set; }
        public string RecursoReferencia { get; set; }
        public long CodigoReferencia { get; set; }
    }
}
