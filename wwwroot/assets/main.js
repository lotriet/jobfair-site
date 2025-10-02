// Config: where the QR should point
const LANDING_URL = "https://lotriet.dev/index.html";

window.addEventListener('DOMContentLoaded', () => {
  const el = document.getElementById('shortUrl');
  if (el) el.textContent = LANDING_URL.replace(/^https?:\/\//,'');
  const qrEl = document.getElementById('qrcode');
  if (qrEl && window.QRCode) new QRCode(qrEl, { text: LANDING_URL, width: 140, height: 140 });
});
