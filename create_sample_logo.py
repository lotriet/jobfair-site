#!/usr/bin/env python3
"""
Creates a sample logo for the QR code
"""

from PIL import Image, ImageDraw, ImageFont

def create_sample_logo():
    """Create a professional sample logo"""
    size = 100
    logo = Image.new('RGBA', (size, size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(logo)
    
    # Create a gradient-like effect with multiple circles
    colors = [(41, 128, 185), (52, 152, 219), (155, 89, 182)]
    
    for i, color in enumerate(colors):
        radius = size // 2 - (i * 8)
        if radius > 0:
            draw.ellipse([size//2 - radius, size//2 - radius, 
                         size//2 + radius, size//2 + radius], 
                        fill=(*color, 200))
    
    # Add initials in the center
    text = "LT"
    try:
        font = ImageFont.truetype("arial.ttf", size // 3)
    except:
        font = ImageFont.load_default()
    
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_height = bbox[3] - bbox[1]
    
    text_x = (size - text_width) // 2
    text_y = (size - text_height) // 2 - 5
    
    # Add text shadow
    draw.text((text_x + 1, text_y + 1), text, fill=(0, 0, 0, 100), font=font)
    # Add main text
    draw.text((text_x, text_y), text, fill=(255, 255, 255, 255), font=font)
    
    # Save the logo
    logo.save("logo.png", "PNG")
    print("✅ Sample logo created: logo.png")
    print("🎨 You can replace this with your own logo file")

if __name__ == "__main__":
    create_sample_logo()