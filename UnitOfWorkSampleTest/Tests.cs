using System;
using System.Threading.Tasks;

using NUnit.Framework;

using UnitOfWorkSample.Interface;
using UnitOfWorkSample.Model;

namespace UnitOfWorkSampleTest
{
    public class Tests
    {
        [TestCaseSource(typeof(TestCases))]
        public async Task Test(TestCase testCase)
        {
            using (testCase)
            {
                testCase.Verify();
                TestContext.WriteLine("/////////// First Session:");
                await TestSessionAsync(testCase);
                TestContext.WriteLine("/////////// Second Session:");
                await TestSessionAsync(testCase);
            }
        }

        private async Task TestSessionAsync(TestCase testCase)
        {
            var partner = await CreatePartnerAsync(testCase);
            await CreateOrganizationAsync(testCase, partner.Id);
        }

        private async Task<Partner> CreatePartnerAsync(TestCase testCase)
        {
            using (testCase.BeginScope())
            {
                var unitOfWork = testCase.GetService<IUnitOfWork>();
                var localeRepository = testCase.GetService<ILocaleRepository>();

                var partner = new Partner { Name = Guid.NewGuid().ToString() };
                var locales = await localeRepository.GetLocalesAsync();
                partner.AddLocales(locales);

                var partnerRepository = testCase.GetService<IPartnerRepository>();

                partnerRepository.AddPartner(partner);
                
                await unitOfWork.SaveChangesAsync();

                return partner;
            }
        }

        private async Task<Organization> CreateOrganizationAsync(TestCase testCase, int partnerId)
        {
            using (testCase.BeginScope())
            {
                var unitOfWork = testCase.GetService<IUnitOfWork>();
                var partnerRepository = testCase.GetService<IPartnerRepository>();

                var partner = await partnerRepository.GetPartnerAsync(partnerId);

                var organization = new Organization { Name = Guid.NewGuid().ToString(), Partner = partner};

                var organizationRepository = testCase.GetService<IOrganizationRepository>();

                organizationRepository.AddOrganization(organization);
                
                await unitOfWork.SaveChangesAsync();

                return organization;
            }
        }
    }
}