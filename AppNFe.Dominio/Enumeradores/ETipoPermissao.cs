using AppNFe.Core;
using AppNFe.Core.Enumeradores;
using System.Collections.Generic;

namespace AppNFe.Dominio.Enumeradores
{
    public enum ETipoPermissao
    {
        Dados = 1,
        Autorizacoes = 2,
        Widgets = 3,
        Relatorios = 4
    }

    public static class ETipoPermissaoExtensions
    {
        public static string Descricao(this ETipoPermissao enumerador)
        {
            string descricao = "";

            switch (enumerador)
            {
                case ETipoPermissao.Dados:
                    descricao = "Dados";
                    break;

                case ETipoPermissao.Autorizacoes:
                    descricao = "Autorizações";
                    break;

                case ETipoPermissao.Widgets:
                    descricao = "Widgets";
                    break;

                case ETipoPermissao.Relatorios:
                    descricao = "Relatórios";
                    break;
            }

            return descricao;
        }

        public static List<ItemGenerico> ObterLista()
        {
            List<ItemGenerico> lista = new List<ItemGenerico>
            {
                new ItemGenerico { TipoDadoIdentificador = ETipoDado.Inteiro, Identificador = ((int)ETipoPermissao.Dados).ToString(), Descricao = ETipoPermissao.Dados.Descricao()},
                new ItemGenerico { TipoDadoIdentificador = ETipoDado.Inteiro, Identificador = ((int)ETipoPermissao.Autorizacoes).ToString(), Descricao = ETipoPermissao.Autorizacoes.Descricao()},
                new ItemGenerico { TipoDadoIdentificador = ETipoDado.Inteiro, Identificador = ((int)ETipoPermissao.Widgets).ToString(), Descricao = ETipoPermissao.Widgets.Descricao()},
                new ItemGenerico { TipoDadoIdentificador = ETipoDado.Inteiro, Identificador = ((int)ETipoPermissao.Relatorios).ToString(), Descricao = ETipoPermissao.Relatorios.Descricao()},
            };

            return lista;
        }
    }
}
