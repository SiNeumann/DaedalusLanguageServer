/**************************************************************************************
    Stampfer - Gothic Script Editor
    Copyright (C) 2008 Jpmon1

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
**************************************************************************************/
using System;
using System.Runtime.InteropServices;

namespace Peter
{
    public class cShell32
    {
        public const int MAX_PATH = 256;
        [StructLayout(LayoutKind.Sequential)]
        public struct SHITEMID
        {
            public ushort cb;
            [MarshalAs(UnmanagedType.LPArray)]
            public byte[] abID;
        }

        // Browsing for directory.
        public const uint BIF_RETURNONLYFSDIRS = 0x0001;
        public const uint BIF_DONTGOBELOWDOMAIN = 0x0002;
        public const uint BIF_STATUSTEXT = 0x0004;
        public const uint BIF_RETURNFSANCESTORS = 0x0008;
        public const uint BIF_EDITBOX = 0x0010;
        public const uint BIF_VALIDATE = 0x0020;
        public const uint BIF_NEWDIALOGSTYLE = 0x0040;
        public const uint BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
        public const uint BIF_BROWSEINCLUDEURLS = 0x0080;
        public const uint BIF_BROWSEFORCOMPUTER = 0x1000;
        public const uint BIF_BROWSEFORPRINTER = 0x2000;
        public const uint BIF_BROWSEINCLUDEFILES = 0x4000;
        public const uint BIF_SHAREABLE = 0x8000;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public const int NAMESIZE = 80;
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAMESIZE)]
            public string szTypeName;
        };

        public const uint SHGFI_ICON = 0x000000100;     // get icon
        public const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
        public const uint SHGFI_TYPENAME = 0x000000400;     // get type name
        public const uint SHGFI_ATTRIBUTES = 0x000000800;     // get attributes
        public const uint SHGFI_ICONLOCATION = 0x000001000;     // get icon location
        public const uint SHGFI_EXETYPE = 0x000002000;     // return exe type
        public const uint SHGFI_SYSICONINDEX = 0x000004000;     // get system icon index
        public const uint SHGFI_LINKOVERLAY = 0x000008000;     // put a link overlay on icon
        public const uint SHGFI_SELECTED = 0x000010000;     // show icon in selected state
        public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
        public const uint SHGFI_LARGEICON = 0x000000000;     // get large icon
        public const uint SHGFI_SMALLICON = 0x000000001;     // get small icon
        public const uint SHGFI_OPENICON = 0x000000002;     // get open icon
        public const uint SHGFI_SHELLICONSIZE = 0x000000004;     // get shell size icon
        public const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;     // use passed dwFileAttribute
        public const uint SHGFI_ADDOVERLAYS = 0x000000020;     // apply the appropriate overlays
        public const uint SHGFI_OVERLAYINDEX = 0x000000040;     // Get the index of the overlay

        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        [DllImport("Shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags
            );
    }

    public class User32
    {
        /// <summary>
        /// Provides access to function required to delete handle. This method is used internally
        /// and is not required to be called separately.
        /// </summary>
        /// <param name="hIcon">Pointer to icon handle.</param>
        /// <returns>N/A</returns>
        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);
    }
}
