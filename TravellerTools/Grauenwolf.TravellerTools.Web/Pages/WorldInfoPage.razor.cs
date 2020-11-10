﻿using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Data;
using Grauenwolf.TravellerTools.Web.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    public class WorldInfoPageBase : PageBase<WorldModel>
    {
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;
        [Inject] NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        public string? MilieuCode
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        [Parameter]
        public string? SectorHex
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        [Parameter]
        public string? PlanetHex
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        public int? Seed
        {
            get => Get<int?>();
            set => Set(value, true);
        }

        protected override void Initialized()
        {
            if (NavigationManager.TryGetQueryString("seed", out int seed))
                Seed = seed;
            else
                Seed = (new Random()).Next();
        }

        protected void Reroll(MouseEventArgs _)
        {
            NavigationManager.NavigateTo($"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/info", false);
        }

        protected void Permalink(MouseEventArgs _)
        {
            NavigationManager.NavigateTo($"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/info?seed={Seed}", true);
        }

        protected override async Task ParametersSetAsync()
        {
            if (PlanetHex == null || SectorHex == null || MilieuCode == null)
                goto ReturnToIndex;

            var milieu = Milieu.FromCode(MilieuCode);
            if (milieu == null)
                goto ReturnToIndex;

            var service = TravellerMapServiceLocator.GetMapService(MilieuCode);
            var world = await service.FetchWorldAsync(SectorHex, PlanetHex);
            if (world == null)
                goto ReturnToIndex;

            Model = new WorldModel(milieu, world);
            PageTitle = Model.World.Name;

            if (Seed != null)
            {
                var dice = new Dice(Seed.Value);
                Model.HighportDetails = TradeEngine.CalculateStarportDetails(Model.World, dice, true);
                Model.DownportDetails = TradeEngine.CalculateStarportDetails(Model.World, dice, false);
            }
            return;

            ReturnToIndex:
            Navigation.NavigateTo("/"); //bounce back to home.
        }
    }
}