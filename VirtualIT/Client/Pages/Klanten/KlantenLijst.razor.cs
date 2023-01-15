using Microsoft.AspNetCore.Components;
using GridBlazor;
using VirtualIT.Shared.Beheerder;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using VirtualIT.Shared.Klanten;
using VirtualIT.Client.Pages.Klanten.CustomColumns;

namespace VirtualIT.Client.Pages.Klanten
{
    public partial class KlantenLijst
    {
        private CGrid<KlantDto.Index> _grid;
        private Task _task;
        public static readonly Action<IGridColumnCollection<KlantDto.Index>> Columns = c =>
        {
            c.Add().RenderComponentAs<ButtonCellView>();
            c.Add().RenderComponentAs<ButtonCellEdit>();
            c.Add(o => o.Extern).RenderValueAs(o => o.Extern ? "Extern" : "Intern").Titled("Type klant");
            c.Add(o => o.KlantNaam);
            c.Add(o => o.Departement);
            c.Add(o => o.Opleiding);
        };
        protected override async Task OnParametersSetAsync()
        {
            string url = "api/Klant/GetWithFilters";
            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");
            var client = new GridClient<KlantDto.Index>(HttpClient, url, query, false, "ordersGrid", Columns).Sortable().Filterable().Searchable(true, false, true).ClearFiltersButton(true).RearrangeableColumns().SetStriped(true).Selectable(true, false, false).WithMultipleFilters();
            _grid = client.Grid;
            // Set new items to grid
            _task = client.UpdateGrid();
            await _task;
        }
    }
}