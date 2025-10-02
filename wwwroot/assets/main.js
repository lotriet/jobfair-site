// Config: where the QR should point
const LANDING_URL = "https://lotriet.dev";

window.addEventListener("DOMContentLoaded", () => {
  const el = document.getElementById("shortUrl");
  if (el) el.textContent = LANDING_URL.replace(/^https?:\/\//, "");

  const qrEl = document.getElementById("qrcode");
  if (qrEl && window.QRCode) {
    new QRCode(qrEl, { text: LANDING_URL, width: 140, height: 140 });

    // Make QR code clickable
    qrEl.addEventListener("click", () => {
      window.open(LANDING_URL, "_blank");
    });

    // Add cursor pointer and title
    qrEl.style.cursor = "pointer";
    qrEl.title = "Click to view CV";
  }
});
