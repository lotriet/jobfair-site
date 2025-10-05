# QR Code with Logo Generator

This enhanced QR code generator creates professional QR codes with logos embedded in the center, perfect for business cards, portfolios, and marketing materials.

## Features

- ✅ QR codes with logo overlay support
- ✅ High error correction for reliable scanning
- ✅ Professional circular logo design
- ✅ Custom logo file support
- ✅ Both Python and HTML versions
- ✅ Print-ready output

## Quick Start

### Option 1: Batch File (Easiest)

```bash
generate_qr_with_logo.bat
```

### Option 2: Python Scripts

```bash
# Create sample logo
python create_sample_logo.py

# Generate QR code with logo
python generate_qr.py
```

### Option 3: HTML Version

Open `qr-code-generator.html` in your browser for an interactive version.

## Using Your Own Logo

1. **Replace the sample logo**: Save your logo as `logo.png` in the project directory
2. **Supported formats**: PNG, JPG, JPEG (PNG recommended for transparency)
3. **Recommended size**: 100x100 pixels or larger (will be automatically resized)
4. **Best practices**:
   - Use high contrast logos
   - Simple designs work best
   - Square aspect ratio preferred

## Technical Details

### Error Correction

- Uses **High (H)** error correction level
- Allows up to 30% of QR code to be damaged/obscured
- Ensures reliable scanning even with logo overlay

### Logo Specifications

- **Size**: 80x80 pixels (adjustable in code)
- **Position**: Centered in QR code
- **Shape**: Automatically converted to circular
- **Background**: White circle with black border

### Output Files

- `portfolio_qr_code_with_logo.png` - Complete QR code with logo
- `logo.png` - Your logo file (sample created if not exists)

## Customization

### Change Logo Size

Edit `generate_qr.py` line with `create_logo(size=80)` and change the size value.

### Change Logo File

Edit `generate_qr.py` and update the `logo_path` parameter:

```python
logo = create_logo(size=80, logo_path="your_logo.png")
```

### Change Initials

Edit `create_sample_logo.py` and change the `text = "LT"` line to your initials.

## Browser Compatibility

The HTML version works in all modern browsers and includes:

- Interactive QR code generation
- Logo overlay visualization
- Download functionality with embedded logo

## Tips for Best Results

1. **Test your QR code**: Always test with multiple QR scanners
2. **Print quality**: Use high-resolution output for professional printing
3. **Size considerations**: Larger QR codes scan more reliably
4. **Logo contrast**: Ensure good contrast between logo and background
5. **Keep it simple**: Complex logos may interfere with scanning

## Troubleshooting

**QR code won't scan?**

- Try reducing logo size
- Ensure high contrast
- Test with different QR scanner apps

**Logo looks blurry?**

- Use higher resolution source image
- Save as PNG for better quality
- Increase canvas size in code

**Custom logo not appearing?**

- Check file path and name
- Ensure file format is supported
- Verify file isn't corrupted
