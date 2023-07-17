using ClosedXML.Excel;
using FileExceptions;
using System;
using System.Data;
using System.Text.Json;

namespace FileException
{
    class HandleExceptions
    {
        static void Main(string[] args)
        {
            try
            {
                // string filePath = @"E:\Excels\\TimeTesting 1.xlsx"; --> IOException
                //string filePath = @"E:\Excels\Testing.xlsx"; --->  FileNotFoundException                
                string filePath = @"E:\Excels\TimeTesting 1.xlsx";
                List<Schedulers> schedulers = new List<Schedulers>();
                using (XLWorkbook workBook = new XLWorkbook(filePath))
                {
                    DataSet dataSet = new DataSet();
                    IXLWorksheet xLWorksheet = workBook.Worksheet(1);
                    DataTable dt = new DataTable(xLWorksheet.Name);

                    // Read First Row of Excel Sheet to add Columns to DataTable
                    xLWorksheet.FirstRowUsed().CellsUsed().ToList()
                        .ForEach(x => { dt.Columns.Add(x.Value.ToString()); });
                    foreach (IXLRow row in xLWorksheet.RowsUsed().Skip(1))
                    {
                        var scheduler = new Schedulers();
                        bool isfilled = true;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            string cellContent = row.Cell(i + 1).Value.ToString();
                            switch (i)
                            {
                                case 0:
                                    scheduler.Week = cellContent;
                                    break;
                                case 1:
                                    if (string.IsNullOrEmpty(cellContent)) isfilled = false;
                                    else
                                        scheduler.Schedules = JsonSerializer.Deserialize<ResponseModel>(cellContent).Schedules ?? new List<ScheduleModal>();
                                    break;
                            }


                        }
                        if (isfilled) schedulers.Add(scheduler);
                    }

                }
                DateTime now = DateTime.Now;
                DayOfWeek dayOfWeek = now.DayOfWeek;
                var response = schedulers.FirstOrDefault(s => string.Equals(s.Week, dayOfWeek.ToString(), StringComparison.OrdinalIgnoreCase))
                    .Schedules.Select(s => new ScheduleModal()
                    {
                        ScheduleTimes = s.ScheduleTimes,
                        AppPath = s.AppPath,
                        Arguments = s.Arguments
                    }).ToList();


                foreach (var item in response)
                {
                    foreach (var item2 in item.ScheduleTimes)
                    {
                        DateTime beforeTime = now.AddMinutes(-10);
                        DateTime afterTime = now.AddMinutes(10);
                        TimeSpan t = TimeSpan.Parse(item2);
                        if (beforeTime.TimeOfDay <= t && afterTime.TimeOfDay >= t)
                        {
                            Console.WriteLine(item.AppPath);
                            Console.WriteLine(item2);
                        }
                        else
                        {
                            Console.WriteLine("Schedule Time are not matched");
                        }
                    }
                }

            }
            catch (FileNotFoundException)
            {

                Console.WriteLine("File not found. Please provide a valid file path.");
            }
            catch (IOException)
            {
                Console.WriteLine("Error reading the file. Please check file permissions or file format.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
