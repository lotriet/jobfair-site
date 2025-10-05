// Config: where the QR should point
const LANDING_URL = "https://lotriet.dev";

window.addEventListener("DOMContentLoaded", () => {
  const el = document.getElementById("shortUrl");
  if (el) el.textContent = LANDING_URL.replace(/^https?:\/\//, "");

  const qrEl = document.getElementById("qrcode");
  if (qrEl && window.QRCode) {
    // Generate QR code with enhanced styling
    const qr = new QRCode(qrEl, {
      text: LANDING_URL,
      width: 140,
      height: 140,
      correctLevel: QRCode.CorrectLevel.H, // High error correction for logo overlay
      colorDark: "#1a365d", // Dark blue instead of black
      colorLight: "#ffffff",
    });

    // Add enhanced logo overlay after QR code is generated
    setTimeout(() => {
      addEnhancedLogoOverlay(qrEl);
      addQRStyling(qrEl);
    }, 100);

    // Make QR code clickable
    qrEl.addEventListener("click", () => {
      window.open(LANDING_URL, "_blank");
    });

    // Add cursor pointer and title
    qrEl.style.cursor = "pointer";
    qrEl.title = "Click to view CV";
  }
});

function addEnhancedLogoOverlay(qrContainer) {
  // Create logo container
  const logoContainer = document.createElement("div");
  logoContainer.className = "qr-logo-container";

  // Create the main logo circle
  const logoCircle = document.createElement("div");
  logoCircle.className = "qr-logo-circle";

  // Create initials text
  const logoText = document.createElement("span");
  logoText.className = "qr-logo-text";
  logoText.textContent = "CL";

  // Assemble logo
  logoCircle.appendChild(logoText);
  logoContainer.appendChild(logoCircle);

  // Style the logo container
  logoContainer.style.cssText = `
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 40px;
    height: 40px;
    pointer-events: none;
    z-index: 10;
  `;

  // Style the logo circle with advanced design
  logoCircle.style.cssText = `
    width: 100%;
    height: 100%;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 50%, #f093fb 100%);
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    border: 3px solid white;
    box-shadow: 
      0 4px 12px rgba(0,0,0,0.15),
      0 2px 4px rgba(0,0,0,0.1),
      inset 0 1px 0 rgba(255,255,255,0.2);
    position: relative;
    overflow: hidden;
  `;

  // Add a subtle inner glow effect
  const innerGlow = document.createElement("div");
  innerGlow.style.cssText = `
    position: absolute;
    top: 2px;
    left: 2px;
    right: 2px;
    bottom: 2px;
    border-radius: 50%;
    background: linear-gradient(135deg, rgba(255,255,255,0.3) 0%, transparent 50%);
    pointer-events: none;
  `;
  logoCircle.appendChild(innerGlow);

  // Style the text
  logoText.style.cssText = `
    font-weight: 700;
    font-size: 14px;
    color: white;
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Inter', sans-serif;
    text-shadow: 0 1px 2px rgba(0,0,0,0.3);
    z-index: 2;
    position: relative;
  `;

  // Make QR container relative for absolute positioning
  qrContainer.style.position = "relative";
  qrContainer.appendChild(logoContainer);
}

function addQRStyling(qrContainer) {
  // Add container styling for better presentation
  qrContainer.style.cssText += `
    border-radius: 12px;
    padding: 8px;
    background: linear-gradient(145deg, #f8fafc, #e2e8f0);
    box-shadow: 
      0 4px 6px -1px rgba(0, 0, 0, 0.1),
      0 2px 4px -1px rgba(0, 0, 0, 0.06),
      inset 0 1px 0 rgba(255, 255, 255, 0.1);
    border: 1px solid rgba(255, 255, 255, 0.2);
    transition: all 0.3s ease;
  `;

  // Add hover effect
  qrContainer.addEventListener("mouseenter", () => {
    qrContainer.style.transform = "scale(1.05)";
    qrContainer.style.boxShadow = `
      0 10px 25px -3px rgba(0, 0, 0, 0.1),
      0 4px 6px -2px rgba(0, 0, 0, 0.05),
      inset 0 1px 0 rgba(255, 255, 255, 0.1)
    `;
  });

  qrContainer.addEventListener("mouseleave", () => {
    qrContainer.style.transform = "scale(1)";
    qrContainer.style.boxShadow = `
      0 4px 6px -1px rgba(0, 0, 0, 0.1),
      0 2px 4px -1px rgba(0, 0, 0, 0.06),
      inset 0 1px 0 rgba(255, 255, 255, 0.1)
    `;
  });
}
