using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using KnightFrank.MemfusWongData.Api.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KnightFrank.MemfusWongData.Api.Helpers
{
    public static class ExcelHelper
    {
        //public static class XLSToXLSXConverter
        //{
        //    public static byte[] Convert(Stream sourceStream)
        //    {
        //        var source = new NPOI.HSSF.UserModel.HSSFWorkbook(sourceStream);
        //        var destination = new NPOI.XSSF.UserModel.XSSFWorkbook();
        //        for (int i = 0; i < source.NumberOfSheets; i++)
        //        {
        //            var xssfSheet = (NPOI.XSSF.UserModel.XSSFSheet)destination.CreateSheet(source.GetSheetAt(i).SheetName);
        //            var hssfSheet = (NPOI.HSSF.UserModel.HSSFSheet)source.GetSheetAt(i);
        //            CopyStyles(hssfSheet, xssfSheet);
        //            CopySheets(hssfSheet, xssfSheet);
        //        }
        //        using (var ms = new MemoryStream())
        //        {
        //            destination.Write(ms);
        //            return ms.ToArray();
        //        }
        //    }

        //    private static void CopyStyles(NPOI.HSSF.UserModel.HSSFSheet from, NPOI.XSSF.UserModel.XSSFSheet to)
        //    {
        //        for (short i = 0; i <= from.Workbook.NumberOfFonts; i++)
        //        {
        //            CopyFont(to.Workbook.CreateFont(), from.Workbook.GetFontAt(i));
        //        }

        //        for (short i = 0; i < from.Workbook.NumCellStyles; i++)
        //        {
        //            CopyStyle(to.Workbook.CreateCellStyle(), from.Workbook.GetCellStyleAt(i), to.Workbook, from.Workbook);
        //        }
        //    }

        //    private static void CopyFont(NPOI.SS.UserModel.IFont toFront, NPOI.SS.UserModel.IFont fontFrom)
        //    {
        //        toFront.Boldweight = fontFrom.Boldweight;
        //        toFront.Charset = fontFrom.Charset;
        //        toFront.Color = fontFrom.Color;
        //        toFront.FontHeightInPoints = fontFrom.FontHeightInPoints;
        //        toFront.FontName = fontFrom.FontName;
        //        toFront.IsBold = fontFrom.IsBold;
        //        toFront.IsItalic = fontFrom.IsItalic;
        //        toFront.IsStrikeout = fontFrom.IsStrikeout;
        //        //toFront.Underline = fontFrom.Underline; <- bug in npoi setter
        //    }

        //    private static void CopyStyle(NPOI.SS.UserModel.ICellStyle toCellStyle, NPOI.SS.UserModel.ICellStyle fromCellStyle,
        //        NPOI.SS.UserModel.IWorkbook toWorkbook, NPOI.SS.UserModel.IWorkbook fromWorkbook)
        //    {
        //        toCellStyle.Alignment = fromCellStyle.Alignment;
        //        toCellStyle.BorderBottom = fromCellStyle.BorderBottom;
        //        toCellStyle.BorderDiagonal = fromCellStyle.BorderDiagonal;
        //        toCellStyle.BorderDiagonalColor = fromCellStyle.BorderDiagonalColor;
        //        toCellStyle.BorderDiagonalLineStyle = fromCellStyle.BorderDiagonalLineStyle;
        //        toCellStyle.BorderLeft = fromCellStyle.BorderLeft;
        //        toCellStyle.BorderRight = fromCellStyle.BorderRight;
        //        toCellStyle.BorderTop = fromCellStyle.BorderTop;
        //        toCellStyle.BottomBorderColor = fromCellStyle.BottomBorderColor;
        //        toCellStyle.DataFormat = fromCellStyle.DataFormat;
        //        toCellStyle.FillBackgroundColor = fromCellStyle.FillBackgroundColor;
        //        toCellStyle.FillForegroundColor = fromCellStyle.FillForegroundColor;
        //        toCellStyle.FillPattern = fromCellStyle.FillPattern;
        //        toCellStyle.Indention = fromCellStyle.Indention;
        //        toCellStyle.IsHidden = fromCellStyle.IsHidden;
        //        toCellStyle.IsLocked = fromCellStyle.IsLocked;
        //        toCellStyle.LeftBorderColor = fromCellStyle.LeftBorderColor;
        //        toCellStyle.RightBorderColor = fromCellStyle.RightBorderColor;
        //        toCellStyle.Rotation = fromCellStyle.Rotation;
        //        toCellStyle.ShrinkToFit = fromCellStyle.ShrinkToFit;
        //        toCellStyle.TopBorderColor = fromCellStyle.TopBorderColor;
        //        toCellStyle.VerticalAlignment = fromCellStyle.VerticalAlignment;
        //        toCellStyle.WrapText = fromCellStyle.WrapText;

        //        toCellStyle.SetFont(toWorkbook.GetFontAt((short)(fromCellStyle.GetFont(fromWorkbook).Index + 1)));
        //    }

        //    private static void CopySheets(NPOI.HSSF.UserModel.HSSFSheet source, NPOI.XSSF.UserModel.XSSFSheet destination)
        //    {
        //        var maxColumnNum = 0;
        //        var mergedRegions = new List<NPOI.SS.Util.CellRangeAddress>();
        //        var styleMap = new Dictionary<int, NPOI.HSSF.UserModel.HSSFCellStyle>();
        //        for (int i = source.FirstRowNum; i <= source.LastRowNum; i++)
        //        {
        //            var srcRow = (NPOI.HSSF.UserModel.HSSFRow)source.GetRow(i);
        //            var destRow = (NPOI.XSSF.UserModel.XSSFRow)destination.CreateRow(i);
        //            if (srcRow != null)
        //            {
        //                CopyRow(source, destination, srcRow, destRow, mergedRegions);
        //                if (srcRow.LastCellNum > maxColumnNum)
        //                {
        //                    maxColumnNum = srcRow.LastCellNum;
        //                }
        //            }
        //        }
        //        for (int i = 0; i <= maxColumnNum; i++)
        //        {
        //            destination.SetColumnWidth(i, source.GetColumnWidth(i));
        //        }
        //    }

        //    private static void CopyRow(NPOI.HSSF.UserModel.HSSFSheet srcSheet, NPOI.XSSF.UserModel.XSSFSheet destSheet,
        //        NPOI.HSSF.UserModel.HSSFRow srcRow, NPOI.XSSF.UserModel.XSSFRow destRow,
        //        List<NPOI.SS.Util.CellRangeAddress> mergedRegions)
        //    {
        //        destRow.Height = srcRow.Height;

        //        for (int j = srcRow.FirstCellNum; srcRow.LastCellNum >= 0 && j <= srcRow.LastCellNum; j++)
        //        {
        //            var oldCell = (NPOI.HSSF.UserModel.HSSFCell)srcRow.GetCell(j);
        //            var newCell = (NPOI.XSSF.UserModel.XSSFCell)destRow.GetCell(j);
        //            if (oldCell != null)
        //            {
        //                if (newCell == null)
        //                {
        //                    newCell = (NPOI.XSSF.UserModel.XSSFCell)destRow.CreateCell(j);
        //                }

        //                CopyCell(oldCell, newCell);

        //                var mergedRegion = GetMergedRegion(srcSheet, srcRow.RowNum, (short)oldCell.ColumnIndex);
        //                if (mergedRegion != null)
        //                {
        //                    var newMergedRegion = new NPOI.SS.Util.CellRangeAddress(mergedRegion.FirstRow, mergedRegion.LastRow, mergedRegion.FirstColumn, mergedRegion.LastColumn);
        //                    if (IsNewMergedRegion(newMergedRegion, mergedRegions))
        //                    {
        //                        mergedRegions.Add(newMergedRegion);
        //                        destSheet.AddMergedRegion(newMergedRegion);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    private static void CopyCell(NPOI.HSSF.UserModel.HSSFCell oldCell, NPOI.XSSF.UserModel.XSSFCell newCell)
        //    {
        //        CopyCellStyle(oldCell, newCell);
        //        CopyCellValue(oldCell, newCell);
        //    }

        //    private static void CopyCellValue(NPOI.HSSF.UserModel.HSSFCell oldCell, NPOI.XSSF.UserModel.XSSFCell newCell)
        //    {
        //        switch (oldCell.CellType)
        //        {
        //            case NPOI.SS.UserModel.CellType.String:
        //                newCell.SetCellValue(oldCell.StringCellValue);
        //                break;
        //            case NPOI.SS.UserModel.CellType.Numeric:
        //                newCell.SetCellValue(oldCell.NumericCellValue);
        //                break;
        //            case NPOI.SS.UserModel.CellType.Blank:
        //                newCell.SetCellType(NPOI.SS.UserModel.CellType.Blank);
        //                break;
        //            case NPOI.SS.UserModel.CellType.Boolean:
        //                newCell.SetCellValue(oldCell.BooleanCellValue);
        //                break;
        //            case NPOI.SS.UserModel.CellType.Error:
        //                newCell.SetCellErrorValue(oldCell.ErrorCellValue);
        //                break;
        //            case NPOI.SS.UserModel.CellType.Formula:
        //                newCell.SetCellFormula(oldCell.CellFormula);
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    private static void CopyCellStyle(NPOI.HSSF.UserModel.HSSFCell oldCell, NPOI.XSSF.UserModel.XSSFCell newCell)
        //    {
        //        if (oldCell.CellStyle == null) return;
        //        newCell.CellStyle = newCell.Sheet.Workbook.GetCellStyleAt((short)(oldCell.CellStyle.Index + 1));
        //    }

        //    private static NPOI.SS.Util.CellRangeAddress GetMergedRegion(NPOI.HSSF.UserModel.HSSFSheet sheet, int rowNum, short cellNum)
        //    {
        //        for (var i = 0; i < sheet.NumMergedRegions; i++)
        //        {
        //            var merged = sheet.GetMergedRegion(i);
        //            if (merged.IsInRange(rowNum, cellNum))
        //            {
        //                return merged;
        //            }
        //        }
        //        return null;
        //    }

        //    private static bool IsNewMergedRegion(NPOI.SS.Util.CellRangeAddress newMergedRegion, List<NPOI.SS.Util.CellRangeAddress> mergedRegions)
        //    {
        //        return !mergedRegions.Any(r =>
        //            r.FirstColumn == newMergedRegion.FirstColumn &&
        //            r.LastColumn == newMergedRegion.LastColumn &&
        //            r.FirstRow == newMergedRegion.FirstRow &&
        //            r.LastRow == newMergedRegion.LastRow);
        //    }
        //}

        static readonly Regex dateTimeFormatRegex = new(@"((?=([^[]*\[[^[\]]*\])*([^[]*[ymdhs]+[^\]]*))|.*\[(h|mm|ss)\].*)", RegexOptions.Compiled);
        static readonly uint[] builtInDateTimeNumberFormatIDs = new uint[] { 14, 15, 16, 17, 18, 19, 20, 21, 22, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 45, 46, 47, 50, 51, 52, 53, 54, 55, 56, 57, 58 };
        static readonly Dictionary<uint, NumberingFormat> builtInDateTimeNumberFormats = builtInDateTimeNumberFormatIDs.ToDictionary(id => id, id => new NumberingFormat { NumberFormatId = id });

        private static Dictionary<uint, NumberingFormat> GetDateTimeCellFormats(WorkbookPart workbookPart)
        {
            var dateNumberFormats = workbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats
                .Descendants<NumberingFormat>()
                .Where(nf => dateTimeFormatRegex.Match(nf.FormatCode.Value).Success)
                .ToDictionary(nf => nf.NumberFormatId.Value);

            var cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats
                .Descendants<CellFormat>();

            var dateCellFormats = new Dictionary<uint, NumberingFormat>();
            uint styleIndex = 0;
            foreach (var cellFormat in cellFormats)
            {
                if (cellFormat.ApplyNumberFormat != null && cellFormat.ApplyNumberFormat.Value)
                {
                    if (dateNumberFormats.ContainsKey(cellFormat.NumberFormatId.Value))
                    {
                        dateCellFormats.Add(styleIndex, dateNumberFormats[cellFormat.NumberFormatId.Value]);
                    }
                    else if (builtInDateTimeNumberFormats.ContainsKey(cellFormat.NumberFormatId.Value))
                    {
                        dateCellFormats.Add(styleIndex, builtInDateTimeNumberFormats[cellFormat.NumberFormatId.Value]);
                    }
                }

                styleIndex++;
            }

            return dateCellFormats;
        }

        private static bool IsDateTimeCell(WorkbookPart workbookPart, Cell cell)
        {
            if (cell.StyleIndex == null)
                return false;

            var dateTimeCellFormats = GetDateTimeCellFormats(workbookPart);

            return dateTimeCellFormats.ContainsKey(cell.StyleIndex);
        }

        public static object GetCellValue(WorkbookPart wbPart, Cell cell)
        {
            if (cell == null)
                return null;

            if (cell.CellValue == null)
                return null;

            var sstp = wbPart.GetPartsOfType<SharedStringTablePart>().First();
            var items = sstp.SharedStringTable.Elements<SharedStringItem>().ToArray();

            var value = cell.CellValue.Text;

            if (cell.DataType != null && cell.DataType.HasValue)
            {
                switch (cell.DataType.Value)
                {
                    case CellValues.Boolean:
                        break;
                    case CellValues.Date:
                        break;
                    case CellValues.Error:
                        break;
                    case CellValues.InlineString:
                        break;
                    case CellValues.Number:
                        break;
                    case CellValues.SharedString:
                        return items[int.Parse(value)].InnerText;
                    case CellValues.String:
                        break;
                }
            }
            else
            {
                if (IsDateTimeCell(wbPart, cell))
                {
                    return DateTime.FromOADate(double.Parse(value));
                }
            }

            return value;
        }

        // Given a cell name, parses the specified cell to get the column name.
        public static string GetColumnName(string cellName)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new("[A-Za-z]+");
            Match match = regex.Match(cellName);

            return match.Value;
        }

        // Given a cell name, parses the specified cell to get the row index.
        public static uint GetRowIndex(string cellName)
        {
            // Create a regular expression to match the row index portion the cell name.
            Regex regex = new(@"\d+");
            Match match = regex.Match(cellName);

            return uint.Parse(match.Value);
        }

        /// <summary>
        /// Sets a cell value. The row and the cell are created if they do not exist. If the cell exists, the contents of the cell is overwritten
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="valueType">Type of the value</param>
        /// <param name="value">The actual value</param>
        /// <param name="styleIndex">Index of the style to use. Null if no style is to be defined</param>
        /// <param name="save">Save the worksheet?</param>
        /// <returns>True if succesful</returns>
        private static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, CellValues valueType, string value, uint? styleIndex, bool save = true)
        {
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            Row row;
            Row previousRow = null;
            Cell cell;
            Cell previousCell = null;
            Columns columns;
            Column previousColumn = null;
            string cellAddress = ColumnNameFromIndex(columnIndex) + rowIndex;

            // Check if the row exists, create if necessary
            if (sheetData.Elements<Row>().Any(item => item.RowIndex == rowIndex))
            {
                row = sheetData.Elements<Row>().Where(item => item.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                //sheetData.Append(row);
                for (uint counter = rowIndex - 1; counter > 0; counter--)
                {
                    previousRow = sheetData.Elements<Row>().Where(item => item.RowIndex == counter).FirstOrDefault();
                    if (previousRow != null)
                    {
                        break;
                    }
                }
                sheetData.InsertAfter(row, previousRow);
            }

            // Check if the cell exists, create if necessary
            if (row.Elements<Cell>().Any(item => item.CellReference.Value == cellAddress))
            {
                cell = row.Elements<Cell>().Where(item => item.CellReference.Value == cellAddress).First();
            }
            else
            {
                // Find the previous existing cell in the row
                for (uint counter = columnIndex - 1; counter > 0; counter--)
                {
                    previousCell = row.Elements<Cell>().Where(item => item.CellReference.Value == ColumnNameFromIndex(counter) + rowIndex).FirstOrDefault();
                    if (previousCell != null)
                    {
                        break;
                    }
                }
                cell = new Cell() { CellReference = cellAddress };
                row.InsertAfter(cell, previousCell);
            }

            // Check if the column collection exists
            columns = worksheet.Elements<Columns>().FirstOrDefault();
            if (columns == null)
            {
                columns = worksheet.InsertAt(new Columns(), 0);
            }
            // Check if the column exists
            if (!columns.Elements<Column>().Any(item => item.Min == columnIndex))
            {
                // Find the previous existing column in the columns
                for (uint counter = columnIndex - 1; counter > 0; counter--)
                {
                    previousColumn = columns.Elements<Column>().Where(item => item.Min == counter).FirstOrDefault();
                    if (previousColumn != null)
                    {
                        break;
                    }
                }
                columns.InsertAfter(
                   new Column()
                   {
                       Min = columnIndex,
                       Max = columnIndex,
                       CustomWidth = true,
                       Width = 9
                   }, previousColumn);
            }

            // Add the value
            cell.CellValue = new CellValue(value);
            if (styleIndex != null)
            {
                cell.StyleIndex = styleIndex;
            }
            if (valueType != CellValues.Date)
            {
                cell.DataType = new EnumValue<CellValues>(valueType);
            }

            if (save)
            {
                worksheet.Save();
            }

            return true;
        }

        /// <summary>
        /// Creates the workbook
        /// </summary>
        /// <returns>Spreadsheet created</returns>
        public static SpreadsheetDocument CreateWorkbook(MemoryStream memoryStream)
        {
            SharedStringTablePart sharedStringTablePart;
            WorkbookStylesPart workbookStylesPart;

            // Create the Excel workbook
            SpreadsheetDocument spreadSheet = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, false);

            // Create the parts and the corresponding objects

            // Workbook
            spreadSheet.AddWorkbookPart();
            spreadSheet.WorkbookPart.Workbook = new Workbook();
            spreadSheet.WorkbookPart.Workbook.Save();

            // Shared string table
            sharedStringTablePart = spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
            sharedStringTablePart.SharedStringTable = new SharedStringTable();
            sharedStringTablePart.SharedStringTable.Save();

            // Sheets collection
            spreadSheet.WorkbookPart.Workbook.Sheets = new Sheets();
            spreadSheet.WorkbookPart.Workbook.Save();

            // Stylesheet
            workbookStylesPart = spreadSheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            workbookStylesPart.Stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet.Save();

            return spreadSheet;
        }

        public static Worksheet GetWorksheet(SpreadsheetDocument document, string worksheetName)
        {
            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
            if (!sheets.Any())
                return null;
            else
                return worksheetPart.Worksheet;
        }

        /// <summary>
        /// Adds a new worksheet to the workbook
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="name">Name of the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool AddWorksheet(SpreadsheetDocument spreadsheet, string name)
        {
            Sheets sheets = spreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            Sheet sheet;
            WorksheetPart worksheetPart;

            // Add the worksheetpart
            worksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());
            worksheetPart.Worksheet.Save();

            // Add the sheet and make relation to workbook
            sheet = new Sheet()
            {
                Id = spreadsheet.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = (uint)(spreadsheet.WorkbookPart.Workbook.Sheets.Count() + 1),
                Name = name
            };
            sheets.Append(sheet);
            spreadsheet.WorkbookPart.Workbook.Save();

            return true;
        }

        /// <summary>
        /// Adds the basic styles to the workbook
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <returns>True if succesful</returns>
        public static bool AddBasicStyles(SpreadsheetDocument spreadsheet)
        {
            Stylesheet stylesheet = spreadsheet.WorkbookPart.WorkbookStylesPart.Stylesheet;

            // Numbering formats (x:numFmts)
            stylesheet.InsertAt(new NumberingFormats(), 0);
            stylesheet.GetFirstChild<NumberingFormats>().InsertAt(
                new NumberingFormat()
                {
                    NumberFormatId = 164,
                    FormatCode = "#,##0.00"
                    //+ "\\ \"" + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + "\""
                }, 0); // Currency
            stylesheet.GetFirstChild<NumberingFormats>().InsertAt(
                new NumberingFormat()
                {
                    NumberFormatId = 165,
                    FormatCode = StringValue.FromString("yyyy-mm-dd")
                }, 1); // Date
            stylesheet.GetFirstChild<NumberingFormats>().InsertAt(
                new NumberingFormat()
                {
                    NumberFormatId = 166,
                    FormatCode = "\\ \"" + "$" + "\"" + "#,##0"
                }, 2); // Currency with Symbol

            // Fonts (x:fonts)
            stylesheet.InsertAt(new Fonts(), 1);
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 11
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    }
                }, 0);   // Index 0 - The default font.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 11
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Bold = new Bold()
                    {
                        Val = true
                    },
                }, 1);   // Index 1 - The bold font.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 16
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Bold = new Bold()
                    {
                        Val = true
                    },
                }, 2);   // Index 2 - The bold font with 16 size.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 11
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Bold = new Bold()
                    {
                        Val = true
                    },
                    Underline = new Underline()
                    {
                        Val = UnderlineValues.Single
                    },
                }, 3);   // Index 3 - The bold and undeline font.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 12
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Bold = new Bold()
                    {
                        Val = true
                    },
                }, 4);   // Index 4 - The bold font with 12 size.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 11
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Underline = new Underline()
                    {
                        Val = UnderlineValues.Single
                    }
                }, 5);   // Index 5 - The undeline font.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 8
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    }
                }, 6);   // Index 6 - The 8 size.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 8
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Underline = new Underline()
                    {
                        Val = UnderlineValues.Single
                    }
                }, 7);   // Index 7 - The undeline, 8 size.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 8
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Strike = new Strike()
                    {
                        Val = true
                    }
                }, 8);   // Index 8 - The strike, 8 size.
            stylesheet.GetFirstChild<Fonts>().InsertAt(
                new Font()
                {
                    FontSize = new FontSize()
                    {
                        Val = 8
                    },
                    FontName = new FontName()
                    {
                        Val = "Calibri"
                    },
                    Bold = new Bold()
                    {
                        Val = true
                    }
                }, 9);   // Index 9 - The bold, 8 size.

            // Fills (x:fills)
            stylesheet.InsertAt(new Fills(), 2);
            stylesheet.GetFirstChild<Fills>().InsertAt(
                new Fill()
                {
                    PatternFill = new PatternFill()
                    {
                        PatternType = new EnumValue<PatternValues>()
                        {
                            Value = PatternValues.None
                        }
                    }
                }, 0);
            stylesheet.GetFirstChild<Fills>().InsertAt(
                new Fill()
                {
                    PatternFill = new PatternFill()
                    {
                        PatternType = new EnumValue<PatternValues>()
                        {
                            Value = PatternValues.Gray125
                        }
                    }
                }, 1);
            stylesheet.GetFirstChild<Fills>().InsertAt(
                new Fill()
                {
                    PatternFill = new PatternFill()
                    {
                        PatternType = new EnumValue<PatternValues>()
                        {
                            Value = PatternValues.Solid
                        },
                        ForegroundColor = new ForegroundColor()
                        {
                            Rgb = "d9d9d9"
                        },
                        BackgroundColor = new BackgroundColor()
                        {
                            //Indexed = (UInt32Value)64U
                        }
                    }
                }, 2);

            // Borders (x:borders)
            stylesheet.InsertAt(new Borders(), 3);
            stylesheet.GetFirstChild<Borders>().InsertAt(
                new Border()
                {
                    LeftBorder = new LeftBorder(),
                    RightBorder = new RightBorder(),
                    TopBorder = new TopBorder(),
                    BottomBorder = new BottomBorder(),
                    DiagonalBorder = new DiagonalBorder()
                }, 0);
            stylesheet.GetFirstChild<Borders>().InsertAt(
                new Border()
                {
                    LeftBorder = new LeftBorder() { Style = BorderStyleValues.Thin },
                    RightBorder = new RightBorder() { Style = BorderStyleValues.Thin },
                    TopBorder = new TopBorder() { Style = BorderStyleValues.Thin },
                    BottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin },
                    DiagonalBorder = new DiagonalBorder()
                }, 1);
            stylesheet.GetFirstChild<Borders>().InsertAt(
                new Border()
                {
                    LeftBorder = new LeftBorder() { Style = BorderStyleValues.Thin },
                    RightBorder = new RightBorder() { Style = BorderStyleValues.Thin },
                    TopBorder = new TopBorder() { Style = BorderStyleValues.None },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.None },
                    DiagonalBorder = new DiagonalBorder()
                }, 2);

            // Cell style formats (x:CellStyleXfs)
            stylesheet.InsertAt(new CellStyleFormats(), 4);
            stylesheet.GetFirstChild<CellStyleFormats>().InsertAt(
                new CellFormat()
                {
                    NumberFormatId = 0,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0
                }, 0);   // Default 

            // Cell formats (x:CellXfs)
            stylesheet.InsertAt(new CellFormats(), 5);
            // General text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    NumberFormatId = 0
                }, 0);
            // Date
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    ApplyNumberFormat = true,
                    FormatId = 0,
                    NumberFormatId = 165,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top
                    }
                }, 1);
            // Currency
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyNumberFormat = true,
                    FormatId = 0,
                    NumberFormatId = 164,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0
                }, 2);
            // Percentage
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyNumberFormat = true,
                    FormatId = 0,
                    NumberFormatId = 10,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0
                }, 3);
            // Bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    NumberFormatId = 0,
                    FontId = 1,
                }, 4);
            // 16 size and bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    NumberFormatId = 0,
                    FontId = 2,
                }, 5);
            // 16 size, bold, center align text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    NumberFormatId = 0,
                    FontId = 2,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Center,
                    },
                }, 6);
            // underline and bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    NumberFormatId = 0,
                    FontId = 3,
                }, 7);
            // align top and left, wrap text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                }, 8);
            // align top and left
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                    },
                }, 9);
            // align top and center, wrap text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                }, 10);
            // align top and center, wrap text, bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 1,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                }, 11);
            // align top and left, bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 1,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                    },
                }, 12);
            // 12 size and bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    NumberFormatId = 0,
                    FontId = 4,
                }, 13);
            // align top and center
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                    },
                }, 14);
            // align top and left, grey background
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    FontId = 0,
                    ApplyAlignment = true,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                    },
                    ApplyFill = true,
                    FillId = 2
                }, 15);
            // align top and left, wrap text, border
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 16);
            // align top and left, underline
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 5,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top
                    }
                }, 17);
            // align top and center, bold
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 1,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top
                    }
                }, 18);
            // align top and left, wrap text, underline
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 5,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    }
                }, 19);
            // align top and center, wrap text, border
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 20);
            // align top and center, wrap text, border, number
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyNumberFormat = true,
                    NumberFormatId = 166,
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 21);
            // align top and left, wrap text, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                }, 22);
            // align top and left, wrap text, border, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 23);
            // align top and left, wrap text, 8 size, underline
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 7,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                }, 24);
            // align bottom and left, grey background, wrap text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    FormatId = 0,
                    FontId = 0,
                    ApplyAlignment = true,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Bottom,
                        WrapText = true
                    },
                    ApplyFill = true,
                    FillId = 2
                }, 25);
            // align top and right
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Right,
                        Vertical = VerticalAlignmentValues.Top,
                    },
                }, 26);
            // Currency, align top and right
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyNumberFormat = true,
                    FormatId = 0,
                    NumberFormatId = 164,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    ApplyAlignment = true,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Right,
                        Vertical = VerticalAlignmentValues.Top
                    }
                }, 27);
            // align top and center, wrap text, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                }, 28);
            // align top and center, wrap text, border, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 29);
            // align top and center, wrap text, border, 8 size, Date
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    ApplyNumberFormat = true,
                    FormatId = 0,
                    NumberFormatId = 165,
                    FontId = 6,
                    FillId = 0,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Top,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 30);
            // align center and center, wrap text, border, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Center,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 31);
            // align center and center, wrap text, 8 size, left & right border
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Center,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 2
                }, 32);
            // align center and center, wrap text, 8 size, strike
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 8,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Center,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 2
                }, 33);
            // align center and center, wrap text, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Center,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 0
                }, 34);
            // align center and center, wrap text, border, 8 size, bold text
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 9,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Center,
                        Vertical = VerticalAlignmentValues.Center,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 1
                }, 35);
            // align center and left, wrap text, 8 size
            stylesheet.GetFirstChild<CellFormats>().InsertAt(
                new CellFormat()
                {
                    ApplyAlignment = true,
                    FormatId = 0,
                    FontId = 6,
                    Alignment = new Alignment()
                    {
                        Horizontal = HorizontalAlignmentValues.Left,
                        Vertical = VerticalAlignmentValues.Center,
                        WrapText = true,
                    },
                    ApplyBorder = true,
                    BorderId = 0
                }, 36);

            stylesheet.Save();

            return true;
        }

        /// <summary>
        /// Adds a list of strings to the shared strings table.
        /// </summary>
        /// <param name="spreadsheet">The spreadsheet</param>
        /// <param name="stringList">Strings to add</param>
        /// <returns></returns>
        public static bool AddSharedStrings(SpreadsheetDocument spreadsheet, List<string> stringList)
        {
            foreach (string item in stringList)
            {
                AddSharedString(spreadsheet, item, false);
            }
            spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable.Save();

            return true;
        }

        /// <summary>
        /// Add a single string to shared strings table.
        /// Shared string table is created if it doesn't exist.
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="stringItem">string to add</param>
        /// <param name="save">Save the shared string table</param>
        /// <returns></returns>
        public static bool AddSharedString(SpreadsheetDocument spreadsheet, string stringItem, bool save = true)
        {
            SharedStringTable sharedStringTable = spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable;

            if (!sharedStringTable.Where(item => item.InnerText == stringItem).Any())
            {
                sharedStringTable.AppendChild(
                   new SharedStringItem(
                      new Text(stringItem)));

                // Save the changes
                if (save)
                {
                    sharedStringTable.Save();
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the index of a shared string.
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="stringItem">String to search for</param>
        /// <returns>Index of a shared string. -1 if not found</returns>
        public static int IndexOfSharedString(SpreadsheetDocument spreadsheet, string stringItem)
        {
            SharedStringTable sharedStringTable = spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable;
            bool found = false;
            int index = 0;

            foreach (SharedStringItem sharedString in sharedStringTable.Elements<SharedStringItem>())
            {
                if (sharedString.InnerText == stringItem)
                {
                    found = true;
                    break; ;
                }
                index++;
            }

            return found ? index : -1;
        }

        /// <summary>
        /// Converts a column number to column name (i.e. A, B, C..., AA, AB...)
        /// </summary>
        /// <param name="columnIndex">Index of the column</param>
        /// <returns>Column name</returns>
        public static string ColumnNameFromIndex(uint columnIndex)
        {
            uint remainder;
            string columnName = "";

            while (columnIndex > 0)
            {
                remainder = (columnIndex - 1) % 26;
                columnName = Convert.ToChar(65 + remainder).ToString() + columnName;
                columnIndex = (columnIndex - remainder) / 26;
            }

            return columnName;
        }

        /// <summary>
        /// Sets a string value to a cell
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="stringValue">String value to set</param>
        /// <param name="useSharedString">Use shared strings? If true and the string isn't found in shared strings, it will be added</param>
        /// <param name="save">Save the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, string stringValue, bool useSharedString, uint? styleIndex, bool save = true)
        {
            string columnValue = stringValue;
            CellValues cellValueType;

            // Add the shared string if necessary
            if (useSharedString)
            {
                if (IndexOfSharedString(spreadsheet, stringValue) == -1)
                {
                    AddSharedString(spreadsheet, stringValue, true);
                }
                columnValue = IndexOfSharedString(spreadsheet, stringValue).ToString();
                cellValueType = CellValues.InlineString;
            }
            else
            {
                cellValueType = CellValues.String;
            }

            return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, cellValueType, columnValue, styleIndex, save);
        }

        /// <summary>
        /// Sets a cell value with a date
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="datetimeValue">DateTime value</param>
        /// <param name="styleIndex">Style to use</param>
        /// <param name="save">Save the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, DateTime datetimeValue, uint? styleIndex, bool save = true)
        {
            string columnValue = datetimeValue.ToOADate().ToString();
            return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, CellValues.Date, columnValue, styleIndex, save);
        }

        /// <summary>
        /// Sets a cell value with double number
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="doubleValue">Double value</param>
        /// <param name="styleIndex">Style to use</param>
        /// <param name="save">Save the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, double doubleValue, uint? styleIndex, bool save = true)
        {
            string columnValue = doubleValue.ToString();
            return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, CellValues.Number, columnValue, styleIndex, save);
        }

        /// <summary>
        /// Sets a cell value with decimal number
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="decimalValue">Decimal value</param>
        /// <param name="styleIndex">Style to use</param>
        /// <param name="save">Save the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, decimal decimalValue, uint? styleIndex, bool save = true)
        {
            string columnValue = decimalValue.ToString();
            return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, CellValues.Number, columnValue, styleIndex, save);
        }

        /// <summary>
        /// Sets a cell value with integer number
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="integerValue">Integer value</param>
        /// <param name="styleIndex">Style to use</param>
        /// <param name="save">Save the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, int integerValue, uint? styleIndex, bool save = true)
        {
            string columnValue = integerValue.ToString();
            return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, CellValues.Number, columnValue, styleIndex, save);
        }

        /// <summary>
        /// Sets a cell value with boolean value
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="boolValue">Boolean value</param>
        /// <param name="styleIndex">Style to use</param>
        /// <param name="save">Save the worksheet</param>
        /// <returns>True if succesful</returns>
        public static bool SetCellValue(SpreadsheetDocument spreadsheet, Worksheet worksheet, uint columnIndex, uint rowIndex, bool boolValue, uint? styleIndex, bool save = true)
        {
            string columnValue = boolValue ? "1" : "0";

            return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, CellValues.Boolean, columnValue, styleIndex, save);
        }

        /// <summary>
        /// Sets the column width
        /// </summary>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Array index of the column</param>
        /// <param name="width">Width to set</param>
        /// <returns>True if succesful</returns>
        public static bool SetColumnWidth(Worksheet worksheet, int[] columnIndexs, int width)
        {
            if (columnIndexs == null)
                return false;

            foreach (var columnIndex in columnIndexs)
            {
                SetColumnWidth(worksheet, columnIndex, width);
            }

            return true;
        }

        /// <summary>
        /// Sets the column width
        /// </summary>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="columnIndex">Index of the column</param>
        /// <param name="width">Width to set</param>
        /// <returns>True if succesful</returns>
        public static bool SetColumnWidth(Worksheet worksheet, int columnIndex, int width)
        {
            Columns columns;
            Column column;

            // Get the column collection exists
            columns = worksheet.Elements<Columns>().FirstOrDefault();
            if (columns == null)
            {
                return false;
            }

            // Get the column
            column = columns.Elements<Column>().Where(item => item.Min == columnIndex).FirstOrDefault();
            if (column == null)
            {
                return false;
            }

            column.Width = width;
            column.CustomWidth = true;

            worksheet.Save();

            return true;
        }

        /// <summary>
        /// Sets the row height
        /// </summary>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="rowIndex">Array index of the row</param>
        /// <param name="height">height to set</param>
        /// <returns>True if succesful</returns>
        public static bool SetRowHeight(Worksheet worksheet, int[] rowIndexs, int height)
        {
            if (rowIndexs == null)
                return false;

            foreach (var rowIndex in rowIndexs)
            {
                SetRowHeight(worksheet, rowIndex, height);
            }

            return true;
        }

        /// <summary>
        /// Sets the row height
        /// </summary>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="rowIndex">Index of the row</param>
        /// <param name="height">height to set</param>
        /// <returns>True if succesful</returns>
        public static bool SetRowHeight(Worksheet worksheet, int rowIndex, int height)
        {
            Row row;

            // Get the sheet data
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            if (sheetData == null)
            {
                return false;
            }

            // Get the row
            row = sheetData.Elements<Row>().Where(item => item.RowIndex == rowIndex).FirstOrDefault();
            if (row == null)
            {
                return false;
            }

            row.Height = height;
            row.CustomHeight = true;

            worksheet.Save();

            return true;
        }

        /// <summary>
        /// Sets the page setup
        /// </summary>
        /// <param name="worksheet">Worksheet to use</param>
        /// <param name="orientation">Page Orientation</param>
        /// <param name="pageWide">Fit to page(s) wide</param>
        /// <param name="pageTall">Fit to page(s) tall</param>
        /// <returns>True if succesful</returns>
        public static bool SetPageSetup(Worksheet worksheet, OrientationValues orientation, uint pageWide, uint pageTall,
            double marginTop = 0.76, double marginRight = 0.72, double marginBottom = 0.76, double marginLeft = 0.72, double marginHeader = 0.32, double marginFooter = 0.32,
            bool showFooter = true, string footerText = null, bool showHeader = false, string headerText = null, bool insertPageBreak = false, uint pageBreakRowIndex = 1)
{
            worksheet.SheetProperties = new SheetProperties(new PageSetupProperties());

            // Set the FitToPage property to true
            worksheet.SheetProperties.PageSetupProperties.FitToPage = true;

            PageMargins pageMargins = new()
            {
                Top = marginTop,
                Right = marginRight,
                Bottom = marginBottom,
                Left = marginLeft,
                Header = marginHeader,
                Footer = marginFooter
            };
            worksheet.Append(pageMargins);

            PageSetup pageSetup = new()
            {
                PaperSize = 9,
                Orientation = orientation,
                FitToHeight = pageTall,
                FitToWidth = pageWide
            };
            worksheet.Append(pageSetup);

            if (showFooter || showHeader)
            {
                HeaderFooter headerFooter = new();

                if (showFooter)
                {
                    OddFooter oddFooter = new();
                    if (string.IsNullOrEmpty(footerText))
                    {
                        //oddFooter.Text = "&L&\"Garamond,Regular\"Page &P of &N&R&\"Garamond,Regular\"Print On: " + DateTime.Now.ToString(AppsConstants.DatetimeFormat);
                        oddFooter.Text = "&L&\"Garamond,Regular\"Page &P of &N&R&\"Garamond,Regular\"Print On: " + DateTime.Now.ToString(AppConfigs.Format.DatetimeFormat);
                    }
                    else
                    {
                        oddFooter.Text = footerText;
                    }
                    headerFooter.Append(oddFooter);
                }

                if (showHeader)
                {
                    OddHeader oddHeader = new()
                    {
                        Text = headerText
                    };
                    headerFooter.Append(oddHeader);
                }

                worksheet.Append(headerFooter);
            }

            if (insertPageBreak)
            {
                RowBreaks rb = worksheet.GetFirstChild<RowBreaks>();
                if (rb == null)
                {
                    rb = new RowBreaks() { Count = 0, ManualBreakCount = 0 };
                }

                Break rowBreak = new() { Id = pageBreakRowIndex, Max = 16383U, ManualPageBreak = true };

                rb.Append(rowBreak);
                rb.ManualBreakCount++;
                rb.Count++;

                worksheet.Append(rb);
            }

            worksheet.Save();

            return true;
        }

        /// <summary>
        /// Adds a predefined style from the given xml
        /// </summary>
        /// <param name="spreadsheet">Spreadsheet to use</param>
        /// <param name="xml">Style definition as xml</param>
        /// <returns>True if succesful</returns>
        public static bool AddPredefinedStyles(SpreadsheetDocument spreadsheet, string xml)
        {
            spreadsheet.WorkbookPart.WorkbookStylesPart.Stylesheet.InnerXml = xml;
            spreadsheet.WorkbookPart.WorkbookStylesPart.Stylesheet.Save();

            return true;
        }

        // Given a Worksheet and a cell name, verifies that the specified cell exists.
        // If it does not exist, creates a new cell. 
        private static void CreateSpreadsheetCellIfNotExist(Worksheet worksheet, string cellName)
        {
            string columnName = GetColumnName(cellName);
            uint rowIndex = GetRowIndex(cellName);

            IEnumerable<Row> rows = worksheet.Descendants<Row>().Where(r => r.RowIndex.Value == rowIndex);

// If the Worksheet does not contain the specified row, create the specified row.
// Create the specified cell in that row, and insert the row into the Worksheet.
if (!rows.Any())
{
                Row row = new() { RowIndex = new UInt32Value(rowIndex) };
                Cell cell = new() { CellReference = new StringValue(cellName) };
                row.Append(cell);
                worksheet.Descendants<SheetData>().First().Append(row);
                worksheet.Save();
            }
            else
            {
                Row row = rows.First();

                IEnumerable<Cell> cells = row.Elements<Cell>().Where(c => c.CellReference.Value == cellName);

// If the row does not contain the specified cell, create the specified cell.
if (!cells.Any())
                {
                    Cell cell = new() { CellReference = new StringValue(cellName) };
                    row.Append(cell);
                    worksheet.Save();
                }
            }
        }

        public static void MergeTwoCells(Worksheet worksheet, string cell1Name, string cell2Name)
        {
            if (worksheet == null || string.IsNullOrEmpty(cell1Name) || string.IsNullOrEmpty(cell2Name))
            {
                return;
            }

            // Verify if the specified cells exist, and if they do not exist, create them.
            CreateSpreadsheetCellIfNotExist(worksheet, cell1Name);
            CreateSpreadsheetCellIfNotExist(worksheet, cell2Name);

            MergeCells mergeCells;
            if (worksheet.Elements<MergeCells>().Any())
            {
                mergeCells = worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();

                // Insert a MergeCells object into the specified position.
                if (worksheet.Elements<CustomSheetView>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                }
                else if (worksheet.Elements<DataConsolidate>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                }
                else if (worksheet.Elements<SortState>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                }
                else if (worksheet.Elements<AutoFilter>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                }
                else if (worksheet.Elements<Scenarios>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                }
                else if (worksheet.Elements<ProtectedRanges>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                }
                else if (worksheet.Elements<SheetProtection>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                }
                else if (worksheet.Elements<SheetCalculationProperties>().Any())
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                }
                else
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                }
            }

