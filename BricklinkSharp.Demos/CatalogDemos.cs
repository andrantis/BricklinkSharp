﻿#region License
// Copyright (c) 2020 Jens Eisenbach
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Threading.Tasks;
using BricklinkSharp.Client;

namespace BricklinkSharp.Demos
{
    internal static class CatalogDemos
    {
        public static async Task GetKnownColorsDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var knownColors = await client.GetKnownColorsAsync(ItemType.Part, "3006");

            PrintHelper.PrintAsJson(knownColors);
        }

        public static async Task GetPriceGuideDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var priceGuide = await client.GetPriceGuideAsync(ItemType.Part, "3003", colorId: 1,
                priceGuideType: PriceGuideType.Sold, condition: Condition.Used);

            PrintHelper.PrintAsJson(priceGuide);
        }

        public static async Task GetSubsetsDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var subsets = await client.GetSubsetsAsync(ItemType.Set, "1095-1", breakMinifigs: false);

            PrintHelper.PrintAsJson(subsets);
        }

        public static async Task GetSupersetsDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var supersets = await client.GetSupersetsAsync(ItemType.Minifig, "aqu004", 0);

            PrintHelper.PrintAsJson(supersets);
        }

        public static async Task GetItemDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var catalogItem = await client.GetItemAsync(ItemType.Minifig, "sw0693");

            PrintHelper.PrintAsJson(catalogItem);
        }

        public static async Task GetItemImageDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var catalogImage = await client.GetItemImageAsync(ItemType.Part, "2540", 8);

            PrintHelper.PrintAsJson(catalogImage);
        }

        public static void GetPartImageForColorDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var uri = client.GetPartImageForColor("2540", 10);

            Console.WriteLine($"URL: {uri.AbsoluteUri}");
        }

        public static void EnsureImageUrlSchemeDemo()
        {
            using var client = BricklinkClientFactory.Build();
            var uri = client.EnsureImageUrlScheme("//img.bricklink.com/ItemImage/PN/34/43898pb006.png", "https");

            Console.WriteLine($"URL: {uri.AbsoluteUri}");
        }
    }
}
