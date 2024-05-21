using AutoMapper;
using ClosedXML.Excel;
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
        public async Task<XLWorkbook> ExportTransactionList()
        {
            var transactions = await GetAllTransactionAdmin();
            var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Transactions");

            // Thêm tiêu đề cho các cột
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nội dung";
            worksheet.Cell(1, 3).Value = "Ngày tạo";
            worksheet.Cell(1, 4).Value = "Tổng tiền";
            worksheet.Cell(1, 5).Value = "Người gửi";

            int currentRow = 2;
            foreach (var transaction in transactions)
            {
                worksheet.Cell(currentRow, 1).Value = transaction.Id;
                worksheet.Cell(currentRow, 2).Value = transaction.ReportName;
                worksheet.Cell(currentRow, 3).Value = transaction.CreateDate;
                worksheet.Cell(currentRow, 4).Value = transaction.TotalPrice;
                worksheet.Cell(currentRow, 5).Value = transaction.Sender;
                currentRow++;
            }
            worksheet.Columns().AdjustToContents();
            worksheet.Rows().AdjustToContents();
            return workbook;
        }
    }
}
