using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaedalusCompiler.Compilation
{
    public static class SrcFileHelper
    {

        public static string[] GetLines(string srcFilePath)
        {
            var lines = File.ReadAllLines(srcFilePath);
            for (var i = 0; i < lines.Length; ++i)
            {
                if (lines[i].Contains("//"))
                {
                    lines[i] = lines[i].Split("//").First();
                }
            }
            return lines;
        }

        public static List<string> LoadScriptsFilePaths(string srcFilePath)
        {
            Path.GetFileName(srcFilePath);
            return LoadScriptsFilePaths(srcFilePath, new HashSet<string>());
        }

        private static List<string> LoadScriptsFilePaths(string srcFilePath, HashSet<string> alreadyLoadedFiles)
        {
            if (Path.GetExtension(srcFilePath).ToLower() != ".src")
                throw new Exception($"Invalid SRC file: '{srcFilePath}'.");

            if (alreadyLoadedFiles.Contains(srcFilePath.ToLower()))
            {
                System.Diagnostics.Trace.WriteLine($"Cyclic dependency detected. SRC file '{srcFilePath}' is already loaded");
                return new List<string>();
            }

            alreadyLoadedFiles.Add(srcFilePath.ToLower());

            try
            {
                var lines = GetLines(srcFilePath);
                var basePath = Path.GetDirectoryName(srcFilePath);
                var result = LoadScriptsFilePaths(basePath, lines, alreadyLoadedFiles);
                return result;
            }
            catch (Exception exc)
            {
                throw new Exception($"Error while loading scripts file paths from SRC file '{srcFilePath}'", exc);
            }
        }


        private static string GetDirPathInsensitive(string basePath, string relativePath)
        {
            var resultPath = basePath;

            var options = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive };
            var relativePathSplitted = relativePath.Split(Path.DirectorySeparatorChar);
            var lastIndex = relativePathSplitted.Length - 1;

            for (var i = 0; i < lastIndex; i++)
            {
                var relativePathPart = relativePathSplitted[i];
                var directories = Directory.GetDirectories(resultPath, relativePathPart, options);
                if (directories.Length == 0)
                {
                    throw new DirectoryNotFoundException($"ERROR: Could not find a part of the path '{Path.Combine(resultPath, relativePathPart)}'");
                }

                if (directories.Length > 1)
                {
                    throw new DirectoryNotFoundException($"ERROR: Ambigous path '{Path.Combine(resultPath, relativePathPart)}'. Matches {string.Join(";", directories)}"); ;
                }

                resultPath = Path.Combine(resultPath, directories.First());
            }
            return resultPath;
        }

        private static List<string> GetFilesInsensitive(string dirPath, string filenamePattern)
        {
            var options = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive };
            var filePaths = Directory.GetFiles(dirPath, filenamePattern, options).ToList();
            if (filePaths.Count == 0)
            {
                System.Diagnostics.Trace.WriteLine($"WARNING: Could not find any files in '{dirPath}' that matches pattern '{filenamePattern}'");
            }

            return filePaths;
        }

        private static string GetFileInsensitive(string dirPath, string filenamePattern)
        {
            var filePaths = GetFilesInsensitive(dirPath, filenamePattern);
            if (filePaths.Count > 1)
            {
                System.Diagnostics.Trace.WriteLine($"WARNING: Ambigous path '{Path.Combine(dirPath, filenamePattern)}'. Matches {string.Join(";", filePaths)}");
            }
            if (filePaths.Count == 0)
            {
                return null;
            }
            return filePaths.First();
        }

        private static List<string> LoadScriptsFilePaths(string basePath, string[] srcLines, HashSet<string> alreadyLoadedFiles)
        {
            var result = new List<string>();

            foreach (var line in srcLines.Where(x => string.IsNullOrWhiteSpace(x) == false).Select(item => Path.Combine(item.Trim().Split("\\").ToArray())))
            {
                try
                {
                    var containsWildcard = line.Contains("*");
                    var relativePath = line;
                    var dirPath = GetDirPathInsensitive(basePath, relativePath);
                    var filenamePattern = Path.GetFileName(relativePath);
                    var pathExtensionLower = Path.GetExtension(filenamePattern).ToLower();

                    if (containsWildcard && pathExtensionLower == ".d")
                    {
                        var filePaths = GetFilesInsensitive(dirPath, filenamePattern);

                        // we make custom sort to achieve same sort results independent from OS 
                        filePaths.Sort((a, b) =>
                        {
                            if (a.StartsWith(b))
                            {
                                return a.Length > b.Length ? -1 : 1;
                            }

                            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
                        });

                        foreach (var filePath in filePaths)
                        {
                            if (File.Exists(filePath))
                            {
                                var filePathLower = filePath.ToLower();
                                if (!alreadyLoadedFiles.Contains(filePathLower))
                                {
                                    alreadyLoadedFiles.Add(filePathLower);
                                    result.Add(filePath);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Trace.WriteLine($"WARNING while parsing Gothic.src: '{filePath}' does not exist");
                            }
                        }
                    }
                    else if (pathExtensionLower == ".d")
                    {
                        var fullPath = GetFileInsensitive(dirPath, filenamePattern);
                        if (!string.IsNullOrWhiteSpace(fullPath))
                        {
                            if (File.Exists(fullPath))
                            {
                                var fullPathLower = fullPath.ToLower();
                                if (!alreadyLoadedFiles.Contains(fullPathLower))
                                {
                                    alreadyLoadedFiles.Add(fullPathLower);
                                    result.Add(fullPath);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Trace.WriteLine($"WARNING while parsing Gothic.src: '{fullPath}' does not exist");
                            }
                        } else
                        {
                                System.Diagnostics.Trace.WriteLine($"WARNING while parsing Gothic.src: no matches in '{dirPath}' for '{filenamePattern}'.");
                        }
                    }
                    else if (pathExtensionLower == ".src")
                    {
                        var fullPath = GetFileInsensitive(dirPath, filenamePattern);
                        var scriptsFiles = LoadScriptsFilePaths(fullPath, alreadyLoadedFiles);
                        if (scriptsFiles?.Count > 0)
                        {
                            result.AddRange(scriptsFiles);
                        }
                    }
                    else
                    {
                        throw new Exception("Unsupported script file format");
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception($"Invalid line {Array.IndexOf(srcLines, line) + 1}: '{line}'", exc);
                }
            }

            return result;
        }
    }
}
