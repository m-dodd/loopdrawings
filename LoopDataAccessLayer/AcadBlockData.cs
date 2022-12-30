using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockDataExcel : BlockDataMappable
    {
        protected readonly ExcelDataLoader excelLoader;

        public BlockDataExcel(ExcelDataLoader excelLoader)
        {
            this.excelLoader = excelLoader;
        }

        public override void MapData()
        {
            FetchExcelData();
        }

        protected abstract void FetchExcelData();
    }


    public abstract class BlockDataDB : BlockDataMappable
    {
        protected readonly DBDataLoader dbLoader;

        public BlockDataDB(DBDataLoader dbLoader)
        {
            this.dbLoader = dbLoader;
        }

        public override void MapData()
        {
            FetchDBData();
        }

        protected abstract void FetchDBData();
    }


    public abstract class BlockDataExcelDB : BlockDataMappable
    {
        protected readonly DBDataLoader dbLoader;
        protected readonly ExcelDataLoader excelLoader;

        public BlockDataExcelDB(ExcelDataLoader excelLoader, DBDataLoader dbLoader)
        {
            this.excelLoader = excelLoader; 
            this.dbLoader = dbLoader;
        }

        public override void MapData()
        {
            FetchExcelData(); 
            FetchDBData();
        }

        protected abstract void FetchExcelData();
        protected abstract void FetchDBData();
    }


    public class JB_3_TERM_SINGLE : BlockDataExcel
    {
        public JB_3_TERM_SINGLE(ExcelDataLoader excelLoader) : base(excelLoader) { }

        protected override void FetchExcelData()
        {
            var rows = excelLoader
                ?.GetJBRows(Tag)
                ?.OrderBy(r => ExcelStringHelper.GetJBRowString(r, ExcelJBColumns.TAG_01));
            if (rows != null)
            {
                List<ClosedXML.Excel.IXLRow> r = rows.ToList();

                Attributes["JB_TAG-1"] = Tag;
                Attributes["JB_TS-1"] = ExcelStringHelper.GetJBRowString(r[0], ExcelJBColumns.TerminalStrip);
                Attributes["TB1-1"] = ExcelStringHelper.GetJBRowString(r[0], ExcelJBColumns.Terminal);
                Attributes["TB2-1"] = ExcelStringHelper.GetJBRowString(r[1], ExcelJBColumns.Terminal);
                Attributes["TB3-1"] = ExcelStringHelper.GetJBRowString(r[2], ExcelJBColumns.Terminal);
                Attributes["CLR1-L1"] = ExcelStringHelper.GetJBRowString(r[0], ExcelJBColumns.LeftColor);
                Attributes["CLR2-L2"] = ExcelStringHelper.GetJBRowString(r[1], ExcelJBColumns.LeftColor);
                Attributes["CLR1-R1"] = ExcelStringHelper.GetJBRowString(r[0], ExcelJBColumns.RightColor);
                Attributes["CLR2-R2"] = ExcelStringHelper.GetJBRowString(r[1], ExcelJBColumns.RightColor);
            }
        }
    }


    public class PNL_3_TERM_24VDC : BlockDataExcel
    {
        public PNL_3_TERM_24VDC(ExcelDataLoader excelLoader) : base(excelLoader) { }

        protected override void FetchExcelData()
        {
            var row = excelLoader.GetIORow(Tag);
            if (row != null)
            {
                Attributes["PNL_TAG"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.PANEL_TAG);
                Attributes["PNL_TS"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.PNL_TS_01);
                Attributes["TB1"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.PNL_TB_01);
                Attributes["TB2"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.PNL_TB_02);
                Attributes["TB3"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.PNL_TB_03);
                Attributes["CLR1"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.IOClrPlus);
                Attributes["CLR2"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.IOClrNeg);
                Attributes["PAIR_NO"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.IOCorePairPlus) + "PR";
                Attributes["WIRE_TAG_PANEL"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.IOWireTagPlus);
                Attributes["CABLE_TAG_PANEL"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.IOCableTag);
                Attributes["BREAKER_NO"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.BREAKER_NUM);
            }
        }
    }


    public class MOD_1_TERM : BlockDataDB
    {
        public MOD_1_TERM(DBDataLoader dbLoader) : base(dbLoader)
        {
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dbLoader.GetLoopData(Tag);

            Attributes["RACK"] = data.Rack;
            Attributes["SLOT"] = data.Slot;
            Attributes["CHANNEL"] = data.Channel;
            Attributes["MOD_TERM"] = data.ModTerm;

            string[] tagComponents = Tag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["FUNCTIONAL_ID"] = tagComponents[0];
                Attributes["LOOP_NO"] = tagComponents[1];
            }

            Attributes["ALARM1"] = data.HiHiAlarm;
            Attributes["ALARM2"] = data.HiAlarm;
            Attributes["ALARM3"] = data.LoAlarm;
            Attributes["ALARM4"] = data.LoLoAlarm;
            Attributes["ALARM5"] = data.HiControl;
            Attributes["ALARM6"] = data.LoControl;

            Attributes["WIRETAG_IO"] = "AIN-"
                + Attributes["RACK"].PadLeft(3, '0')
                + "."
                + Attributes["SLOT"].PadLeft(2, '0')
                + "."
                + Attributes["CHANNEL"];

            Attributes["DRAWING_NO"] = data.DrawingNumber;
        }
    }


    public class INST_AI_2W : BlockDataExcelDB
    {
        public INST_AI_2W(ExcelDataLoader excelLoader, DBDataLoader dbLoader) : base(excelLoader, dbLoader)
        {
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dbLoader.GetLoopData(Tag);

            Attributes["MANUFACTURER"] = data.Manufacturer;
            Attributes["MODEL"] = data.Model;

            if (data.MinCalRange != DBLoopData.CALERROR.ToString() && data.MaxCalRange != DBLoopData.CALERROR.ToString())
            {
                Attributes["RANGE"] = data.MinCalRange + "-" + data.MaxCalRange;
            }
            
            // TODO: Break the description up into multiple lines
            Attributes["DESCRIPTION_LINE1"] = data.Description;
            Attributes["DESCRIPTION_LINE2"] = string.Empty;
            Attributes["DESCRIPTION_LINE3"] = string.Empty;
            Attributes["DESCRIPTION_LINE4"] = string.Empty;
            
            string[] tagComponents = Tag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["TAG1"] = tagComponents[0];
                Attributes["TAG2"] = tagComponents[1];
            }
        }

        protected override void FetchExcelData()
        {
            var row = excelLoader.GetIORow(Tag);
            if (row != null)
            {
                Attributes["TERM1"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.DeviceTerminalPlus);
                Attributes["TERM2"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.DeviceTerminalNeg);
                Attributes["CLR1"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.WireColorPlus);
                Attributes["CLR2"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.WireColorNeg);
                Attributes["PAIR_NO"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.CorePairPlus) + "PR";
                Attributes["WIRE_TAG_FIELD"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.WireTagPlus);
                Attributes["CABLE_TAG_FIELD"] = ExcelStringHelper.GetIORowString(row, ExcelIOColumns.CableTagField);
            }
        }
    }

}
