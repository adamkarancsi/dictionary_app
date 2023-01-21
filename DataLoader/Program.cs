using OfficeOpenXml;

var existingFile = new FileInfo(args[0]);
using (var package = new ExcelPackage(existingFile))
{
    
}