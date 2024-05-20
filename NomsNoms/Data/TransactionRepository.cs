using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TransactionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TransactionAdminDTO>> GetAllTransactionAdmin()
        {
            List<TransactionAdminDTO> list = null;
            try
            {
                var l = await _context.Transactions.Include(t => t.Sender).ToListAsync();
                list = _mapper.Map<List<TransactionAdminDTO>>(l);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
