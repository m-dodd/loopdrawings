using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    //public class AcadDrawingDataFactory
    //{
    //    private readonly DBDataLoader dbLoader;
    //    private readonly ExcelDataLoader excelLoader;
    //    private readonly AcadBlockFactory _blockFactory;

    //    public AcadDrawingDataFactory(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
    //    {
    //        this.dbLoader = dbLoader;
    //        this.excelLoader = excelLoader;
    //        _blockFactory = new AcadBlockFactory(this.dbLoader, this.excelLoader);
    //    }

    //    public LoopDrawingData GetLoop(string TemplateID, string Tag)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void GetAllBlocks(TemplateConfig template)
    //    {
    //        foreach (KeyValuePair<string, string> b in template.BlockMap)
    //        {
    //            _blockFactory.GetBlock(b);
    //        }
    //    }
    //}
}
