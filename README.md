# TextFileParse

A command-line tool for parsing and reordering Qualcomm (Qcom) 1.6 comlab log files.

## Purpose

This tool addresses an order mismatch issue with RX (receive) and TX (transmit) messages in log files generated from UGP V7 code. It reorders the messages into the expected sequence and outputs a corrected log file.

**Note:** This is a temporary solution until a fix is implemented in the platform to store messages in the correct order.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

## Building the Project

To build the project, navigate to the project directory and run:

```bash
dotnet build
```

This will compile the application and create the executable in the `bin/Debug/net8.0/` directory.

## Usage

### Running the Tool

Run the tool with the following command:

```bash
dotnet run -- <input-file> [output-file]
```

Or, after building, run the executable directly:

```bash
./bin/Debug/net8.0/TextFileParse <input-file> [output-file]
```

### Command-Line Arguments

- `input-file` (required): Path to the comlab RTB file to parse
- `output-file` (optional): Path to the output file. If not specified, defaults to the input filename with `.parsed` appended

### Examples

1. Parse a file with default output name:
   ```bash
   dotnet run -- qcom_in.txt
   ```
   This creates `qcom_in.txt.parsed` as the output.

2. Parse a file with custom output name:
   ```bash
   dotnet run -- qcom_in.txt qcom_out.txt
   ```
   This creates `qcom_out.txt` as the output.

## How It Works

The tool processes the input file and reorders messages according to the following pattern:

1. **Finds Global RX messages** - Uses these as reference points
2. **Locates TX messages** - Identifies transmit messages that may be out of order
3. **Finds RX messages** - Locates the corresponding receive messages
4. **Writes in correct order** - Outputs messages in the sequence: Global → RX → TX

This ensures that the log file follows the expected message flow for analysis and debugging purposes.

## Input File Format

The tool expects text files containing Qcom comlab logs with:
- Global RX markers
- TX (transmit) message lines containing "TX"
- RX (receive) message lines containing "RX : 01"

## Limitations

- This is not the most efficient implementation but is designed for occasional use
- The tool is specifically designed for Qcom 1.6 comlab log files
- Future platform updates are expected to resolve the underlying ordering issue

## License

Please refer to the repository license for usage terms. 
