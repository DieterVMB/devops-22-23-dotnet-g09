using GridBlazor;
using VirtualIT.Shared.Beheerder;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using VirtualIT.Client.Pages.Beheerders.CustomColumns;

namespace VirtualIT.Client.Pages.Beheerders
{
    public partial class BeheerdersLijst
    {
        private CGrid<BeheerderDto.Index> _grid;
        private Task _task;
        public static readonly Action<IGridColumnCollection<BeheerderDto.Index>> Columns = c =>
        {
            c.Add().RenderComponentAs<ButtonCellView>();
            c.Add().RenderComponentAs<ButtonCellEdit>();
            c.Add(o => o.Voornaam);
            c.Add(o => o.Naam);
            c.Add(o => o.Rol);
        };
        protected override async Task OnParametersSetAsync()
        {
            string url = "api/Beheerder/GetWithFilters";
            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");
            var client = new GridClient<BeheerderDto.Index>(HttpClient, url, query, false, "ordersGrid", Columns).Sortable().Filterable().Searchable(true, false, true).ClearFiltersButton(true).RearrangeableColumns().SetStriped(true).Selectable(true, false, false).WithMultipleFilters();
            _grid = client.Grid;
            // Set new items to grid
            _task = client.UpdateGrid();
            await _task;
        }
    }
}