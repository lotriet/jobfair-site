#!/usr/bin/env python3
"""
QR Code Generator for Portfolio Site
Generates a printable QR code for the Azure-hosted .NET Micro API
"""

import qrcode
from PIL import Image, ImageDraw, ImageFont
import os

def create_portfolio_qr():
    # Your live Azure URL
    url = "https://lotriet-jobfair-site-d4gvegbgaybne9cq.canadacentral-01.azurewebsites.net"
    
    # Create QR code instance
    qr = qrcode.QRCode(
        version=1,  # Controls the size of the QR Code
        error_correction=qrcode.constants.ERROR_CORRECT_M,  # About 15% error correction
        box_size=10,  # Size of each box in pixels
        border=4,    # Size of the border (minimum is 4)
    )
    
    # Add data to QR code
    qr.add_data(url)
    qr.make(fit=True)
    
    # Create QR code image
    qr_img = qr.make_image(fill_color="black", back_color="white")
    
    # Create a larger image for the complete design
    width, height = 800, 1000
    img = Image.new('RGB', (width, height), 'white')
    
    # Paste QR code in the center
    qr_width, qr_height = qr_img.size
    qr_x = (width - qr_width) // 2
    qr_y = 200
    img.paste(qr_img, (qr_x, qr_y))
    
    # Add text (simplified without custom fonts)
    draw = ImageDraw.Draw(img)
    
    # Title
    title = "🎯 Portfolio Demo"
    title_bbox = draw.textbbox((0, 0), title)
    title_width = title_bbox[2] - title_bbox[0]
    draw.text(((width - title_width) // 2, 50), title, fill="black")
    
    # Subtitle
    subtitle = "Scan to view .NET Micro API"
    subtitle_bbox = draw.textbbox((0, 0), subtitle)
    subtitle_width = subtitle_bbox[2] - subtitle_bbox[0]
    draw.text(((width - subtitle_width) // 2, 100), subtitle, fill="gray")
    
    # URL
    url_text = "lotriet-jobfair-site.azurewebsites.net"
    url_bbox = draw.textbbox((0, 0), url_text)
    url_width = url_bbox[2] - url_bbox[0]
    draw.text(((width - url_width) // 2, qr_y + qr_height + 30), url_text, fill="black")
    
    # Features
    features = [
        "✅ .NET 8 Web API",
        "✅ Async/Await Patterns", 
        "✅ SQLite + Entity Framework",
        "✅ Polly Retry Policy",
        "✅ Azure Cloud Hosting",
        "✅ Professional Portfolio"
    ]
    
    y_start = qr_y + qr_height + 80
    for i, feature in enumerate(features):
        feature_bbox = draw.textbbox((0, 0), feature)
        feature_width = feature_bbox[2] - feature_bbox[0]
        draw.text(((width - feature_width) // 2, y_start + i * 30), feature, fill="darkblue")
    
    # Save the image
    output_path = "portfolio_qr_code.png"
    img.save(output_path, "PNG", quality=100)
    
    print(f"✅ QR Code generated successfully!")
    print(f"📁 Saved as: {output_path}")
    print(f"🌐 URL: {url}")
    print(f"📏 Image size: {width}x{height} pixels")
    print(f"🖨️ Ready for printing!")
    
    return output_path

if __name__ == "__main__":
    create_portfolio_qr()