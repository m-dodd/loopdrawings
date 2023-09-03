using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class TITLE_BLOCK : BlockDataExcel
    {
        public string DescriptionTag { get; set; } = string.Empty;
        public TITLE_BLOCK(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
            DescriptionTag = tagMap[blockMap.Tags[1]];
        }

        protected override void FetchExcelData()
        {
            IExcelTitleBlockData<string> titleBlockData = dataLoader.GetTitleBlockData();
            var data = dataLoader.GetLoopTagData(DescriptionTag);

            Attributes["TITLE_1"] = titleBlockData.GeneralRevData.Description;
            Attributes["TITLE_2"] = data.LoopNo + " LOOP DIAGRAM";
            Attributes["TITLE_3"] = data.Description;

            Attributes["Drawing No.:"] = Tag;
            Attributes["SHTNO"] = titleBlockData.Sheet;
            Attributes["SHTOF"] = titleBlockData.MaxSheets;
            Attributes["PROJECT"] = titleBlockData.Project;
            
            Attributes["REV"] = titleBlockData.GeneralRevData.Rev;
            Attributes["DATE"] = titleBlockData.GeneralRevData.Date;
            Attributes["DWN"] = titleBlockData.GeneralRevData.DrawnBy;
            Attributes["CHK"] = titleBlockData.GeneralRevData.CheckedBy;
            Attributes["APPD"] = titleBlockData.GeneralRevData.ApprovedBy;
            
            Attributes["R1"] = titleBlockData.RevBlockRevData.Rev;
            Attributes["DR_1"] = titleBlockData.RevBlockRevData.Date;
            Attributes["DESC_R1"] = titleBlockData.RevBlockRevData.Description;
            Attributes["REVD_R1"] = titleBlockData.RevBlockRevData.DrawnBy;
            Attributes["CHK_R1"] = titleBlockData.RevBlockRevData.CheckedBy;
            Attributes["APD_R1"] = titleBlockData.RevBlockRevData.ApprovedBy;

            Attributes["LOCATION-CITY/TOWN"] = titleBlockData.CityTown;
            Attributes["LOCATION-PROVINCE/STATE"] = titleBlockData.ProvinceState;
        }

    }
}
