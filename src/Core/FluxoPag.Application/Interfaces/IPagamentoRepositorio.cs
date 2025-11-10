using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluxoPag.Domain.Entities;

namespace FluxoPag.Application.Interfaces
{
    public interface IPagamentoRepositorio
    {
        Task AddAsync(Pagamento pagamentos);
        Task<Pagamento> GetByIdAsync(Guid id);
        Task UpdateAsync(Pagamento pagamento);
    }
}
