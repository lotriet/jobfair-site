#!/usr/bin/env python3
"""
Premium QR Code Generator with Advanced Styling
Creates beautiful, professional QR codes with various design options
"""

import qrcode
from PIL import Image, ImageDraw, ImageFont, ImageFilter
import os
import math

def create_premium_logo(size=100, style="gradient"):
    """Create premium logo designs"""
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    if style == "gradient":
        # Create gradient effect manually
        for y in range(size):
            for x in range(size):
                # Calculate distance from center
                center = size // 2
                distance = math.sqrt((x - center) ** 2 + (y - center) ** 2)
                
                if distance <= center - 3:
                    # Create gradient from blue to purple
                    ratio = distance / (center - 3)
                    r = int(102 + (118 - 102) * ratio)  # 102->118
                    g = int(126 + (75 - 126) * ratio)   # 126->75
                    b = int(234 + (162 - 234) * ratio)  # 234->162
                    
                    logo.putpixel((x, y), (r, g, b, 255))
        
        # Add white border
        draw.ellipse([2, 2, size-2, size-2], outline=(255, 255, 255, 255), width=4)
        
    elif style == "modern":
        # Modern flat design with accent
        draw.ellipse([3, 3, size-3, size-3], fill=(45, 55, 72, 255))  # Dark background
        draw.ellipse([8, 8, size-8, size-8], outline=(99, 179, 237, 255), width=3)  # Blue accent ring
        
    elif style == "glass":
        # Glassmorphism effect
        draw.ellipse([2, 2, size-2, size-2], fill=(255, 255, 255, 180))
        draw.ellipse([6, 6, size-6, size-6], outline=(100, 149, 237, 200), width=2)
        
        # Add glass highlights
        highlight = Image.new('RGBA', (size, size), (0, 0, 0, 0))
        highlight_draw = ImageDraw.Draw(highlight)
        highlight_draw.ellipse([size//4, size//8, size//2, size//3], fill=(255, 255, 255, 80))
    
    # Add initials
    text = "CL"
    try:
        font = ImageFont.truetype("arial.ttf", size // 2.2)
    except:
        font = ImageFont.load_default()
    
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    
    text_x = (size - text_width) // 2
    text_y = (size - text_height) // 2 - 2
    
    # Add text shadow
    if style != "glass":
        draw.text((text_x + 1, text_y + 1), text, fill=(0, 0, 0, 100), font=font)
    
    # Add main text
    text_color = (255, 255, 255, 255) if style != "glass" else (45, 55, 72, 255)
    draw.text((text_x, text_y), text, fill=text_color, font=font)
    
    return logo

def create_beautiful_qr(url, style="premium"):
    """Create a beautiful QR code with various styling options"""
    
    # QR code generation with high error correction
    qr = qrcode.QRCode(
        version=1,
        error_correction=qrcode.constants.ERROR_CORRECT_H,  # Highest error correction
        box_size=12,  # Larger boxes for better quality
        border=4,
    )
    
    qr.add_data(url)
    qr.make(fit=True)
    
    if style == "premium":
        # Premium style with rounded corners and gradients
        qr_img = qr.make_image(
            fill_color="#1a365d",  # Dark blue
            back_color="#ffffff"
        ).convert("RGBA")
        
        # Add subtle rounded corners effect
        mask = Image.new('L', qr_img.size, 0)
        mask_draw = ImageDraw.Draw(mask)
        mask_draw.rounded_rectangle([0, 0, qr_img.size[0], qr_img.size[1]], radius=20, fill=255)
        
        # Apply mask for rounded corners
        rounded_qr = Image.new('RGBA', qr_img.size, (0, 0, 0, 0))
        rounded_qr.paste(qr_img, (0, 0))
        rounded_qr.putalpha(mask)
        
        qr_img = rounded_qr
        
    elif style == "modern":
        # Modern flat design
        qr_img = qr.make_image(
            fill_color="#2d3748",  # Modern dark gray
            back_color="#f7fafc"   # Light gray background
        ).convert("RGBA")
        
    elif style == "colorful":
        # Colorful gradient style
        qr_img = qr.make_image(
            fill_color="#6b46c1",  # Purple
            back_color="#fef3c7"   # Light yellow background
        ).convert("RGBA")
    
    else:  # classic
        qr_img = qr.make_image(fill_color="black", back_color="white").convert("RGBA")
    
    # Add premium logo
    logo_style = "gradient" if style == "premium" else "modern" if style == "modern" else "glass"
    logo = create_premium_logo(size=100, style=logo_style)
    
    # Calculate position for logo (center of QR code)
    qr_width, qr_height = qr_img.size
    logo_size = logo.size[0]
    logo_pos = ((qr_width - logo_size) // 2, (qr_height - logo_size) // 2)
    
    # Paste logo onto QR code
    qr_img.paste(logo, logo_pos, logo)
    
    return qr_img

def create_portfolio_qr_premium():
    """Create premium portfolio QR code"""
    url = "https://lotriet.dev"
    
    # Create multiple style variants
    styles = {
        "premium": "Premium gradient style with rounded corners",
        "modern": "Modern flat design with clean aesthetics", 
        "colorful": "Vibrant colors for creative portfolios",
        "classic": "Traditional professional look"
    }
    
    # Create a large composition with multiple styles
    width, height = 1200, 1600
    img = Image.new('RGB', (width, height), '#f8fafc')
    draw = ImageDraw.Draw(img)
    
    # Title
    try:
        title_font = ImageFont.truetype("arial.ttf", 48)
        subtitle_font = ImageFont.truetype("arial.ttf", 24)
        desc_font = ImageFont.truetype("arial.ttf", 18)
    except:
        title_font = ImageFont.load_default()
        subtitle_font = ImageFont.load_default()
        desc_font = ImageFont.load_default()
    
    # Draw title
    title = "Premium QR Code Collection"
    title_bbox = draw.textbbox((0, 0), title, font=title_font)
    title_width = title_bbox[2] - title_bbox[0]
    draw.text(((width - title_width) // 2, 50), title, fill="#1a365d", font=title_font)
    
    # Draw subtitle
    subtitle = "Professional Portfolio Access"
    subtitle_bbox = draw.textbbox((0, 0), subtitle, font=subtitle_font)
    subtitle_width = subtitle_bbox[2] - subtitle_bbox[0]
    draw.text(((width - subtitle_width) // 2, 120), subtitle, fill="#4a5568", font=subtitle_font)
    
    # Generate and place QR codes in a 2x2 grid
    y_start = 200
    for i, (style_name, description) in enumerate(styles.items()):
        row = i // 2
        col = i % 2
        
        qr_img = create_beautiful_qr(url, style_name)
        qr_img = qr_img.resize((300, 300), Image.Resampling.LANCZOS)
        
        x = 150 + col * 450
        y = y_start + row * 400
        
        # Create a card background
        card_margin = 30
        card_x1 = x - card_margin
        card_y1 = y - card_margin
        card_x2 = x + 300 + card_margin
        card_y2 = y + 300 + card_margin + 80
        
        # Draw card shadow
        shadow_offset = 5
        draw.rounded_rectangle(
            [card_x1 + shadow_offset, card_y1 + shadow_offset, card_x2 + shadow_offset, card_y2 + shadow_offset],
            radius=15, fill=(0, 0, 0, 30)
        )
        
        # Draw card background
        draw.rounded_rectangle([card_x1, card_y1, card_x2, card_y2], radius=15, fill="white")
        
        # Paste QR code
        img.paste(qr_img, (x, y), qr_img)
        
        # Add style name
        style_title = style_name.capitalize()
        style_bbox = draw.textbbox((0, 0), style_title, font=subtitle_font)
        style_width = style_bbox[2] - style_bbox[0]
        draw.text((x + (300 - style_width) // 2, y + 320), style_title, fill="#1a365d", font=subtitle_font)
        
        # Add description
        desc_lines = description.split()
        line1 = " ".join(desc_lines[:3])
        line2 = " ".join(desc_lines[3:]) if len(desc_lines) > 3 else ""
        
        line1_bbox = draw.textbbox((0, 0), line1, font=desc_font)
        line1_width = line1_bbox[2] - line1_bbox[0]
        draw.text((x + (300 - line1_width) // 2, y + 350), line1, fill="#718096", font=desc_font)
        
        if line2:
            line2_bbox = draw.textbbox((0, 0), line2, font=desc_font)
            line2_width = line2_bbox[2] - line2_bbox[0]
            draw.text((x + (300 - line2_width) // 2, y + 370), line2, fill="#718096", font=desc_font)
    
    # Add footer
    footer = f"Scan any QR code to visit: {url}"
    footer_bbox = draw.textbbox((0, 0), footer, font=desc_font)
    footer_width = footer_bbox[2] - footer_bbox[0]
    draw.text(((width - footer_width) // 2, height - 100), footer, fill="#4a5568", font=desc_font)
    
    # Save the collection
    output_path = "premium_qr_collection.png"
    img.save(output_path, "PNG", quality=100)
    
    # Also save individual premium version
    premium_qr = create_beautiful_qr(url, "premium")
    premium_single = Image.new('RGB', (600, 700), '#f8fafc')
    
    # Add padding and center the QR code
    qr_size = 400
    premium_qr_resized = premium_qr.resize((qr_size, qr_size), Image.Resampling.LANCZOS)
    premium_single.paste(premium_qr_resized, ((600 - qr_size) // 2, 100), premium_qr_resized)
    
    # Add title
    single_draw = ImageDraw.Draw(premium_single)
    title = "Premium Portfolio QR"
    title_bbox = single_draw.textbbox((0, 0), title, font=title_font)
    title_width = title_bbox[2] - title_bbox[0]
    single_draw.text(((600 - title_width) // 2, 30), title, fill="#1a365d", font=title_font)
    
    # Add URL
    url_text = url
    url_bbox = single_draw.textbbox((0, 0), url_text, font=subtitle_font)
    url_width = url_bbox[2] - url_bbox[0]
    single_draw.text(((600 - url_width) // 2, 520), url_text, fill="#4a5568", font=subtitle_font)
    
    premium_single.save("premium_portfolio_qr.png", "PNG", quality=100)
    
    print(f"✅ Premium QR codes generated successfully!")
    print(f"📁 Collection saved as: {output_path}")
    print(f"📁 Premium single saved as: premium_portfolio_qr.png")
    print(f"🌐 URL: {url}")
    print(f"🎨 4 different premium styles created")
    print(f"🖨️ Ready for professional use!")
    
    return output_path

if __name__ == "__main__":
    create_portfolio_qr_premium()