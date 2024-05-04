using HtmlPdfConverter;
using Microsoft.AspNetCore.Mvc;
using PDFGenerator.Models;
using System.Threading.Tasks;

namespace PDFGenerator.Controllers
{
    /// <summary>
    /// Controller for handling PDF downloads.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DownloadController"/> class.
    /// </remarks>
    /// <param name="pdfConverter">The PDF converter service to be injected.</param>
    public class DownloadController(IPdfConverter pdfConverter) : Controller
    {
        private readonly IPdfConverter _pdfConverter = pdfConverter;

        /// <summary>
        /// Renders the index view.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Converts an HTML partial view to a PDF document and returns it as a file download.
        /// </summary>
        /// <returns>A file download containing the generated PDF.</returns>
        [HttpGet]
        public async Task<IActionResult> DownloadPdf()
        {
            var model = new Employee
            {
                Name = "Supraja Konchada",
                EmployeeID = 242,
                Company = "Cognine Technologies"
            };

            // Specify the partial view path
            string partialViewPath = "Views/Download/ExamplePartialView.cshtml";

            // Convert the partial view to PDF using the PDF converter service
            byte[] pdfBytes = await _pdfConverter.ConvertHtmlToPdfAsync(partialViewPath, model);

            // Return the generated PDF as a file download
            return File(pdfBytes, "application/pdf", "download.pdf");
        }
    }
}
