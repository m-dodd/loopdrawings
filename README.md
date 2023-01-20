# Introduction 

The goal of this project is to create a system to automatically generate loop drawings in AutoCAD from data in the WTEdge database, supplemented by a spreadsheet for wiring and junction box data.

# Structure
AutoCAD object model only supports FrameWork4.8 and as we need to run the program *in-process*, which means, within the AutoCAD exe, there was no way to get Enitity Framework to work directly in this application. We were directed towards potentially creating a rest API for the database, which would have worked, but that didn't feel right.

We gravitated towards a compromise. We have a data access / generation layer that gets all of the data required and generates a file that can be consumed by the AutoCAD applicationt to generate the final drawings.

Currently the program is broken into five individual programs.
1. `WTEdgeEntities` - this layer contains all of hte Entity Framework entities. I've moved the context object to `LoopDataAccessLayer` as it feels more appropriate.
1. `LoopDataAccessLayer` - as the name implied this layer contains all of the logic to get the required data.
2. `LoopDrawingDataUI` - UI for the `LoopDataAccessLayer`.
3. `LoopDrawingAcadUI` - this is the dll that will be created that will provide a minimal front end, and from that create all loop drawings.
4. `LoopDrawingAcadUI` - as there are different versions of .NET being used in the other projects this project exists to bridge the gap between the Data Acess Layer and the AutoCad layer. Any objects that both layers need will live here.

# Contribute
TO-DO - there is probably a lot that others could add to this.