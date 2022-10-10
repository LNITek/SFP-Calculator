# SFP Calculator

[![NuGet](https://img.shields.io/nuget/v/ExtraFunctions.svg)](https://www.nuget.org/packages/SFPCalculator/)

Satisfactory Production Calculator Is A Library That Does All Of The Hard Work To Calculate A Entire Production Line.

[Chage Log](ChangeLog.md)

## Installation
```
Install-Package SFPCalculator
```

## How To Use

Create A Production Plan.

``` CSharp
SFPPlanner SFPPlaner = new SFPPlanner();
var ProductionPlan = SFPPlaner.Produce(RecipeName, UnitPerMin);
```

[For More Info Look At The Wiki](https://github.com/Tekknow1580/SFP-Calculator/wiki)