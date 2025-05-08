﻿using CRM.Core.Entities;

namespace CRM.Core.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();

        Task<Invoice> GetByIdAsync(Guid id);

        Task AddAsync(Invoice invoice);

        Task UpdateAsync(Invoice invoice);

        Task DeleteAsync(Guid id);
    }
}
