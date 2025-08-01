using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QR_Track.Models;
using QRCoder;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace QR_Track.Controllers
{
         /* App web que lea un QR por medio de la cámara es decir
         que permita utilizar la cámara del dispositivo, (pc, celular) 
         y registre la fecha y la hora del código recuperado, contando catalogo de códigos,
         con una descripción o nombre de persona.
    
        private readonly QR_TrackDbContext _context;

        public QRController(QR_TrackDbContext context)
        {
            _context = context;
        }
  */ 


    [Authorize(Roles = "Admin")]
    public class QRController : Controller
    {  
        private readonly QrTrackDbContext context;

        public QRController(QrTrackDbContext dBContext)
        {
            context = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcesarQR([FromBody] QRRequest request)
        {
            // Procesa el código QR recibido
            var qrLeido = request.Qr;
            var arr = qrLeido.Split('|');


            var idQr = int.Parse(arr[0]);

            var item = new TblLeido();
            item.IdQr = idQr;
            item.DtLeido = DateTime.Now;
            
            context.TblLeidos.Add(item);
            context.SaveChanges();

            return Json(new { success = true, mensaje = "QR recibido correctamente" });
        }
        
        public IActionResult GenerarQR(string texto, int id, int toDb)
        {
            try
            {
                var newId = 0;
                var idPersona = id;

                if (toDb == 1)
                {
                    var newItem = new TblQr();
                    newItem.Descripcion = idPersona + "|" + texto; ;
                    newItem.IdPersona = idPersona;
                    context.TblQrs.Add(newItem);
                    context.SaveChanges();

                    newId = newItem.Id;
                }

                string fullText = newId + "|" + idPersona + "|" + texto;

                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode(fullText ?? "Texto por defecto", QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new QRCode(qrCodeData))
                using (var bitmap = qrCode.GetGraphic(20))
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream.ToArray(), "image/png");
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    mensaje = "Error al generar el QR",
                    error = ex.Message
                });
            }
        }

        public JsonResult PersonasGetN(string texto)
        {
            var lst = context.TblPersonas.ToList()
                //.Where(p => string.IsNullOrEmpty(texto) || p.Nombre.Contains(texto))
                .Where(p => p.Nombre.ToLower().Contains(texto.ToLower() ) )
                .ToList();
            return Json(lst);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostProcesarQRAsync([FromBody] QRRequest request)
        {
            var qrLeido = request.Qr;
            // Lógica adicional aquí

            return new JsonResult(new { success = true, mensaje = "QR recibido correctamente" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