// Create the merged cell and append it to the MergeCells collection.
            MergeCell mergeCell = new() { Reference = new StringValue(cell1Name + ":" + cell2Name) };
            mergeCells.Append(mergeCell);

            worksheet.Save();
        }

        public static void SetAutoFilters(Worksheet worksheet, string cell1Name, string cell2Name)
        {
            if (worksheet == null || string.IsNullOrEmpty(cell1Name) || string.IsNullOrEmpty(cell2Name))
            {
                return;
            }

            // Verify if the specified cells exist, and if they do not exist, create them.
            CreateSpreadsheetCellIfNotExist(worksheet, cell1Name);
            CreateSpreadsheetCellIfNotExist(worksheet, cell2Name);

// Set the AutoFilter property to a range that is the size of the data
// within the worksheet
            AutoFilter autoFilter = new() { Reference = new StringValue(cell1Name + ":" + cell2Name) };

            // Insert a MergeCells object into the specified position.
            if (worksheet.Elements<CustomSheetView>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<CustomSheetView>().First());
            }
            else if (worksheet.Elements<DataConsolidate>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<DataConsolidate>().First());
            }
            else if (worksheet.Elements<SortState>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<SortState>().First());
            }
            else if (worksheet.Elements<Scenarios>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<Scenarios>().First());
            }
            else if (worksheet.Elements<ProtectedRanges>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<ProtectedRanges>().First());
            }
            else if (worksheet.Elements<SheetProtection>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<SheetProtection>().First());
            }
            else if (worksheet.Elements<SheetCalculationProperties>().Any())
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<SheetCalculationProperties>().First());
            }
            else
            {
                worksheet.InsertAfter(autoFilter, worksheet.Elements<SheetData>().First());
            }

            worksheet.Save();
        }
    }
}
