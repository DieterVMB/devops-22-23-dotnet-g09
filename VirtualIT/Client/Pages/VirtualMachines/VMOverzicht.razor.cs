using GridBlazor;
using VirtualIT.Client.Pages.VirtualMachines.CustomColumns;
using VirtualIT.Shared.VirtualMachines;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using MudBlazor.Extensions;

namespace VirtualIT.Client.Pages.VirtualMachines
{
    public partial class VMOverzicht
    {
        private CGrid<VirtualMachineDto.Detail> _grid;
        private Task _task;
        public static readonly Action<IGridColumnCollection<VirtualMachineDto.Detail>> Columns = c =>
        {
            c.Add().RenderComponentAs<ButtonCellView>();
            c.Add().RenderComponentAs<ButtonCellEdit>();
            c.Add(o => o.Naam);
            c.Add(o => o.Hostname).Titled("Hostnaam");
            c.Add(o => o.DatumAanvraag).RenderValueAs(o => o.DatumAanvraag.ToString("dd/MM/yyyy")).Titled("Datum aanvraag");
            c.Add(o => o.Startdatum).RenderValueAs(o => o.Startdatum.ToString("dd/MM/yyyy"));
            c.Add(o => o.Einddatum).RenderValueAs(o => o.Einddatum.ToString("dd/MM/yyyy"));
            c.Add(o => o.Mode);
            c.Add(o => o.Backup).RenderValueAs(o => o.Backup ? "Ja" : "Nee").Titled("Backups");
            c.Add(o => o.EmailAanvrager).Titled("Aanvrager");
            c.Add(o => o.Status).RenderValueAs(o => o.Status.ToDescriptionString());
        //c.Add(o => o.FQDN);
        //c.Add(o => o.Memory);
        //c.Add(o => o.Storage);
        };
        protected override async Task OnParametersSetAsync()
        {
            string url = "api/VirtualMachine/GetWithFilters";
            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");
            var client = new GridClient<VirtualMachineDto.Detail>(HttpClient, url, query, false, "ordersGrid", Columns).Sortable().Filterable().Searchable(true, false, true).ClearFiltersButton(true).RearrangeableColumns().SetStriped(true).WithMultipleFilters();
            _grid = client.Grid;
            // Set new items to grid
            _task = client.UpdateGrid();
            await _task;
        }
    }
}