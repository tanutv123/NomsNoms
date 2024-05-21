using ClosedXML.Excel;
using NomsNoms.DTOs;

namespace NomsNoms.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionAdminDTO>> GetAllTransactionAdmin();
        Task<XLWorkbook> ExportTransactionList();
    }
}