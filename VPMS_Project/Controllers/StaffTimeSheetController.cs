using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Models;
using VPMS_Project.Repository;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace VPMS_Project.Controllers
{
    public class StaffTimeSheetController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TaskRepo _taskRepository = null;

        public StaffTimeSheetController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, TaskRepo taskRepository)
        {
            _taskRepository = taskRepository;
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<IActionResult> TimeSheet(int id, DateTime Start, DateTime End, String Complete, int ID = 0)
        {
            ViewBag.EmpId = 2;
            ViewBag.TaskId = ID;

            if (ID != 0)
            {
                await _taskRepository.AddTaskFromList(ID);
            }
            if (Start != DateTime.MinValue && End != DateTime.MinValue)
            {
               
                await _taskRepository.TImeSheetTaskInsert(id, Start, End);

                if (Complete == "Complete")
                {
                    await _taskRepository.CompleteTask(id);
                }
                if (Complete == "NotComplete")
                {
                    await _taskRepository.NotCompleteTask(id);
                }
                return RedirectToAction(nameof(TimeSheet), new {ID = 0 });

            }
            var data = await _taskRepository.AddTaskList(ViewBag.EmpId);
            return View(data);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            bool success = await _taskRepository.CancelAddingTask(id);

            if (success)
            {
                return RedirectToAction(nameof(TimeSheet));
            }
            return RedirectToAction(nameof(TimeSheet));
        }

        [HttpGet]
        public async Task<IActionResult> ViewTimeS(DateTime Date)
        {
            int id = 2;
            if (Date == DateTime.MinValue)
            {
                ViewBag.Empty = true;
                return View();
            }
            else
            {
                ViewBag.Date = Date;

                ViewBag.TotalHours = _taskRepository.GetTotalHours(id, Date);
                var data = await _taskRepository.GetTimeSheet(id, Date);
                if (data != null)
                {
                    ViewBag.Empty = false;
                    return View(data);
                }
                else
                {
                    ViewBag.Empty = true;
                    return View();
                }
            }


        }

        public IActionResult CreateDocument()
        {
            int id = 2;
            //Creates a new PDF document
            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            //Loads the image as stream
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 24);
            graphics.DrawString("Daily Time Sheet  -  ", font, PdfBrushes.Blue, new PointF(170, 30));
            string Date = DateTime.Now.ToString("MM/dd/yyyy");
            PdfFont font4 = new PdfStandardFont(PdfFontFamily.Helvetica, 18);
            string currentDate = "DATE " + DateTime.Now.ToString("MM/dd/yyyy");
            graphics.DrawString(Date, font4, PdfBrushes.Blue, new PointF(370, 33));

            PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            graphics.DrawString("Ishara Maduhansi", font2, PdfBrushes.Black, new PointF(190, 70));

            PdfFont font3 = new PdfStandardFont(PdfFontFamily.Helvetica, 13);
            graphics.DrawString("Software Engineer", font3, PdfBrushes.Black, new PointF(190, 95));

            FileStream imageStream = new FileStream("wwwroot/images/bell.jpg", FileMode.Open, FileAccess.Read);
            RectangleF bounds = new RectangleF(10, 0, 150, 150);
            PdfImage image = PdfImage.FromStream(imageStream);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, 160, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("Time Sheet ", subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, 165));
            //Measures the width of the text to place it in the correct location
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, 165);
            //Draws the date by using DrawString method
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);

            //Creates text elements to add the address and draw it to the page.
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            ////Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);
            //Creates the datasource for the table
            // Creates a PDF grid
            // Creates the grid cell styles
            //Create a PdfGrid.
            //Add values to list
            //var tasks = _taskRepository.GetTimeSheet(id, DateTime.Now);
            //foreach (var task in tasks) 
            //{ 
            
            
            //}
            List<object> data = new List<object>();
            Object row1 = new { Task = "Login", Start_Date = Date , End_Date = Date , Effort="2 h 40 Min"};
            Object row2 = new { Task = "Time Sheet", Start_Date = Date, End_Date = Date, Effort = "3 h " };
            Object row3 = new { Task = "Employee", Start_Date = Date, End_Date = Date, Effort = "2 h 40 Min" };
            Object row4 = new { Task = "Leave Part", Start_Date = Date, End_Date = Date, Effort = "4 h 20 Min" };
            Object row5 = new { Task = "Attendence Part", Start_Date = Date, End_Date = Date, Effort = "1 h 40 Min" };
            data.Add(row1);
            data.Add(row2);
            data.Add(row3);
            data.Add(row4);
            data.Add(row5);
            //Add list to IEnumerable
            IEnumerable<object> dataTable = data;
            //Assign data source.
            //Draw grid to the page of PDF document.
            //Creates the datasource for the table 
            //Creates a PDF grid
            PdfGrid grid = new PdfGrid();
            //Adds the data source
            grid.DataSource = dataTable;
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];
            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 13f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;
            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return fileStreamResult;

        }

      
    }
}
