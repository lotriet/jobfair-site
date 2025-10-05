#!/usr/bin/env python3
"""
QR Code Generator for Portfolio Site
Generates a printable QR code for the Azure-hosted .NET Micro API
Enhanced with logo overlay support
"""

import qrcode
from PIL import Image, ImageDraw, ImageFont
import os

def create_logo(size=80, logo_path=None):
    """Create a logo for the QR code center"""
    if logo_path and os.path.exists(logo_path):
        # Use custom logo file
        try:
            logo = Image.open(logo_path)
            logo = logo.convert("RGBA")
            
            # Resize logo to fit
            logo = logo.resize((size, size), Image.Resampling.LANCZOS)
            
            # Create circular mask
            mask = Image.new('L', (size, size), 0)
            mask_draw = ImageDraw.Draw(mask)
            mask_draw.ellipse([0, 0, size, size], fill=255)
            
            # Apply circular mask to logo
            circular_logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
            circular_logo.paste(logo, (0, 0))
            circular_logo.putalpha(mask)
            
            return circular_logo
        except Exception as e:
            print(f"Warning: Could not load logo from {logo_path}: {e}")
            print("Falling back to default logo...")
    
    # Create default logo with initials
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    # Draw a circular background
    margin = 5
    circle_size = size - (margin * 2)
    draw.ellipse([margin, margin, size - margin, size - margin], 
                fill=(255, 215, 0, 255), outline=(0, 0, 0, 255), width=2)
    
    # Draw initials or icon in the center
    text = "LT"  # Your initials
    try:
        # Try to use a larger font
        font_size = size // 3
        font = ImageFont.truetype("arial.ttf", font_size)
    except:
        # Fallback to default font
        font = ImageFont.load_default()
    
    # Get text bounding box
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    
    # Center the text
    text_x = (size - text_width) // 2
    text_y = (size - text_height) // 2
    
    draw.text((text_x, text_y), text, fill=(0, 0, 0, 255), font=font)
    
    return logo

def create_portfolio_qr():
    # Your live Azure URL
    url = "https://lotriet-jobfair-site-d4gvegbgaybne9cq.canadacentral-01.azurewebsites.net"
    
    # Create QR code instance
    qr = qrcode.QRCode(
        version=1,  # Controls the size of the QR Code
        error_correction=qrcode.constants.ERROR_CORRECT_H,  # High error correction for logo overlay
        box_size=10,  # Size of each box in pixels
        border=4,    # Size of the border (minimum is 4)
    )
    
    # Add data to QR code
    qr.add_data(url)
    qr.make(fit=True)
    
    # Create QR code image
    qr_img = qr.make_image(fill_color="black", back_color="white")
    
    # Add logo to the center of QR code
    # You can specify a custom logo file here, e.g., "logo.png"
    logo = create_logo(size=80, logo_path="logo.png")
    
    # Calculate position for logo (center of QR code)
    qr_width, qr_height = qr_img.size
    logo_size = logo.size[0]
    logo_pos = ((qr_width - logo_size) // 2, (qr_height - logo_size) // 2)
    
    # Convert QR code to RGBA for transparency support
    qr_img = qr_img.convert("RGBA")
    
    # Paste logo onto QR code
    qr_img.paste(logo, logo_pos, logo)
    
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
    output_path = "portfolio_qr_code_with_logo.png"
    img.save(output_path, "PNG", quality=100)
    
    print(f"✅ QR Code with logo generated successfully!")
    print(f"📁 Saved as: {output_path}")
    print(f"🌐 URL: {url}")
    print(f"📏 Image size: {width}x{height} pixels")
    print(f"🎨 Logo: Embedded in center")
    print(f"🖨️ Ready for printing!")
    
    return output_path

if __name__ == "__main__":
    create_portfolio_qr()