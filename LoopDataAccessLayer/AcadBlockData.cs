using LoopDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockDataMappable : IMappable
    {
        /* Maybe this won't be abstract - maybe this is a concrete class as I think all blocks will be hte same */
        /* NO - the whole point is that MapData is different for each and every block */
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        protected readonly DataLoader dataLoader;

        public BlockDataMappable(DataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public abstract void MapData(); // maps the block data
    }

    public abstract class BlockDataExcel : BlockDataMappable
    {

        public BlockDataExcel(DataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchExcelData();
        }

        protected abstract void FetchExcelData();
    }


    public abstract class BlockDataDB : BlockDataMappable
    {
        public BlockDataDB(DataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchDBData();
        }

        protected abstract void FetchDBData();
    }


    public abstract class BlockDataExcelDB : BlockDataMappable
    {
        public BlockDataExcelDB(DataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchExcelData(); 
            FetchDBData();
        }

        protected abstract void FetchExcelData();
        protected abstract void FetchDBData();
    }

    public class EMPTY_BLOCK : BlockDataExcel
    {
        public EMPTY_BLOCK(DataLoader dataLoader) : base(dataLoader) { }
        public override void MapData() { }
        protected override void FetchExcelData() { }
    }


    public class JB_3_TERM_SINGLE : BlockDataExcel
    {
        public JB_3_TERM_SINGLE(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var rows = dataLoader
                .ExcelLoader
                ?.GetJBRows(Tag)
                ?.OrderBy(r => ExcelStringHelper.GetRowString(r, ExcelJBColumns.TAG_01));
            if (rows != null)
            {
                List<ClosedXML.Excel.IXLRow> r = rows.ToList();

                Attributes["JB_TAG-1"] = Tag;
                Attributes["JB_TS-1"] = ExcelStringHelper.GetRowString(r[0], ExcelJBColumns.TerminalStrip);
                Attributes["TB1-1"] = ExcelStringHelper.GetRowString(r[0], ExcelJBColumns.Terminal);
                Attributes["TB2-1"] = ExcelStringHelper.GetRowString(r[1], ExcelJBColumns.Terminal);
                Attributes["TB3-1"] = ExcelStringHelper.GetRowString(r[2], ExcelJBColumns.Terminal);
                Attributes["CLR1-L1"] = ExcelStringHelper.GetRowString(r[0], ExcelJBColumns.LeftColor);
                Attributes["CLR2-L2"] = ExcelStringHelper.GetRowString(r[1], ExcelJBColumns.LeftColor);
                Attributes["CLR1-R1"] = ExcelStringHelper.GetRowString(r[0], ExcelJBColumns.RightColor);
                Attributes["CLR2-R2"] = ExcelStringHelper.GetRowString(r[1], ExcelJBColumns.RightColor);
            }
        }
    }


    public class PNL_3_TERM_24VDC : BlockDataExcel
    {
        public PNL_3_TERM_24VDC(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var row = dataLoader.ExcelLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["PNL_TAG"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.PANEL_TAG);
                Attributes["PNL_TS"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.PNL_TS_01);
                Attributes["TB1"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.PNL_TB_01);
                Attributes["TB2"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.PNL_TB_02);
                Attributes["TB3"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.PNL_TB_03);
                Attributes["CLR1"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.IOClrPlus);
                Attributes["CLR2"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.IOClrNeg);
                Attributes["PAIR_NO"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.IOCorePairPlus) + "PR";
                Attributes["WIRE_TAG_PANEL"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.IOWireTagPlus);
                Attributes["CABLE_TAG_PANEL"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.IOCableTag);
                Attributes["BREAKER_NO"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.BREAKER_NUM);
            }
        }
    }


    public class PNL_3_TERM : PNL_3_TERM_24VDC
    {
        public PNL_3_TERM(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
            Attributes.Remove("BREAKER_NO");
        }
    }


    public class MOD_1_TERM : BlockDataExcelDB
    {
        public MOD_1_TERM(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.DBLoader.GetLoopData(Tag);

            Attributes["RACK"] = data.Rack;
            Attributes["SLOT"] = data.Slot;
            Attributes["CHANNEL"] = data.Channel;
            

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

            Attributes["WIRE_TAG_IO"] = "AIN-"
                + Attributes["RACK"].PadLeft(3, '0')
                + "."
                + Attributes["SLOT"].PadLeft(2, '0')
                + "."
                + Attributes["CHANNEL"]
                + "+";

            Attributes["DRAWING_NO"] = data.PidDrawingNumber;
        }

        protected override void FetchExcelData()
        {
            var row = dataLoader.ExcelLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["MOD_TERM"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.MOD_TERM_01);
            }
        }
    }


    public class MOD_2_TERM : MOD_1_TERM
    {
        public MOD_2_TERM(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            base.FetchDBData();
            Attributes["WIRE_TAG_IO_1"] = Attributes["WIRE_TAG_IO"].Replace("AIN", "AOUT");
            Attributes["WIRE_TAG_IO_2"] = Attributes["WIRE_TAG_IO_1"].Replace("+", "-");
            Attributes.Remove("WIRE_TAG_IO");
        }

        protected override void FetchExcelData()
        {
            var row = dataLoader.ExcelLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["MOD_TERM1"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.MOD_TERM_01);
                Attributes["MOD_TERM2"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.MOD_TERM_02);
            }
        }
    }


    public class INST_AI_2W : BlockDataExcelDB
    {
        public INST_AI_2W(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.DBLoader.GetLoopData(Tag);

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
            var row = dataLoader.ExcelLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["TERM1"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.DeviceTerminalPlus);
                Attributes["TERM2"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.DeviceTerminalNeg);
                Attributes["CLR1"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.WireColorPlus);
                Attributes["CLR2"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.WireColorNeg);
                Attributes["PAIR_NO"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.CorePairPlus) + "PR";
                Attributes["WIRE_TAG_FIELD"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.WireTagPlus);
                Attributes["CABLE_TAG_FIELD"] = ExcelStringHelper.GetRowString(row, ExcelIOColumns.CableTagField);
            }
        }
    }


    public class INST_AO_2W : INST_AI_2W
    {
        public INST_AO_2W(DataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            base.FetchDBData();
            DBLoopData data = dataLoader.DBLoader.GetLoopData(Tag);

            Attributes.Remove("RANGE");
            Attributes["VALVE_FAIL"] = data.FailPosition; 

        }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
        }
    }

    

}
