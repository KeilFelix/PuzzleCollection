# PuzzleCollection
## Status [![Build & Test](https://github.com/KeilFelix/PuzzleCollection/actions/workflows/dotnet.yml/badge.svg)](https://github.com/KeilFelix/PuzzleCollection/actions/workflows/dotnet.yml)

## About
This puzzle collection showcases solutions with minimal yet easily understandable declarative code and a basic object-oriented design.

It contains solutions for
* [AdventOfCode](https://adventofcode.com/) - Creative puzzles with beautiful stories and entities that allow us to craft a great domain driven software design
* [ProjectEuler](https://projecteuler.net/) - Nice puzzles, but heavily math-/algo-oriented.
* [CodeWars](https://www.codewars.com/) - Some good and some bad coding puzzles

## Structure
* **[PuzzleCollection](PuzzleCollection)**  
  Well structured declarative solutions with a minimal object-oriented design, paired with the puzzle input.
* [PuzzleCollection.Util](PuzzleCollection.Util):  
  A utility library that enhances .NET functionality (important: no domain knowledge of the puzzles!).
* [PuzzleCollection.Test](PuzzleCollection.Test):  
  A single generic test that asserts all answers are still correct, along with explicit tests if provided by the puzzle.
 * [PuzzleCollection.ExperimentingConsole](PuzzleCollection.ExperimentingConsole):  
  Just for temporary code to try out and fiddle during puzzle creation. 
