using AppNFe.Core.DominioProblema;
using System;

namespace AppNFe.Core.Utilitarios
{
    public static class UtilitarioRetornoRequisicao
    {
        public static RetornoRequisicao GerarRetorno(bool status)
        {
            if (status)
            {
                return GerarRetornoSucesso();
            }
            else
            {
                return GerarRetornoErro("");
            }
        }

        public static RetornoRequisicao GerarRetorno(EStatusRetornoRequisicao status, string mensagem)
        {
            switch (status)
            {
                case EStatusRetornoRequisicao.Alerta:
                    return GerarRetornoAlerta(mensagem);
                case EStatusRetornoRequisicao.Erro:
                    return GerarRetornoErro(mensagem);
                default:
                    return GerarRetornoSucesso();
            }
        }

        public static RetornoRequisicao GerarRetornoSucesso()
        {
            return new RetornoRequisicao(0, EStatusRetornoRequisicao.Sucesso, "");
        }

        public static RetornoRequisicao GerarRetornoSucesso(long codigoRegistro)
        {
            return new RetornoRequisicao(codigoRegistro, EStatusRetornoRequisicao.Sucesso, "");
        }

        public static RetornoRequisicao GerarRetornoSucesso(string mensagem)
        {
            return new RetornoRequisicao(0, EStatusRetornoRequisicao.Sucesso, mensagem);
        }

        public static RetornoRequisicao GerarRetornoSucesso(long codigoRegistro, string mensagem)
        {
            return new RetornoRequisicao(codigoRegistro, EStatusRetornoRequisicao.Sucesso, mensagem);
        }

        public static RetornoRequisicao GerarRetornoAlerta(string mensagem)
        {
            return new RetornoRequisicao(0, EStatusRetornoRequisicao.Alerta, mensagem);
        }

        public static RetornoRequisicao GerarRetornoErro(string mensagem)
        {
            return new RetornoRequisicao(0, EStatusRetornoRequisicao.Erro, mensagem);
        }

        public static RetornoRequisicao GerarRetornoErro(Exception exception)
        {
            return new RetornoRequisicao(0, EStatusRetornoRequisicao.Erro, exception.Message);
        }
    }
}
