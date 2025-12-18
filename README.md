# OTPCommand

A command-line utility for generating Time-Based One-Time Passwords (TOTP) codes from a secret key. OTPCommand automatically generates the current TOTP code and copies it to the clipboard, making it convenient for use in scripts, automation workflows, or as a command-line tool for two-factor authentication (2FA) codes.

## Description

OTPCommand is a .NET 6.0 console application that:

- Accepts a Base32-encoded secret key as input
- Generates a TOTP (Time-Based One-Time Password) code using the current time
- Displays the code in the console
- Automatically copies the code to the system clipboard for easy pasting

This is useful for:

- Automating 2FA code generation in scripts
- Creating custom authentication workflows
- Integration into other tools that require TOTP codes

## Requirements

- .NET 6.0 Runtime or later
- Windows operating system (uses Windows clipboard API)

## Build

Clone the repository and build using the .NET CLI:

```bash
cd OTPCommand
dotnet build -c Release
```

The built executable will be in `bin/Release/net6.0/OTPCommand.exe`.

## Usage

### Basic Usage

Generate a TOTP code from a Base32-encoded secret key:

```bash
OTPCommand.exe -k <secret-key>
```

**Parameters:**

- `-k <secret-key>`: The Base32-encoded secret key for TOTP generation (required)

### Examples

```bash
# Generate TOTP code from a secret key
OTPCommand.exe -k JBSWY3DPEBLW64TMMQ======

# The output will display the 6-digit TOTP code
123456
```

The generated code is output to the console and automatically copied to the clipboard.

## Exit Codes

- `0`: Success - TOTP code generated successfully
- `-1`: Error - Invalid arguments or secret key format

## Dependencies

- **OTP.NET** (v1.3.0): TOTP code generation
- **CommandLineSwitchParser** (v1.1.0): Command-line argument parsing
