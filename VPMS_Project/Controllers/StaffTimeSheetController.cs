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
                TimeSpan differ = (TimeSpan)(End - Start);
                Double TotalHours = differ.TotalHours;
                await _taskRepository.TImeSheetTaskInsert(id, Start, End, TotalHours);

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

   

        public IActionResult CreateDocument()
        {
            int id = 2;
            //Creates a new PDF document
            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            //Loads the image as stream
            FileStream imageStream = new FileStream("wwwroot/images/Parallax.jpg", FileMode.Open, FileAccess.Read);
            RectangleF bounds = new RectangleF(176, 0, 390, 130);
            PdfImage image = PdfImage.FromStream(imageStream);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("INVOICE " + id.ToString(), subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top + 8));
            string currentDate = "DATE " + DateTime.Now.ToString("MM/dd/yyyy");
            //Measures the width of the text to place it in the correct location
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y);
            //Draws the date by using DrawString method
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("BILL TO ", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);
            //Creates the datasource for the table
            //Creates a PDF grid
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();

            PdfGrid pdfGrid = new PdfGrid();
            //Add values to list
            List<object> data = new List<object>();
            Object row1 = new { ID = "E01", Name = "Clay" };
            Object row2 = new { ID = "E02", Name = "Thomas" };
            Object row3 = new { ID = "E03", Name = "Andrew" };
            Object row4 = new { ID = "E04", Name = "Paul" };
            Object row5 = new { ID = "E05", Name = "Gray" };
            data.Add(row1);
            data.Add(row2);
            data.Add(row3);
            data.Add(row4);
            data.Add(row5);
            //Add list to IEnumerable
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
            //Creates the header style

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
