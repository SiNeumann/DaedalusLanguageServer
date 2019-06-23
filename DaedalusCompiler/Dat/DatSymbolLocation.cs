﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DaedalusCompiler.Dat
{
    [DebuggerDisplay("F:{FileNumber} L:{Line} LC:{LinesCount} P:{Position} PC:{PositionsCount}")]
    public class DatSymbolLocation
    {
        /// <summary>
        /// Zero based source file number
        /// </summary>
        public int FileNumber { get; set; }

        /// <summary>
        /// Line number in source file where symbol is located
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Number of lines (count) in source file taken by symbol definition
        /// </summary>
        public int LinesCount { get; set; }

        /// <summary>
        /// Char position from begining of source file where symbol is located
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Number of characters to next symbol
        /// </summary>
        public int PositionsCount { get; set; }
    }
}
