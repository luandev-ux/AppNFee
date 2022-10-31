using AppNFe.Core.DominioProblema;
using System.Collections.Generic;
using System.Data;

namespace NeoXBI.Core.DominioProblema
{
    public class RetornoConsulta<T>
    {
        public bool Status { get; set; }
        public string Mensagem { get; set; }
        public DataRowCollection DataRow { get; set; }
        public List<T> Registros { get; set; }

        public RetornoConsulta(List<T> registros)
        {
            Status = true;
            Registros = registros;
        }

        public RetornoConsulta(DataRowCollection dataRow)
        {
            Status = true;
            DataRow = dataRow;
        }

        public RetornoConsulta(string mensagem)
        {
            Status = false;
            Mensagem = mensagem;
        }

        public RetornoConsulta()
        {
        }

        public Retorno ObterRetorno()
        {
            return new Retorno(Status, Mensagem, 0);
        }
    }
}
