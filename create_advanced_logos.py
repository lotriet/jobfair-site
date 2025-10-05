#!/usr/bin/env python3
"""
Advanced Logo Creator for QR Codes
Creates various logo styles for professional QR codes
"""

from PIL import Image, ImageDraw, ImageFont
import os

def create_coffee_logo(size=100):
    """Create a coffee cup logo - perfect for café business cards"""
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    # Background circle
    margin = 5
    draw.ellipse([margin, margin, size - margin, size - margin], 
                fill=(139, 69, 19, 255), outline=(101, 67, 33, 255), width=3)
    
    # Coffee cup shape
    cup_width = size // 3
    cup_height = size // 2.5
    cup_x = (size - cup_width) // 2
    cup_y = (size - cup_height) // 2 + 5
    
    # Cup body
    draw.rectangle([cup_x, cup_y, cup_x + cup_width, cup_y + cup_height], 
                  fill=(255, 255, 255, 255))
    
    # Coffee inside
    coffee_margin = 3
    draw.rectangle([cup_x + coffee_margin, cup_y + coffee_margin, 
                   cup_x + cup_width - coffee_margin, cup_y + cup_height - coffee_margin], 
                  fill=(101, 67, 33, 255))
    
    # Steam lines
    steam_x = cup_x + cup_width // 2
    for i in range(3):
        x_offset = (i - 1) * 4
        draw.line([(steam_x + x_offset, cup_y - 10), 
                  (steam_x + x_offset, cup_y - 5)], 
                 fill=(255, 255, 255, 180), width=2)
    
    return logo

def create_tech_logo(size=100):
    """Create a tech/coding logo with brackets"""
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    # Background gradient effect
    for i in range(3):
        radius = size // 2 - (i * 6)
        alpha = 255 - (i * 50)
        colors = [(0, 123, 255, alpha), (40, 167, 69, alpha), (255, 193, 7, alpha)]
        if radius > 0:
            draw.ellipse([size//2 - radius, size//2 - radius, 
                         size//2 + radius, size//2 + radius], 
                        fill=colors[i])
    
    # Draw brackets < >
    try:
        font = ImageFont.truetype("arial.ttf", size // 2)
    except:
        font = ImageFont.load_default()
    
    text = "</>"
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    
    text_x = (size - text_width) // 2
    text_y = (size - text_height) // 2
    
    # Shadow
    draw.text((text_x + 1, text_y + 1), text, fill=(0, 0, 0, 150), font=font)
    # Main text
    draw.text((text_x, text_y), text, fill=(255, 255, 255, 255), font=font)
    
    return logo

def create_business_logo(initials="LT", size=100):
    """Create a professional business logo with initials"""
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    # Professional color scheme
    bg_color = (33, 47, 61, 255)  # Dark blue-gray
    accent_color = (52, 152, 219, 255)  # Professional blue
    
    # Background circle with gradient effect
    draw.ellipse([2, 2, size - 2, size - 2], 
                fill=bg_color, outline=accent_color, width=4)
    
    # Inner accent ring
    inner_margin = 8
    draw.ellipse([inner_margin, inner_margin, size - inner_margin, size - inner_margin], 
                outline=accent_color, width=2)
    
    # Initials
    try:
        font = ImageFont.truetype("arial.ttf", size // 2.5)
    except:
        font = ImageFont.load_default()
    
    bbox = draw.textbbox((0, 0), initials, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    
    text_x = (size - text_width) // 2
    text_y = (size - text_height) // 2
    
    draw.text((text_x, text_y), initials, fill=(255, 255, 255, 255), font=font)
    
    return logo

def create_creative_logo(size=100):
    """Create a creative/artistic logo"""
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    # Colorful background
    colors = [(255, 99, 132), (54, 162, 235), (255, 205, 86), (75, 192, 192)]
    
    # Create pie chart effect
    start_angle = 0
    for i, color in enumerate(colors):
        end_angle = start_angle + 90
        draw.pieslice([5, 5, size - 5, size - 5], start_angle, end_angle, 
                     fill=(*color, 200))
        start_angle = end_angle
    
    # Center circle
    center_size = size // 2
    center_margin = (size - center_size) // 2
    draw.ellipse([center_margin, center_margin, 
                 center_margin + center_size, center_margin + center_size], 
                fill=(255, 255, 255, 255), outline=(0, 0, 0, 255), width=2)
    
    # Star or symbol in center
    try:
        font = ImageFont.truetype("arial.ttf", size // 4)
    except:
        font = ImageFont.load_default()
    
    text = "★"
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    
    text_x = (size - text_width) // 2
    text_y = (size - text_height) // 2
    
    draw.text((text_x, text_y), text, fill=(255, 193, 7, 255), font=font)
    
    return logo

def main():
    """Create different logo options"""
    print("🎨 Advanced Logo Creator")
    print("=" * 30)
    
    # Create different logo styles
    logos = {
        "logo_coffee.png": create_coffee_logo(),
        "logo_tech.png": create_tech_logo(),
        "logo_business.png": create_business_logo("LT"),
        "logo_creative.png": create_creative_logo()
    }
    
    for filename, logo in logos.items():
        logo.save(filename, "PNG")
        print(f"✅ Created: {filename}")
    
    print("\n💡 Usage:")
    print("1. Choose your preferred logo style")
    print("2. Rename it to 'logo.png' or update generate_qr.py")
    print("3. Run generate_qr.py to create QR code with your chosen logo")
    print("\n🎯 Logo styles:")
    print("• logo_coffee.png - Perfect for cafés, restaurants")
    print("• logo_tech.png - Great for developers, tech companies")
    print("• logo_business.png - Professional business cards")
    print("• logo_creative.png - Artists, designers, creative fields")

if __name__ == "__main__":
    main()