using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace LoopDataAccessLayer
{
    

    public class DataLoader : IDataLoader
    {
        public TitleBlockData TitleBlock { get; set; }

        private readonly IExcelLoader excelLoader;
        private readonly IDBLoader dbLoader;
        private readonly Dictionary<string, DBLoopData> loopData;
        private readonly Dictionary<string, List<LoopTagData>> loopTagData;
        private readonly Dictionary<string, IXLRow?> ioRowData;
        private readonly Dictionary<string, IXLRows?> jbRowsData;
        private readonly IDictionary<string, IExcelIOData<string>?> ioData;
        private readonly IDictionary<string, List<ExcelJBData>?> jbData;

        public IExcelJBRowData<int> ExcelJBCols { get; private set; }
        public IExcelIOData<int> ExcelIOCols { get; private set; }
        //private readonly ILogger logger;

        //public DataLoader(IExcelLoader excelLoader, IDBLoader dbLoader, ILogger logger)
        public DataLoader(IExcelLoader excelLoader, IDBLoader dbLoader, TitleBlockData titleBlock)
        {
            this.excelLoader = excelLoader ?? throw new ArgumentNullException(nameof(excelLoader));
            this.dbLoader = dbLoader ?? throw new ArgumentNullException(nameof(dbLoader));
            this.TitleBlock = titleBlock;
            loopData = new Dictionary<string, DBLoopData>();
            loopTagData = new Dictionary<string, List<LoopTagData>>();
            ioRowData = new Dictionary<string, IXLRow?>();
            jbRowsData = new Dictionary<string, IXLRows?>();
            ioData = new Dictionary<string, IExcelIOData<string>?>();
            jbData = new Dictionary<string, List<ExcelJBData>?>();

            ExcelJBCols = this.excelLoader.ExcelJBCols;
            ExcelIOCols = this.excelLoader.ExcelIOCols;
            //logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<LoopNoTemplatePair> GetLoops()
        {
            //logger.LogInformation("Getting loops from the database");
            return dbLoader.GetLoops();
        }

        public List<LoopTagData> GetLoopTags(LoopNoTemplatePair loop)
        {
            //logger.LogInformation("Getting loop tags from the database");
            if (loopTagData.TryGetValue(loop.LoopNo, out var data))
            {
                return data;
            }
            else
            {
                data = dbLoader.GetLoopTags(loop);
                loopTagData.Add(loop.LoopNo, data);
                return data;
            }
        }

        public DBLoopData GetLoopData(string tag)
        {
            //logger.LogInformation("Getting loop data from the database");
            if (loopData.TryGetValue(tag, out var data))
            {
                //logger.LogInformation("Data retrieved from cache");
                return data;
            }
            else
            {
                //logger.LogInformation("Data not found in cache, fetching from the database");
                data = dbLoader.GetLoopData(tag);
                loopData.Add(tag, data);
                return data;
            }
        }

        public IXLRow? GetIORow(string tag)
        {
            //logger.LogInformation("Getting IO row from the excel");
            if (ioRowData.TryGetValue(tag, out var data))
            {
                //logger.LogInformation("Data retrieved from cache");
                return data;
            }
            else
            {
                //logger.LogInformation("Data not found in cache, fetching from the excel");
                data = excelLoader.GetIORow(tag);
                ioRowData.Add(tag, data);
                return data;
            }
        }

        public IXLRows? GetJBRows(string tag)
        {
            //logger.LogInformation("Getting JB rows from the excel");
            if (jbRowsData.TryGetValue(tag, out var data))
            {
                //logger.LogInformation("Data retrieved from cache");
                return data;
            }
            else
            {
                //logger.LogInformation("Data not found in cache, fetching from the excel");
                data = excelLoader.GetJBRows(tag);
                jbRowsData.Add(tag, data);
                return data;
            }
        }

        public IExcelIOData<string>? GetIOData(string tag)
        {
            //logger.LogInformation("Getting IO Data from the excel");
            if (ioData.TryGetValue(tag, out var data))
            {
                //logger.LogInformation("Data retrieved from cache");
                return data;
            }
            else
            {
                //logger.LogInformation("Data not found in cache, fetching from the excel");
                data = excelLoader.GetIOData(tag);
                ioData.Add(tag, data);
                return data;
            }
        }

        public List<ExcelJBData>? GetJBData(string tag)
        {
            if (jbData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = excelLoader.GetJBData(tag);
                jbData.Add(tag, data);
                return data;
            }
        }
    }
        
}

// an example of how to instantiate and set up logging
/*
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.File;

class Program
{
    static void Main(string[] args)
    {
        var excelFileName = "path/to/excel/file.xlsx";
        var excelLoader = new ExcelDataLoader(excelFileName);

        var dbLoader = new DBDataLoader();

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("Program", LogLevel.Debug)
                .AddFile("logs/data-access.log");
        });
        var logger = loggerFactory.CreateLogger<DataLoader>();

        var dataLoader = new DataLoader(excelLoader, dbLoader, logger);
    }
}
*/