using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyScraper.Tests.ScraperFixtures
{
    [TestFixture]
    class When_website_contains_an_external_link : ConcernForScraper
    {
        readonly List<HtmlDoc> htmlDocs = new List<HtmlDoc>();
        string page;

        protected override void Context()
        {
            base.Context();
            Uri = new Uri("http://test");
            page = @"<html>
                         <a href=""http://foo"">link1</a>
                         </html>";
            HttpClient.GetString(Uri).Returns(new Task<string>(() => page));
            OnNext = x => htmlDocs.Add(x);
        }

        [Test]
        public void Then_one_htmldoc_should_be_returned()
        {
            htmlDocs.Count.Should().Be(1);
        }

        [Test]
        public void Then_htmldocs_should_contain_home_page()
        {
            htmlDocs.Should().Contain(x => x.Uri.ToString() == "http://test/" && x.Html == page);
        }
    }

    [TestFixture, Explicit]
    class When_
    {
        [Test]
        public void Then_()
        {
            var scraper = new Scraper();
            scraper.Subscribe(x => Console.WriteLine(x.Uri));
            scraper.Scrape(new Uri("http://www.craftyfella.com"));
        }
    }
}