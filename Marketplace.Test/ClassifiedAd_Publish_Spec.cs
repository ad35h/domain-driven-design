using Marketplace.Domain;
using Marketplace.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Marketplace.Test
{
    public class ClassifiedAd_Publish_Spec
    {
        private readonly ClassifiedAd _classifiedAd;

        public ClassifiedAd_Publish_Spec()
        {
            _classifiedAd = new ClassifiedAd(
            new ClassifiedAdId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_publish_a_valid_ad()
        {
            _classifiedAd.SetTitle(
            ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(
            ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(
            Price.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));
            _classifiedAd.RequestToPublish();
            Assert.Equal(ClassifiedAd.ClassifiedAdState.PendingReview,
            _classifiedAd.State);
        }


    }
}
