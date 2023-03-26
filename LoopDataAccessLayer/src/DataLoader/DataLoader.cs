using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    

    public class DataLoader : IDataLoader
    {
        private readonly IExcelDataLoader excelLoader;
        private readonly IDBLoader dbLoader;

        private readonly Dictionary<string, DBLoopData> loopData;
        private readonly Dictionary<string, IEnumerable<LoopTagData>> loopTagData;
        private readonly Dictionary<string, List<SDKData>> sdkData;

        private readonly Dictionary<string, IXLRow?> ioRowData;
        private readonly Dictionary<string, IXLRows?> jbRowsData;

        private readonly IDictionary<string, IExcelIOData<string>?> ioData;
        private readonly IDictionary<string, List<ExcelJBData>?> jbData;
        private readonly IDictionary<string, IExcelCableData<string>?> cableData;

        public IExcelJBRowData<int> ExcelJBCols { get; private set; }
        public IExcelIOData<int> ExcelIOCols { get; private set; }

        //private readonly ILogger logger;

        //public DataLoader(IExcelLoader excelLoader, IDBLoader dbLoader, ILogger logger)
        public DataLoader(IExcelDataLoader excelLoader, IDBLoader dbLoader)
        {
            this.excelLoader = excelLoader ?? throw new ArgumentNullException(nameof(excelLoader));
            this.dbLoader = dbLoader ?? throw new ArgumentNullException(nameof(dbLoader));
            
            loopData = new Dictionary<string, DBLoopData>();
            loopTagData = new Dictionary<string, IEnumerable<LoopTagData>>();
            sdkData = new Dictionary<string, List<SDKData>>();

            ioRowData = new Dictionary<string, IXLRow?>();
            jbRowsData = new Dictionary<string, IXLRows?>();

            ioData = new Dictionary<string, IExcelIOData<string>?>();
            jbData = new Dictionary<string, List<ExcelJBData>?>();
            cableData = new Dictionary<string, IExcelCableData<string>?>();

            ExcelJBCols = this.excelLoader.ExcelJBCols;
            ExcelIOCols = this.excelLoader.ExcelIOCols;
            //logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<LoopNoTemplatePair> GetLoops()
        {
            //logger.LogInformation("Getting loops from the database");
            return dbLoader.GetLoops();
        }

        public List<SDKData> GetSDs(string tag)
        {
            if (sdkData.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                sdkData[tag] = dbLoader.GetSDs(tag);
                return sdkData[tag];
            }

        }

        public IEnumerable<LoopTagData> GetLoopTags(LoopNoTemplatePair loop)
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
            return GetDataOrMemoizeGetData<DBLoopData>(tag, loopData!, dbLoader.GetLoopData)!;
        }

        public IXLRow? GetIORow(string tag)
        {
            //logger.LogInformation("Getting IO row from the excel");
            return GetDataOrMemoizeGetData<IXLRow?>(tag, ioRowData, excelLoader.GetIORow);
        }

        public IXLRows? GetJBRows(string tag)
        {
            return GetDataOrMemoizeGetData<IXLRows?>(tag, jbRowsData, excelLoader.GetJBRows);
        }

        public IExcelIOData<string>? GetIOData(string tag)
        {
            return GetDataOrMemoizeGetData<IExcelIOData<string>?>(tag, ioData, excelLoader.GetIOData);
        }

        public List<ExcelJBData>? GetJBData(string tag)
        {
            return GetDataOrMemoizeGetData<List<ExcelJBData>?>(tag, jbData, excelLoader.GetJBData);
        }

        public IExcelTitleBlockData<string> GetTitleBlockData()
        {
            return excelLoader.GetTitleBlockData();
        }

        public IExcelCableData<string>? GetCableData(string tag)
        {
            return GetDataOrMemoizeGetData<IExcelCableData<string>?>(tag, this.cableData, excelLoader.GetCableData);
        }

        private T? GetDataOrMemoizeGetData<T>(
            string tag, IDictionary<string, T?> cache, Func<string, T?> dataGettingFunc
        )
        {
            if (cache.TryGetValue(tag, out var data))
            {
                return data;
            }
            else
            {
                data = dataGettingFunc(tag);
                cache.Add(tag, data);
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