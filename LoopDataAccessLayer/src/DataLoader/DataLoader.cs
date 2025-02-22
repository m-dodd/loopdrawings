﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using WTEdge.Entities;

namespace LoopDataAccessLayer
{
    public interface IDataLoader : IExcelDataLoader, IDBLoader
    {
    }

    public class DataLoader : IDataLoader
    {
        private readonly IExcelDataLoader excelLoader;
        private readonly IDBLoader dbLoader;

        private readonly Dictionary<string, DBLoopData> loopData;
        private readonly Dictionary<string, IEnumerable<LoopTagData>> loopTagData;
        private readonly Dictionary<string, List<SDKData>> sdkTagDataCache;
        private readonly Dictionary<string, List<SDKData>> sdkLoopDataCache;

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
            sdkTagDataCache = new Dictionary<string, List<SDKData>>();
            sdkLoopDataCache = new Dictionary<string, List<SDKData>>();

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

        public IEnumerable<LoopTagData> GetLoopTags(LoopNoTemplatePair loop)
        {
            return GetLoopTags(loop.LoopNo);
        }

        public IEnumerable<LoopTagData> GetLoopTags(string loopNo)
        {
            //logger.LogInformation("Getting loop tags from the database");
            if (loopTagData.TryGetValue(loopNo, out var data))
            {
                return data;
            }
            else
            {
                data = dbLoader.GetLoopTags(loopNo);
                loopTagData.Add(loopNo, data);
                return data;
            }
        }

        public List<SDKData> GetSDsForTag(string tag)
        {
            return GetDataOrMemoizeGetData<List<SDKData>>(tag, sdkTagDataCache!, dbLoader.GetSDsForTag)!;
        }

        public List<SDKData> GetSDsForLoop(string loopNo)
        {
            return GetDataOrMemoizeGetData<List<SDKData>>(loopNo, sdkLoopDataCache!, dbLoader.GetSDsForLoop)!;
        }


        public DBLoopData GetLoopTagData(string tag)
        {
            //logger.LogInformation("Getting loop data from the database");
            return GetDataOrMemoizeGetData<DBLoopData>(tag, loopData!, dbLoader.GetLoopTagData)!;
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