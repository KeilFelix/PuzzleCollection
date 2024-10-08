﻿using PuzzleCollection.CodeWars.RailFenceCipher_EncodingAndDecoding;

namespace PuzzleCollection.Test.CodeWars.RailFenceCipher_EncodingAndDecoding;

public class RailFenceCipherTest
{
    [Test]
    public void EncodeSampleTests()
    {
        string[][] encodes =
        [
            ["WEAREDISCOVEREDFLEEATONCE", "WECRLTEERDSOEEFEAOCAIVDEN"],  // 3 rails
            ["Hello, World!", "Hoo!el,Wrdl l"],    // 3 rails
            ["Hello, World!", "H !e,Wdloollr"],    // 4 rails
            ["", ""]                               // 3 rails (even if...)
        ];
        int[] rails = { 3, 3, 4, 3 };

        for (int i = 0; i < encodes.Length; i++)
        {
            Assert.That(RailFenceCipher.Encode(encodes[i][0], rails[i]), Is.EqualTo(encodes[i][1]));
        }
    }

    [Test]
    public void DecodeSampleTests()
    {
        string[][] decodes =
        [
            ["WECRLTEERDSOEEFEAOCAIVDEN", "WEAREDISCOVEREDFLEEATONCE"],    // 3 rails
            ["H !e,Wdloollr", "Hello, World!"],    // 4 rails
            ["", ""]                               // 3 rails (even if...)
        ];
        int[] rails = { 3, 4, 3 };

        for (int i = 0; i < decodes.Length; i++)
        {
            Assert.That(RailFenceCipher.Decode(decodes[i][0], rails[i]), Is.EqualTo(decodes[i][1]));
        }
    }
}
