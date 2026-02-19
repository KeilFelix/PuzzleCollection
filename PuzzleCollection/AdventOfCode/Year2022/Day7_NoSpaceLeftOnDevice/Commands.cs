// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice;

public interface IShellCommand { }
public record CdCommand(string Destination) : IShellCommand;
public record LsCommand(string[] Subdirectories, FileSystem.FileInfo[] Files) : IShellCommand;
