using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public interface IExcelColumnProvider
    {
        int GetColumnNumber(string columnName);
    }
    public class ExcelColumnProvider
    {
        private readonly IXLRow headerRow;
        public ExcelColumnProvider(IXLRow header)
        {
            this.headerRow = header;
        }

        public int GetColumnNumber(string columnName)
        {
            int? colNum = headerRow
                    ?.CellsUsed(cell => cell.GetString().ToUpper() == columnName.ToUpper())
                    ?.FirstOrDefault()
                    ?.WorksheetColumn()
                    ?.ColumnNumber();

            if (colNum is null)
            {
                throw new ExcelColumnNotFoundException(columnName);
            }

            return (int)colNum;
        }
    }

    public class ExcelColumnNotFoundException : Exception
    {
        public ExcelColumnNotFoundException()
        {
        }

        public ExcelColumnNotFoundException(string? message) : base(message)
        {
        }

        public ExcelColumnNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExcelColumnNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
