using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockDataMappable : AcadBlockData, IMappableBlock
    {
        protected readonly IDataLoader dataLoader;

        public BlockDataMappable(IDataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public abstract void MapData(); 
    }

    public abstract class BlockDataExcel : BlockDataMappable
    {

        public BlockDataExcel(IDataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchExcelData();
        }

        protected abstract void FetchExcelData();
    }


    public abstract class BlockDataDB : BlockDataMappable
    {
        public BlockDataDB(IDataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchDBData();
        }

        protected abstract void FetchDBData();
    }


    public abstract class BlockDataExcelDB : BlockDataMappable
    {
        public BlockDataExcelDB(IDataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchExcelData(); 
            FetchDBData();
        }

        protected abstract void FetchExcelData();
        protected abstract void FetchDBData();
    }


    public class EMPTY_BLOCK : BlockDataMappable
    {
        public EMPTY_BLOCK(IDataLoader dataLoader) : base(dataLoader) { }
        public override void MapData() { }
    }

    public class TITLE_BLOCK : BlockDataMappable
    {
        public TITLE_BLOCK(IDataLoader dataLoader) : base(dataLoader) 
        {
        }

        public override void MapData() 
        {
            MapTitleBlockData();
        }

        private void MapTitleBlockData()
        {
            if (dataLoader.TitleBlock is not null)
            {
                // populate attributes from titleblockdata
                //Attributes[""] = TitleBlockData.DrawingName;
            }

            throw new NotImplementedException();
        }
    }


    public class JB_3_TERM_SINGLE : BlockDataExcel
    {
        public JB_3_TERM_SINGLE(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var jbRows = dataLoader
                ?.GetJBRows(Tag)
                ?.OrderBy(r => ExcelHelper.GetRowString(r, ExcelJBColumns.TAG_01));
            if (jbRows != null)
            {
                List<ClosedXML.Excel.IXLRow> rows = jbRows.ToList();

                Attributes["JB_TAG-1"] = Tag;
                Attributes["JB_TS-1"] = ExcelHelper.GetRowString(rows[0], ExcelJBColumns.TerminalStrip);
                Attributes["TB1-1"] = ExcelHelper.GetRowString(rows[0], ExcelJBColumns.Terminal);
                Attributes["TB2-1"] = ExcelHelper.GetRowString(rows[1], ExcelJBColumns.Terminal);
                Attributes["TB3-1"] = ExcelHelper.GetRowString(rows[2], ExcelJBColumns.Terminal);
                Attributes["CLR1-L1"] = ExcelHelper.GetRowString(rows[0], ExcelJBColumns.LeftColor);
                Attributes["CLR2-L2"] = ExcelHelper.GetRowString(rows[1], ExcelJBColumns.LeftColor);
                Attributes["CLR1-R1"] = ExcelHelper.GetRowString(rows[0], ExcelJBColumns.RightColor);
                Attributes["CLR2-R2"] = ExcelHelper.GetRowString(rows[1], ExcelJBColumns.RightColor);
            }
        }
    }


    public class PNL_3_TERM_24VDC : BlockDataExcel
    {
        public PNL_3_TERM_24VDC(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var row = dataLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["PNL_TAG"] = ExcelHelper.GetRowString(row, ExcelIOColumns.PANEL_TAG);
                Attributes["PNL_TS"] = ExcelHelper.GetRowString(row, ExcelIOColumns.PNL_TS_01);
                Attributes["TB1"] = ExcelHelper.GetRowString(row, ExcelIOColumns.PNL_TB_01);
                Attributes["TB2"] = ExcelHelper.GetRowString(row, ExcelIOColumns.PNL_TB_02);
                Attributes["TB3"] = ExcelHelper.GetRowString(row, ExcelIOColumns.PNL_TB_03);
                Attributes["CLR1"] = ExcelHelper.GetRowString(row, ExcelIOColumns.IOClrPlus);
                Attributes["CLR2"] = ExcelHelper.GetRowString(row, ExcelIOColumns.IOClrNeg);
                Attributes["PAIR_NO"] = ExcelHelper.GetRowString(row, ExcelIOColumns.IOCorePairPlus) + "PR";
                Attributes["WIRE_TAG_PANEL"] = ExcelHelper.GetRowString(row, ExcelIOColumns.IOWireTagPlus);
                Attributes["CABLE_TAG_PANEL"] = ExcelHelper.GetRowString(row, ExcelIOColumns.IOCableTag);
                Attributes["BREAKER_NO"] = ExcelHelper.GetRowString(row, ExcelIOColumns.BREAKER_NUM);
            }
        }
    }


    public class PNL_3_TERM : PNL_3_TERM_24VDC
    {
        public PNL_3_TERM(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
            Attributes.Remove("BREAKER_NO");
        }
    }


    public class MOD_1_TERM_1_BPCS : BlockDataExcelDB
    {
        public string ControllerTag { get; set; } = string.Empty;

        public MOD_1_TERM_1_BPCS(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            Attributes["RACK"] = data.Rack;
            Attributes["SLOT"] = data.Slot;
            Attributes["CHANNEL"] = data.Channel;


            string[] tagComponents = ControllerTag.Split('-');
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

            

            Attributes["DRAWING_NO"] = data.PidDrawingNumber;
        }

        protected override void FetchExcelData()
        {
            var row = dataLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["MOD_TERM"] = ExcelHelper.GetRowString(row, ExcelIOColumns.MOD_TERM_01);
                Attributes["WIRE_TAG_IO"] = ExcelHelper.GetRowString(row, ExcelIOColumns.MOD_WIRE_TAG_01);
            }
        }
    }


    public class MOD_2_TERM_1_BPCS : MOD_1_TERM_1_BPCS
    {
        public MOD_2_TERM_1_BPCS(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var row = dataLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["MOD_TERM1"] = ExcelHelper.GetRowString(row, ExcelIOColumns.MOD_TERM_01);
                Attributes["MOD_TERM2"] = ExcelHelper.GetRowString(row, ExcelIOColumns.MOD_TERM_02);
                Attributes["WIRE_TAG_IO-1"] = ExcelHelper.GetRowString(row, ExcelIOColumns.MOD_WIRE_TAG_01);
                Attributes["WIRE_TAG_IO-2"] = ExcelHelper.GetRowString(row, ExcelIOColumns.MOD_WIRE_TAG_02);
                Attributes.Remove("WIRE_TAG_IO");
            }
        }
    }


    public class INST_AI_2W : BlockDataExcelDB
    {
        public INST_AI_2W(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            Attributes["MANUFACTURER"] = data.Manufacturer;
            Attributes["MODEL"] = data.Model;

            if (data.MinCalRange != DBLoopData.CALERROR.ToString() && data.MaxCalRange != DBLoopData.CALERROR.ToString())
            {
                Attributes["RANGE"] = data.MinCalRange + "-" + data.MaxCalRange;
            }
            else
            {
                // this entire else statement needs to be removed, but for testing I want to see a value in that attribute
                Attributes["RANGE"] = "ERROR";
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
            var row = dataLoader?.GetIORow(Tag);
            if (row != null)
            {
                Attributes["TERM1"] = ExcelHelper.GetRowString(row, ExcelIOColumns.DeviceTerminalPlus);
                Attributes["TERM2"] = ExcelHelper.GetRowString(row, ExcelIOColumns.DeviceTerminalNeg);
                Attributes["CLR1"] = ExcelHelper.GetRowString(row, ExcelIOColumns.WireColorPlus);
                Attributes["CLR2"] = ExcelHelper.GetRowString(row, ExcelIOColumns.WireColorNeg);
                Attributes["PAIR_NO"] = ExcelHelper.GetRowString(row, ExcelIOColumns.CorePairPlus) + "PR";
                Attributes["WIRE_TAG_FIELD"] = ExcelHelper.GetRowString(row, ExcelIOColumns.WireTagPlus);
                Attributes["CABLE_TAG_FIELD"] = ExcelHelper.GetRowString(row, ExcelIOColumns.CableTagField);
            }
        }
    }


    public class INST_AO_2W : INST_AI_2W
    {
        public INST_AO_2W(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            base.FetchDBData();
            DBLoopData data = dataLoader.GetLoopData(Tag);

            Attributes.Remove("RANGE");
            Attributes["VALVE_FAIL"] = data.FailPosition; 

        }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();
        }
    }

    public class BUTTERFLY_DIAPHRAGM : BlockDataDB
    {
        public BUTTERFLY_DIAPHRAGM(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);
            string[] tagComponents = Tag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["TAG1"] = tagComponents[0];
                Attributes["TAG2"] = tagComponents[1];
            }
            // NOTE - Can't find SIZE right now so this is just going to be Fail position
            Attributes["SIZE/FAIL_POSITION"] = data.FailPosition;
        }
    }

}
