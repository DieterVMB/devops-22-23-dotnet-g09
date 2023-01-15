using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Shouldly;
using Xunit;

namespace VirtualIT.Tests.VirtualMachines {
    [Parallelizable(ParallelScope.Self)]
    public class VirtualMachinesOverzichtTests : PageTest
    {

       /* [Test]
        public async Task Toon_Beheerder_Details_OnLoad()
        {
            await Page.GotoAsync($"{TestHelper.BaseUri}/vm/overzicht");
            await Page.WaitForSelectorAsync("data-test-id=pagina-titel");
            var klantTitel = await Page.TextContentAsync("data-test-id=pagina-titel");
            klantTitel.ShouldNotBeEmpty();
        }

        [Test]
        public async Task Show_Index_With_25_Items_On_Load()
        {
            await Page.GotoAsync($"{TestHelper.BaseUri}/vm/overzicht");
            await Page.WaitForSelectorAsync("data-test-id=VM-overview");
            var amountOfVms = await Page.Locator("data-test-id=VM-overview").CountAsync();
            amountOfVms.ShouldBe(25);
        }

        [Test]
        public async Task Toon_VM_Details_OnLoad()
        {
            await Page.GotoAsync($"{TestHelper.BaseUri}/vm/overzicht");
            await Page.WaitForSelectorAsync("data-test-id=pagina-titel");
            var vm = await Page.TextContentAsync("data-test-id=pagina-titel");
            vm.ShouldNotBeEmpty();

        }*/
    }
}
