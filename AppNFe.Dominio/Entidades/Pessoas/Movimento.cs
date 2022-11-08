using AppNFe.Core.MensagemPadronizada;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppNFe.Dominio.Entidades.Pessoas
{
    public class Movimento
    {
        #region Propriedades

        #region Codigo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public long Codigo { get; set; }
        #endregion
        #region Codigo Pessoa
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [ForeignKey("pessoa")]
        public long CodigoPessoa { get; set; }
        #endregion
        #region Data
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public DateTime Data { get; set; }
        #endregion
        #region Numero
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(5, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Numero { get; set; }
        #endregion
        #region Serie
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Serie { get; set; }
        #endregion
        #region Modelo
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Modelo { get; set; }
        #endregion
        #region Valor Total
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double ValorTotal { get; set; }
        #endregion
        #region Valor Desconto
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Desconto { get; set; }
        #endregion
        #region Outras Despesas
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double OutrasDespesas { get; set; }
        #endregion
        #region Frete
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Frete { get; set; }
        #endregion
        #region Chave de Acesso
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(44, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string ChaveDeAcesso { get; set; }
        #endregion
        #region Tipo de Movimento
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string TipoMovimentacao { get; set; } // 0 - Entrada, 1 - Saida
        #endregion

        #endregion
        public List<Pessoa> Pessoas { get; set; }

        public Movimento()
        {
            Pessoas = new List<Pessoa>();

        }
    }
}