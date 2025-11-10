using FluxoPag.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoPag.Domain.Entities
{
    public class Pagamento
    {
        public Pagamento(string titularCartao, string numeroCartao, string expira, string cvc, decimal valor ) 
        {
            Id = Guid.NewGuid();
            TitularCartao = titularCartao;
            NumeroCartao = "XXXX-XXXX-XXXX-" + numeroCartao.Substring(numeroCartao.Length - 4);
            Valor = valor;
            Status = PagamentoStatus.Pendente;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string TitularCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public decimal Valor { get; private set; }
        public PagamentoStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ProcessedAt { get; private set; }

        public void Aprovado()
        {
            Status = PagamentoStatus.Aprovado;
            ProcessedAt = DateTime.UtcNow;
        }

        public void Recusado()
        {
            Status = PagamentoStatus.Recusado;
            ProcessedAt = DateTime.UtcNow;
        }
    }
}
