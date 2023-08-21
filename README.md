# Cardamatic

Cardamatic is a tool that uses DSL (Domain Specific Language) to map the content of MiFare card memory to a graphical user interface. This repository contains the source code for all public components.

## Content

1. [Introduction](#introduction)
2. [Components](#components)
3. [Example](#example)

## Introduction

This project focuses on mapping the memory of transport cards to a graphical interface using a DSL. The DSL allows you to define memory schemas and efficiently map transport card data to graphical elements.\

Data conversion from card.\
![Data conversion from card](https://github.com/gguerra10/Cardamatic-public/blob/master/doc/card-2-data.png?raw=true)

Data conversion to card.\
![Data conversion to card](https://github.com/gguerra10/Cardamatic-public/blob/master/doc/data-2-card.png?raw=true)


## Components

- GGuerra.Cardamatic.CardReader.Interface: Defines the interface for a contactless card reader.
- GGuerra.Cardamatic.CardReader.Pcsc: Implements a contactless card reader using PC/SC.
- GGuerra.Cardamatic.CardReader.Common: contains common logical parts of a contactless card reader.
- GGuerra.Cardamatic.Encoding.Interface: defines the interface for a data encoder/decoder.
- GGuerra.Cardamatic.Encoding: Contains common logic for all encoders/decoders.
- GGuerra.Cardamatic.Encoding.System: Encoder/decoder for native fields. Includes types: bool, char, byte, ushort, short, uint, int, long.
- GGuerra.Cardamatic.Encoding.BinaryString: Encoder/decoder for binary representation fields.
- GGuerra.Cardamatic.Encoding.HexString: Encoder/decoder for hexadecimal representation fields.
- GGuerra.Cardamatic.Encoding.Balance: Encoder/decoder for balance fields on MiFare cards.
- GGuerra.Cardamatic.Encoding.CheckSum: Example encoder/decoder for a checksum field type.
- GGuerra.Cardamatic.Encoding.Date: Example encoder/decoder for a date field type.
- GGuerra.Cardamatic.Encoding.DateTime: Example encoder/decoder for a date-time field type.
- GGuerra.Cardamatic.Encoding.Time: Example encoder/decoder for a time field type.
- GGuerra.Cardamatic.WinForm: Contains all the logic related to the graphical interface.
- GGuerra.Cardamatic.Extensions: Extensions methods library.
- GGuerra.Cardamatic.KeysEncrypter: Executable for encrypting the keys used in the solution.
- GGuerra.Cardamatic.App: Executable application that uses all the above libraries through dependency injection.


## Example

Below is an example that demonstrates part of the solution's potential. With the following schema, you can represent raw content.
```json
{
  "Description": "Ultralight RAW",
  "ContactlessTechnology": "Ultralight",
  "Tabs": [
    {
      "Description": "Raw content",
      "Columns": [
        {
          "Description": "Raw Data (0-7)",
          "Properties": [
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page0",
              "address": 0,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page1",
              "address": 1,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page2",
              "address": 2,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page3",
              "address": 3,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page4",
              "address": 4,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page5",
              "address": 5,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page6",
              "address": 6,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page7",
              "address": 7,
              "offset": 0,
              "length": 4
            }
          ]
        },
        {
          "Description": "Raw Data (8-15)",
          "Properties": [
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page8",
              "address": 8,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page9",
              "address": 9,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page10",
              "address": 10,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page11",
              "address": 11,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page12",
              "address": 12,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page13",
              "address": 13,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page14",
              "address": 14,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "Page15",
              "address": 15,
              "offset": 0,
              "length": 4
            }
          ]
        }
      ]
    }
  ]
}
```
![Raw data](https://github.com/gguerra10/Cardamatic-public/blob/master/doc/example-raw.png?raw=true)

Selecting another schema like the following one allows you to format the card data, providing a more comprehensive view.
```json
{
  "Description": "Ultralight Px",
  "ContactlessTechnology": "Ultralight",
  "Tabs": [
    {
      "Description": "General Data",
      "Columns": [
        {
          "Description": "Card Data",
          "Properties": [
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "SerialNumber_H",
              "address": 0,
              "offset": 0,
              "length": 3
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "SerialNumber_L",
              "address": 1,
              "offset": 0,
              "length": 4
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "B0",
              "address": 0,
              "offset": 3,
              "length": 1
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "B1",
              "address": 2,
              "offset": 0,
              "length": 1
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "PRI",
              "address": 2,
              "offset": 1,
              "length": 1
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.HexString.HexString",
              "name": "PRL",
              "address": 2,
              "offset": 2,
              "length": 2
            },
            {
              "typeName": "GGuerra.Cardamatic.Encoding.BinaryString.BinaryString",
              "name": "OTP",
              "address": 3,
              "offset": 0,
              "length": 4
            }
          ]
        },
        {
          "Description": "Company Data",
          "Properties": [
            {
              "typeName": "System.UInt16",
              "name": "Version",
              "address": 4,
              "offset": 0,
              "length": 0,
              "offsetBits": 5,
              "lengthBits": 3
            },
            {
              "typeName": "System.UInt16",
              "name": "Company",
              "address": 4,
              "offset": 0,
              "length": 0,
              "offsetBits": 0,
              "lengthBits": 5
            },
            {
              "typeName": "System.UInt16",
              "name": "CardType",
              "address": 4,
              "offset": 0,
              "length": 0,
              "offsetBits": 5,
              "lengthBits": 3
            },
            {
              "typeName": "System.Boolean",
              "name": "Active",
              "address": 4,
              "offset": 0,
              "length": 0,
              "offsetBits": 4,
              "lengthBits": 1
            }
          ]
        }
      ]
    }
  ]
}
```
![Formatted data](https://github.com/gguerra10/Cardamatic-public/blob/master/doc/example-data.png?raw=true)
