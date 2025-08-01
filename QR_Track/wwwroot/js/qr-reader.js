

function iniciarLectorQR(elementId, onScan) {
    const html5QrCode = new Html5Qrcode(elementId);
    html5QrCode.start(
        { facingMode: "environment" },
        {
            fps: 10,
            qrbox: 250
        },
        
        qrCodeMessage => {

            onScan(qrCodeMessage);
            html5QrCode.stop();


            // Enviar el resultado al servidor
            fetch('/QR/ProcesarQR', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify({ qr: qrCodeMessage })
            })
                .then(response => response.json())
                .then(data => {
                    // Puedes mostrar una respuesta del servidor si lo deseas
                    console.log(data);
                });
        },
            errorMessage => {
                //console.log(errorMessage);
            }
        
    );
}
