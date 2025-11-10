using FluxoPag.Application.Commands;
using FluxoPag.Application.Interfaces;
using FluxoPag.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoPag.Application.Handlers
{
    public class CriarComandoDePagamentoHandler : IRequestHandler<CriarComandoDePagamento, Guid>
    {
        private readonly IGatewayPagamento _gatewayPagamento;
        private readonly IPagamentoRepositorio _pagamentoRepositorio;
        public CriarComandoDePagamentoHandler(IGatewayPagamento gatewayPagamento, IPagamentoRepositorio pagamentoRepositorio)
        {
            _gatewayPagamento = gatewayPagamento;
            _pagamentoRepositorio = pagamentoRepositorio;
        }
        public async Task<Guid> Handle(CriarComandoDePagamento request, CancellationToken cancellationToken)
        {
            var pagamento = new Pagamento(
                request.TitularCartao, 
                request.NumeroCartao, 
                request.Expira, 
                request.Cvc, 
                request.Valor);

            await _pagamentoRepositorio.AddAsync(pagamento);
            var aprovado = await _gatewayPagamento.ProcessarPagamento(
                request.TitularCartao, 
                request.NumeroCartao, 
                request.Expira, 
                request.Valor, 
                request.Cvc);

            if (aprovado)
            {
                pagamento.Aprovado();
            }
            else
            {
                pagamento.Recusado();
            }
            await _pagamentoRepositorio.UpdateAsync(pagamento);
            return pagamento.Id;
        }
    }
}
