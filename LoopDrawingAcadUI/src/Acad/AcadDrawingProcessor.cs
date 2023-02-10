using System;

namespace LoopDrawingAcadUI
{
    public class AcadDrawingProcessor : IAcadDrawingProcessor
    {
        public void ProcessDrawing(AcadDrawing drawing)
        {
            AcadBlockProcessor blockProcessor = new AcadBlockProcessor(drawing.Database, drawing.Transaction);
            blockProcessor.ProcessBlocks(drawing.AcadDrawingData.Blocks);
            drawing.Save();
        }
    }
}
