using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    // With this factory the drawing can just look at the block names defined in the template json config file
    // and instantiate the correct block
    // the block will have the correct map defined in it's version of the MapData() function
    // and finally the list of blocks can just look and call the MapData funcion of each block as built above
    //
    // I think this is beginning to come together
    public class BlockFactory
    {
        private readonly DBDataLoader dbLoader;
        private readonly ExcelDataLoader excelLoader;

        public BlockFactory(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        {
            this.dbLoader = dbLoader;
            this.excelLoader = excelLoader;
        }

        public BlockDataMappable GetBlock(string blockName, string Tag)
        {
            switch (blockName)
            {
                case "JB_3_TERM_SINGLE":
                    return new JB_3_TERM_SINGLE(this.dbLoader, this.excelLoader) { Name=blockName, Tag=Tag };
                //case "B":
                    //return new SomeOtherBlock() { Name=blockName, Tag=Tag };
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class JB_3_TERM_SINGLE : BlockDataMappable
    {
        private readonly DBDataLoader dbLoader;
        private readonly ExcelDataLoader excelLoader;


        public JB_3_TERM_SINGLE(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        {
            this.dbLoader = dbLoader;
            this.excelLoader = excelLoader;
        }

        public override void MapData()
        {
            FetchExcelData();
            //FetchDBData();
        }

        private void FetchExcelData()
        {
            var rows = excelLoader
                ?.GetJBRows(Tag)
                ?.OrderBy(r => ExcelStringHelper.GetJBRowString(r, "TAG_01"));
            if (rows != null)
            {
                List<ClosedXML.Excel.IXLRow> r = rows.ToList();

                Attributes["JB_TAG-1"] = Tag;
                Attributes["JB_TS-1"] = ExcelStringHelper.GetJBRowString(r[0], "TerminalStrip");
                Attributes["TB1-1"] = ExcelStringHelper.GetJBRowString(r[0], "Terminal");
                Attributes["TB2-1"] = ExcelStringHelper.GetJBRowString(r[1], "Terminal");
                Attributes["TB3-1"] = ExcelStringHelper.GetJBRowString(r[2], "Terminal");
                Attributes["CLR1-L1"] = ExcelStringHelper.GetJBRowString(r[0], "LeftColor");
                Attributes["CLR2-L2"] = ExcelStringHelper.GetJBRowString(r[1], "LeftColor");
                Attributes["CLR1-R1"] = ExcelStringHelper.GetJBRowString(r[0], "RightColor");
                Attributes["CLR2-R2"] = ExcelStringHelper.GetJBRowString(r[1], "RightColor");
            }
        }

        private void FetchDBData()
        {
            throw new NotImplementedException();
        }
    }
}
