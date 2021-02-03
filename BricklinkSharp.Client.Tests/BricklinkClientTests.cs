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
using System.Net;
using NUnit.Framework;

namespace BricklinkSharp.Client.Tests
{
    //Code taken from https://stackoverflow.com/questions/153451/how-to-check-if-system-net-webclient-downloaddata-is-downloading-a-binary-file#156750,
    class CustomWebClient : WebClient
    {
        internal bool HeadOnly { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);

            if (HeadOnly && request!.Method == "GET")
            {
                request.Method = "HEAD";
            }

            return request;
        }
    }

    public class BricklinkClientTests
    {
        private bool CheckUriExists(Uri uri)
        {
            try
            {
                using var client = new CustomWebClient { HeadOnly = true };
                var data = client.DownloadData(uri);
                return data.Length == 0;
            }
            catch
            {
                return false;
            }
        }

        [TestCase("//img.bricklink.com/ItemImage/PN/34/43898pb006.png", "https")]
        [TestCase("//img.bricklink.com/ItemImage/PN/34/43898pb006.png", "http")]
        public void EnsureImageUrlScheme(string url, string scheme)
        {
            using var client = BricklinkClientFactory.Build();
            var uri = client.EnsureImageUrlScheme(url, scheme);

            Assert.AreEqual($"{scheme}://img.bricklink.com/ItemImage/PN/34/43898pb006.png", uri.AbsoluteUri);
        }

        [TestCase("2540", 10, "https")]
        [TestCase("2540", 10, "http")]
        [TestCase("43898pb006", 34, "https")]
        public void GetPartImageForColor(string partNo, int colorId, string scheme)
        {
            using var client = BricklinkClientFactory.Build();
            var uri = client.GetPartImageForColor(partNo, colorId, scheme);

            Assert.AreEqual($"{scheme}://img.bricklink.com/ItemImage/PN/{colorId}/{partNo}.png", uri.AbsoluteUri);
            Assert.IsTrue(CheckUriExists(uri));
        }

        [TestCase("soc130", "https")]
        [TestCase("soc130", "http")]
        [TestCase("85863pb095", "https")]
        [TestCase("sw1093", "https")]
        public void GetMinifigImage(string number, string scheme)
        {
            using var client = BricklinkClientFactory.Build();
            var uri = client.GetMinifigImage(number, scheme);

            Assert.AreEqual($"{scheme}://img.bricklink.com/ItemImage/MN/0/{number}.png", uri.AbsoluteUri);
            Assert.IsTrue(CheckUriExists(uri));
        }

        [TestCase("723-1", "https")]
        [TestCase("723-1", "http")]
        [TestCase("7774-1", "https")]
        [TestCase("6090-1", "https")]
        public void GetSetImage(string number, string scheme)
        {
            using var client = BricklinkClientFactory.Build();
            var uri = client.GetSetImage(number, scheme);

            Assert.AreEqual($"{scheme}://img.bricklink.com/ItemImage/SN/0/{number}.png", uri.AbsoluteUri);
            Assert.IsTrue(CheckUriExists(uri));
        }
    }
}
